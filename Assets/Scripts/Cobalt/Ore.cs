using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cobalt {
  public class Ore : MonoBehaviour {
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private GameObject breakParticle;
    [SerializeField] private AudioClip break_audio;
    public int Richness { get; private set; } = 50;

    public void SetRichness(int richness) {
      Richness = richness;
    }

    public void DropChunks() {
      List<int> chunkSizes = new();
      int richness = Richness;

      while (richness > 0) {
        int size = Mathf.Clamp(Random.Range(12, 29), 0, richness);
        richness -= size;
        chunkSizes.Add(size);
      }

      foreach (int size in chunkSizes) {
        GameObject chunk = Instantiate(chunkPrefab, transform.position, Quaternion.identity);

        if (!chunk.TryGetComponent(out Chunk c)) continue;
        c.SetRichness(size);
        c.transform.position += GenerateRandomOffset();
      }

      GameObject p = Instantiate(breakParticle);
      p.transform.position = transform.position;
      AudioSource.PlayClipAtPoint(break_audio, transform.position - Vector3.forward * 9f , 1f);
      Destroy(gameObject);
    }

    private Vector3 GenerateRandomOffset() {
      float RandomAngle = Random.Range(0f, 360f);
      float RandomDistance = Mathf.Sqrt(Random.Range(0f, 1f)) + Random.Range(0f, 0.1f);

      return Quaternion.AngleAxis(RandomAngle, Vector3.forward) * Vector3.up * RandomDistance;
    }
  }
}
