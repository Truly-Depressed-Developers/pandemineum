using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Additions {
  public class RandomSprite : MonoBehaviour {
    public SpriteRenderer visuals;
    public Sprite[] sprite_array;
    public bool randomize_on_start = true;
    [HideInInspector] public int sprite_id = 0;

    private void Start() {
      if (randomize_on_start)
        pick_random();
    }

    public void pick_random() {
      sprite_id = Random.Range(0, sprite_array.Length);
      visuals.sprite = sprite_array[sprite_id];
    }

    public void set_sprite(int id) {
      if (id < 0 || id >= sprite_array.Length)
        return;
      sprite_id = id;
      visuals.sprite = sprite_array[id];
    }
  }
}
