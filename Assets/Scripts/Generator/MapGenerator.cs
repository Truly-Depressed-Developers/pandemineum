using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
  public class MapGenerator : MonoBehaviour {
    public string seed = "";
    public GeneratorTools gen_tools;
    public CaveGenStats CaveProfile;

    private System.Random gen;

    private void Start() {
      //make_gen();
      //SimpleCaveGenerator();
      StartCoroutine(gen_tester());
    }

    IEnumerator gen_tester() {
      if (CaveProfile == null)
        yield break;

      seed = "";
      float avg_size;
      Bounds tile_b;
      while (true) {
        gen_tools.GroundTilemap.ClearAllTiles();
        make_gen();
        SimpleCaveGenerator();
        tile_b = gen_tools.GroundTilemap.localBounds;
        avg_size = (tile_b.extents.x + tile_b.extents.y) / 2f;
        if (avg_size < CaveProfile.MinAverage || avg_size > CaveProfile.MaxAverage)
          yield return null;
        else
          yield return new WaitForSeconds(1);
      }
    }

    private void SimpleCaveGenerator() {
      int tunnels = gen.Next(CaveProfile.MinTunnels, CaveProfile.MaxTunnels);
      int caverns = CaveProfile.Caverns;

      Vector2 dir_to_zero;
      List<Vector3Int> joints = new List<Vector3Int>(0);
      Vector2Int pos = Vector2Int.zero;
      float angle = gen.Next(0, 360);
      int id;
      // snakes
      do {
        joints.AddRange(gen_tools.GenerateSnake(gen.Next(CaveProfile.MinSnakeLength,
          CaveProfile.MaxSnakeLength), pos, angle));
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
        gen_tools.GenerateSphere(gen.Next(CaveProfile.MinCavernSize, CaveProfile.MaxCavernSize)
          , new Vector2Int(joints[id].x, joints[id].y));
        joints.RemoveAt(id);
        caverns--;
        joint_amount--;
      }
    }

    // creates rn-gen instance
    private void make_gen() {
      if (seed == "")
        gen = new System.Random(gen_tools.generate_seed().GetHashCode());
      else
        gen = new System.Random(seed.GetHashCode());
      gen_tools.level_gen = gen;
    }
  }
}
