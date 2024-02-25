using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
  public class RandomAmbience : MonoBehaviour {
    public bool play_sounds = true;
    [SerializeField] private AudioSource sound_src;
    [SerializeField] private AudioClip[] ambienceSounds;
    [SerializeField] private float min_time_treshold = 8f;
    [SerializeField] private float max_time_threshold = 18f;
    [SerializeField] private float play_chance = 0.3f;
    private int last_played = -1 , id;

    private void Start() {
      if (ambienceSounds.Length != 0) StartCoroutine(sound_handling());
    }

    IEnumerator sound_handling() {
      while (true) {
        yield return new WaitForSeconds(Random.Range(min_time_treshold, max_time_threshold));
        if (!play_sounds) continue;
        if (Random.Range(0f, 1f) <= play_chance) {
          do
            id = Random.Range(0, ambienceSounds.Length);
          while (id == last_played && ambienceSounds.Length > 1);
          sound_src.PlayOneShot(ambienceSounds[id]);
          last_played = id;
        }
      }
    }
  }
}
