using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cobalt {
  public class Ore : MonoBehaviour {
    public int Richness { get; private set; }

    public void SetRichness(int richness) {
      Richness = richness;
    }

    public void DropOre() {
      throw new NotImplementedException();
    }
  }
}
