using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
  public class GenerationManager : MonoBehaviour {

    [HideInInspector] public bool done_generating = false;
    public string seed = "";
    private System.Random gen;
    [Space(10)]
    public CaveGenStats cave_profile;
    public MapGenerator map_generator;
    public CaveContentGenerator cave_content;
    public CaveEnemySpawner cave_enemies;
    public GeneratorTools gen_tools;

    private void Start() {
      BeginGeneration();
    }

    public void BeginGeneration() {
      gen = gen_tools.make_gen(seed);
      map_generator.gen = gen;
      map_generator.cave_profile = cave_profile;
      cave_enemies.gen = gen;
      cave_enemies.cave_profile = cave_profile;
      cave_content.gen = gen;
      gen_tools.level_gen = gen;

      StartCoroutine(generating_behaviour());
    }

    private IEnumerator generating_behaviour() {
      done_generating = false;

      // world layout generation...
      map_generator.BeginGeneration();
      while (!map_generator.done_generating)
        yield return null;

      // filling it with content...
      cave_content.calculate_kobalt_richness(100, 2128);
      cave_content.BeginGeneration();
      while (!cave_content.done_generating)
        yield return null;

      // filling it with dangers...
      cave_enemies.BeginGeneration();
      while (!cave_enemies.done_generating)
        yield return null;

      done_generating = true;
      yield break;
    }
  }
}
