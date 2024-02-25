using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Additions {
  public class RandomPlacement : MonoBehaviour {
    [SerializeField] private Transform visuals;
    [SerializeField] private bool randomFlip = false;
    [SerializeField] private float min_rotation = 0;
    [SerializeField] private float max_rotation = 0;

    private void Start() {
      random_flip();
      random_rotation();
    }

    public void random_flip() {
      if (!randomFlip)
        return;
      if (Random.Range(0, 2) == 0)
        return;
      visuals.transform.localScale = new Vector3(-visuals.transform.localScale.x,
        visuals.transform.localScale.y,
        visuals.transform.localScale.z);
    }

    public void random_rotation() {
      if (min_rotation == max_rotation && min_rotation == 0)
        return;
      visuals.rotation = Quaternion.Euler(0, 0, Random.Range(min_rotation, max_rotation));
    }
  }
}
