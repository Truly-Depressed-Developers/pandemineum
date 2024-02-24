using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFade : MonoBehaviour {
  [HideInInspector] public Vector3[] points;
  [HideInInspector] public float fadeOutTime;
  private float creationTime;

  [SerializeField] private LineRenderer lr;
  private Renderer r;
  
  private float FadeProgress {
    get {
      return Mathf.Clamp(1f - ((Time.time - creationTime) / fadeOutTime), 0f, 1f);
    }
  }
  
  private void Start() {
    creationTime = Time.time;
    
    lr.positionCount = points.Length;
    lr.SetPositions(points);

    lr.TryGetComponent(out r);
    if(!r) Destroy(gameObject);
  }
  
  private void Update() {
    if (Mathf.Abs(FadeProgress) < Mathf.Epsilon) Destroy(gameObject);
    
    r.material.SetColor("_Color", new Color(1, 1, 1, FadeProgress));
  }
}
