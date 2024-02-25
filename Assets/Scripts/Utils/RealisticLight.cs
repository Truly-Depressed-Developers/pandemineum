using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utils {
  [RequireComponent(typeof(Light2D))]
  public class RealisticLight : MonoBehaviour {
    private Light2D light;
    private float randomOffset;
    private float initialLightIntensity;
    private float initialLightInner;
    private float initialLightOuter;

    [SerializeField] private AnimationCurve lightIntensityCurve;
    [SerializeField] private AnimationCurve lightInnerCurve;
    [SerializeField] private AnimationCurve lightOuterCurve;
    
    private void Start() {
      light = GetComponent<Light2D>();
      randomOffset = Random.Range(-15f, 15f);

      initialLightIntensity = light.intensity;
      initialLightInner = light.pointLightInnerRadius;
      initialLightOuter = light.pointLightOuterRadius;
    }

    private void Update() {
      float value = GetNoiseValue();

      light.intensity = initialLightIntensity * lightIntensityCurve.Evaluate(value);
      light.pointLightInnerRadius = initialLightInner * lightInnerCurve.Evaluate(value);
      light.pointLightOuterRadius = initialLightOuter * lightOuterCurve.Evaluate(value);
    }

    private float GetNoiseValue() {
      return Mathf.PerlinNoise1D(Time.time + randomOffset);
    }
  }
}
