using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour
{
  public bool play_music = true;
  [SerializeField] private AudioSource music_src;
  [SerializeField] private AudioClip[] music_playlist;
  private int last_played = -1;
  private int id;

  private void Start() {
    StartCoroutine(music_handler());
  }

  private IEnumerator music_handler() {
    while (true) {
      while (play_music) {
        if (music_src.clip == null || music_src.time + 0.01f > music_src.clip.length) {
          if (music_src.clip != null)
            yield return new WaitForSeconds(0.75f);
          else
            yield return new WaitForSeconds(0.2f);
          do {
            id = Random.Range(0, music_playlist.Length);
          }
          while (music_playlist.Length > 1 && id == last_played);
          music_src.clip = music_playlist[id];
          music_src.Play();
        }
        yield return null;
      }

      music_src.Stop();

      while (play_music) {
        yield return null;
      }
    }
  }

  public void set_volume(float volume) {
    music_src.volume = volume;
  }
}
