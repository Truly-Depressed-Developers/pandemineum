using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioControler : MonoBehaviour
{
  [SerializeField] private AudioSource enemy_audio_source;
  [SerializeField] private AudioClip attack_sound;
  [SerializeField] private AudioClip agro_sound;
  [SerializeField] private AudioClip[] damage_sounds;

  // pamietajcie aby audio srouce byl osobnym obiektem i mial kordynat z = -7.5
  // jest wtedy blizej kamery i lepiej to brzmi

  public void PlayAttackSound() {
    enemy_audio_source.PlayOneShot(attack_sound);
  }

  public void PlayAgroSound() {
    enemy_audio_source.PlayOneShot(agro_sound);
  }

  public void PlayDamageSound() {
    AudioSource.PlayClipAtPoint(damage_sounds[Random.Range(0 , damage_sounds.Length)] , 
      enemy_audio_source.transform.position);
  }
}
