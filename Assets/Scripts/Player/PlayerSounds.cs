using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
  public class PlayerSounds : MonoBehaviour {
    [SerializeField] private AudioSource player_src;
    [SerializeField] private AudioClip player_damage_sound;
    [SerializeField] private AudioClip pickup_sound;
    [Space(5)]
    [SerializeField] private Movement p_move;
    [SerializeField] private AudioClip[] foot_steps; // only [0] and [1] works as left and right

    private void Start() {
      StartCoroutine(footstep_manager());
    }

    public void play_player_damage() {
      player_src.PlayOneShot(player_damage_sound);
    }

    public void play_player_pickup() {
      player_src.PlayOneShot(pickup_sound , 1.35f);
    }

    private IEnumerator footstep_manager() {
      float foot_step = 0.3f; int step = 1;
      float max_foot_step = 0.3f;
      while (true) {
        yield return null;

        if (p_move.direction == Vector2.zero) {
          foot_step = max_foot_step/3f;
          continue;
        }

        foot_step -= Time.deltaTime;
        if (foot_step <= 0) {
          if (step == 1)
            player_src.PlayOneShot(foot_steps[0],1.2f);
          else
            player_src.PlayOneShot(foot_steps[1], 1.2f);
          step *= -1;
          foot_step = max_foot_step;
        }
      }
    }
  }
}
