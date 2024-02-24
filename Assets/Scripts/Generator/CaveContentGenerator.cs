using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
  public class CaveContentGenerator : MonoBehaviour {
    [HideInInspector] public bool done_generating = false;
    [HideInInspector] public CaveGenStats cave_profile;
    public GeneratorTools gen_tools;
    public System.Random gen;

    [Header("Stuff to generate")]
    [Space(5)]
    public GameObject kobalt_pref;

    private List<Vector2Int> spawn_tiles;
    private List<GameObject> spawned_kobalt = new List<GameObject>(0);

    public void BeginGeneration() {
      done_generating = false;
      spawn_tiles = gen_tools.get_average_tile_positions();
      if (gen == null)
        gen = new System.Random();
      create_kobalt_veins();
      filter_correct_kobalt();
      done_generating = true;
    }

    private void create_kobalt_veins() {
      int amount = Mathf.CeilToInt(spawn_tiles.Count * gen.Next(
        (int)(cave_profile.min_kobalt_percentage*100), (int)(cave_profile.max_kobalt_percentage*100))/100f);
      List<Vector2Int> pos = new List<Vector2Int>(spawn_tiles); int id;
      int sp_tiles = pos.Count;
      while(amount != 0) {
        id = gen.Next(0, sp_tiles);
        spawn_vein(pos[id], gen.Next(2, 6), gen.Next(3, 6));
        pos.RemoveAt(id);
        amount--;
        sp_tiles--;
      }
    }

    private void spawn_vein(Vector2 pos , int vein_amount, float vein_radious, float collision_dist = 0.8f) {
      Vector2[] spawned = new Vector2[vein_amount];
      int last_id = 0; Vector2 new_pos; int err;
      while(vein_amount != 0) {
        err = 50;
        do {
          new_pos = pos + (Vector2)(Quaternion.Euler(0, 0, 20 * gen.Next(0, 18)) * Vector2.up
            * Mathf.Pow(gen.Next(0, 100) / 100f,2) * vein_radious);
          err--;
        }
        while (!gen_tools.collision_check(new_pos , spawned, collision_dist, last_id) && err != 0);
        vein_amount--;
        if (err == 0)
          continue;
        create_kobalt(new_pos);
        spawned[last_id] = new_pos;
        last_id++;
      }
    }

    private void filter_correct_kobalt() {
      for(int i = spawned_kobalt.Count - 1; i >= 0; i--) {
        if (!gen_tools.check_position_valid(spawned_kobalt[i].transform.position)) {
          Destroy(spawned_kobalt[i]);
          spawned_kobalt.RemoveAt(i);
        }
      }
    }

    private void create_kobalt(Vector2 pos) {
      GameObject a = Instantiate(kobalt_pref, transform);
      a.transform.position = pos + Vector2.one * 0.5f;
      spawned_kobalt.Add(a);
      Cobalt.Ore ore = a.GetComponent<Cobalt.Ore>();
      ore.SetRichness(gen.Next(18,26));
    }
  }
}
