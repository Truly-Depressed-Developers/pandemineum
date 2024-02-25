﻿using System.Collections;
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

    private int min_kobalt_richness = 20, max_kobalt_richenss = 30;
    private int ore_amount = 100, requested_ore_richness = 2000;

    // global read stats
    [HideInInspector] public int kobalt_spawned = 0;
    [HideInInspector] public int kobalt_richness = 0;

    public void BeginGeneration() {
      done_generating = false;
      kobalt_spawned = 0;
      kobalt_richness = 0;
      spawn_tiles = gen_tools.get_average_tile_positions();
      if (gen == null)
        gen = new System.Random();
      create_kobalt_veins();
      Debug.Log(kobalt_spawned);
      Debug.Log(kobalt_richness);
      done_generating = true;
    }

    private void create_kobalt_veins() {
      List<Vector2Int> pos = new List<Vector2Int>(spawn_tiles); int id;
      int sp_tiles = pos.Count;
      while(ore_amount > kobalt_spawned || requested_ore_richness > kobalt_richness) {
        id = gen.Next(0, sp_tiles);
        spawn_vein(pos[id], gen.Next(2, 6), gen.Next(3, 6));
        pos.RemoveAt(id);
        sp_tiles--;
      }
    }

    private void spawn_vein(Vector2 pos , int vein_amount, float vein_radious, float collision_dist = 0.8f) {
      Vector2[] spawned = new Vector2[vein_amount];
      List<GameObject> k_spawned = new List<GameObject>(0);
      int last_id = 0; Vector2 new_pos; int err; int richness_gained = 0;
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
        k_spawned.Add(create_kobalt(new_pos, ref richness_gained));
        spawned[last_id] = new_pos;
        last_id++;
      }

      int lost = filter_correct_kobalt(k_spawned);
      kobalt_spawned += k_spawned.Count;
      kobalt_richness += richness_gained - lost;
    }

    // returns the amount of richness that was lost
    private int filter_correct_kobalt(List<GameObject> cobalt_list) {
      int richness_loss = 0;
      for(int i = cobalt_list.Count - 1; i >= 0; i--) {
        if (!gen_tools.check_position_valid(cobalt_list[i].transform.position)) {
          richness_loss += cobalt_list[i].GetComponent<Cobalt.Ore>().Richness;
          Destroy(cobalt_list[i]);
          cobalt_list.RemoveAt(i);
        }
      }
      return richness_loss;
    }

    private GameObject create_kobalt(Vector2 pos , ref int richness) {
      GameObject a = Instantiate(kobalt_pref, transform);
      a.transform.position = pos + Vector2.one * 0.5f;
      Cobalt.Ore ore = a.GetComponent<Cobalt.Ore>();
      ore.SetRichness(gen.Next(min_kobalt_richness,max_kobalt_richenss));
      richness += ore.Richness;
      return a;
    }

    public float calculate_kobalt_richness(int kobalt_ore_amount , int kobalt_quota) {
      ore_amount = kobalt_ore_amount; requested_ore_richness = kobalt_quota;
      int avg_richness = Mathf.Max(1, kobalt_quota / kobalt_ore_amount);
      min_kobalt_richness = Mathf.Max(1, (int)(avg_richness * 0.7f));
      max_kobalt_richenss = (int)(avg_richness * 1.3);
      return avg_richness;
    }
  }
}
