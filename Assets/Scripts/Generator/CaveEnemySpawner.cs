using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
  public class CaveEnemySpawner : MonoBehaviour {
    [HideInInspector] public bool done_generating = false;
    public CaveGenStats cave_profile;
    public GeneratorTools gen_tools;
    public System.Random gen;

    [Header("Stuff to generate")]
    [Space(5)]
    public GameObject enemy_pref;

    private List<Vector2Int> spawn_tiles;
    private List<GameObject> spawned_enemies = new List<GameObject>(0);
    private List<Vector2> enemy_group_positions = new List<Vector2>(0);

    public void BeginGeneration() {
      done_generating = false;
      spawn_tiles = gen_tools.get_average_tile_positions();
      if (gen == null)
        gen = new System.Random();
      spawn_enemy_troops(gen.Next(cave_profile.enemy_troop_amount - 5 , cave_profile.enemy_troop_amount));
      filter_correct_enemies();
      done_generating = true;
    }

    private void spawn_enemy_troops(int amount) {
      int sp_tiles = spawn_tiles.Count; int id;
      int err = 30;
      while(amount != 0 && sp_tiles != 0) {
        id = gen.Next(0, sp_tiles);
        if (!check_troop_aviability(spawn_tiles[id])) {
          err--;
          if(err == 0) {
            err = 30; amount--;
          }
          continue;
        }
        spawn_enemy_group(spawn_tiles[id],
          gen.Next(cave_profile.min_enemy_per_troop, cave_profile.max_enemy_per_troop),
          cave_profile.troop_boundry_box_size);
        spawn_tiles.RemoveAt(id);
        amount--; sp_tiles--;
      }
    }

    private void spawn_enemy_group(Vector2 pos , int amount , float spawn_box_size = 8f, 
      float enemy_distance = 1.25f) {
      enemy_group_positions.Add(pos);
      Vector2[] spawned = new Vector2[amount]; int err;
      Vector2 new_pos = Vector2.zero; int last_id = 0;
      while(amount > 0) {
        err = 100;
        do {
          new_pos = pos + new Vector2(
            gen.Next((int)(-spawn_box_size * 100f / 2f), (int)(spawn_box_size * 100f / 2f)),
            gen.Next((int)(-spawn_box_size * 100f / 2f), (int)(spawn_box_size * 100f / 2f)))/100f;
          err--;
        }
        while (gen_tools.collision_check(new_pos, spawned, enemy_distance, last_id) && err != 0);
        amount--;
        if (err == 0)
          continue;
        spawn_enemy(new_pos);
        spawned[last_id] = new_pos;
        last_id++;
      }
    }

    private void spawn_enemy(Vector2 pos) {
      GameObject en = Instantiate(enemy_pref, transform);
      en.transform.position = pos;
      spawned_enemies.Add(en);
    }

    private void filter_correct_enemies() {
      for (int i = spawned_enemies.Count - 1; i >= 0; i--) {
        if (!gen_tools.check_position_valid(spawned_enemies[i].transform.position)) {
          Destroy(spawned_enemies[i]);
          spawned_enemies.RemoveAt(i);
        }
      }
    }

    private bool check_troop_aviability(Vector2 pos) {
      for(int i = 0; i < enemy_group_positions.Count; i++) {
        if (Vector2.Distance(pos, enemy_group_positions[i]) < cave_profile.troop_collision)
          return false;
      }
      return true;
    }
  }
}
