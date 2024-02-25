using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
  public class CaveStructureGenerator : MonoBehaviour {
    [HideInInspector] public bool done_generating = false;
    [SerializeField] private GameObject player_spawn_area;
    [SerializeField] private MapGenerator map_gen;
    [SerializeField] private GeneratorTools gen_tool;
    public System.Random gen;

    List<Vector2> caverns = new List<Vector2>(0);

    public void BeginGeneration() {
      done_generating = false;
      get_info();
      done_generating = true;
    }

    private void get_info() {
      caverns = new List<Vector2>(map_gen.cavern_position);
      if (gen == null)
        gen = new System.Random();
    }
  }
}
