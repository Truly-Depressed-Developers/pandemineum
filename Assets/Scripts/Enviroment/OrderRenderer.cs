using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
  public class OrderRenderer : MonoBehaviour {
    [SerializeField] private bool realtime_updates = true;
    [SerializeField] private Transform ordering_anchor;
    [Space(5)]
    [SerializeField] private SpriteRenderer[] sprite_rend_array;
    [SerializeField] private int[] layer_offset;
    private int base_y , i;

    private void Start() {
      calculate_layers();
    }

    private void Update() {
      if(realtime_updates)
        calculate_layers();
    }

    private void calculate_layers() {
      if (sprite_rend_array == null || sprite_rend_array.Length == 0) return;
      base_y = -(int)(ordering_anchor.transform.position.y * 100);
      for(i = 0; i < sprite_rend_array.Length; i++) {
        sprite_rend_array[i].sortingOrder = base_y + layer_offset[i];
      }
    }
  }
}
