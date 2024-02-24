using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
  public class MapGenerator : MonoBehaviour {
    [HideInInspector] public bool done_generating = false;
    public GeneratorTools gen_tools;
    public CaveGenStats cave_profile;

    private Coroutine current_gen;
    public System.Random gen;

    public void BeginGeneration() {
      done_generating = false;
      current_gen = StartCoroutine(generate_layout());
    }

    private void SimpleCaveGenerator() {
      int tunnels = gen.Next(cave_profile.MinTunnels, cave_profile.MaxTunnels);
      int caverns = cave_profile.Caverns;

      Vector2 dir_to_zero;
      List<Vector3Int> joints = new List<Vector3Int>(0);
      Vector2Int pos = Vector2Int.zero;
      float angle = gen.Next(0, 360);
      int id;
      // snakes
      do {
        joints.AddRange(gen_tools.GenerateSnake(gen.Next(cave_profile.MinSnakeLength,
          cave_profile.MaxSnakeLength), pos, angle));
        id = gen.Next(0, joints.Count);
        pos = new Vector2Int(joints[id].x, joints[id].y);
        if (gen.Next(0, 2) == 1)
          angle = joints[id].z + 90f;
        else
          angle = joints[id].z - 90f;

        dir_to_zero = -pos;
        if (Vector2.Dot(dir_to_zero, angle * Vector2.up) < 0f)
          angle = joints[id].z + 180f;

        joints.RemoveAt(id);
        tunnels--;
      } while (tunnels != 0);

      int joint_amount = joints.Count;
      // caverns
      while (caverns > 0 && joint_amount > 0) {
        id = gen.Next(0, joint_amount);
        gen_tools.GenerateSphere(gen.Next(cave_profile.MinCavernSize, cave_profile.MaxCavernSize)
          , new Vector2Int(joints[id].x, joints[id].y));
        joints.RemoveAt(id);
        caverns--;
        joint_amount--;
      }
    }



    //  GENERATOR BEHAVIOUR
    public IEnumerator generate_layout() {
      // check if cave profile exists
      if (cave_profile == null) {
        Debug.LogWarning("generation canceled : reason - no cave profile provided");
        current_gen = null;
        yield break;
      }

      // generating layout
      while (true) { 
        gen_tools.clear_map();
        SimpleCaveGenerator();
        yield return null;

        if (is_generation_correct())
          break;
        else
          gen = gen_tools.make_gen(gen_tools.generate_seed_from_gen(gen));
      }

      current_gen = null;
      done_generating = true;
      yield break;
    }



    //  GENERATING SYSTEMS AND LOGIC

    private float calculate_average() {
      Bounds tile_b;
      tile_b = gen_tools.GroundTilemap.localBounds;
      return (tile_b.extents.x + tile_b.extents.y) / 2f;
    }

    private bool is_generation_correct() {
      float avg = calculate_average();
      if (avg < cave_profile.MinAverage || avg > cave_profile.MaxAverage)
        return false;
      return true;
    }
  }
}
