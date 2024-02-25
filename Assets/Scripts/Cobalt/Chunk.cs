using System;
using System.Collections;
using System.Collections.Generic;
using Pickups;
using UnityEngine;

namespace Cobalt {
  public class Chunk : MonoBehaviour {
    public int Richness { get; private set; }

    public void SetRichness(int richness) {
      Richness = richness;
    }
  }
}
