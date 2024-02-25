using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Utils;

public class SceneFader : MonoSingleton<SceneFader> {
  public Image fadeOutUIImage;
  public float fadeSpeed = 0.8f;

  public enum FadeDirection {
    In, //Alpha = 1
    Out // Alpha = 0
  }

  private void OnEnable() {
    StartCoroutine(Fade(FadeDirection.Out));
  }

  private IEnumerator Fade(FadeDirection fadeDirection) {
    float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
    float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
    if (fadeDirection == FadeDirection.Out) {
      while (alpha >= fadeEndValue) {
        SetColorImage(ref alpha, fadeDirection);
        yield return null;
      }

      fadeOutUIImage.enabled = false;
    } else {
      fadeOutUIImage.enabled = true;
      while (alpha <= fadeEndValue) {
        SetColorImage(ref alpha, fadeDirection);
        yield return null;
      }
    }
  }

  public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection) {
    yield return Fade(fadeDirection);
  }

  private void SetColorImage(ref float alpha, FadeDirection fadeDirection) {
    fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
    alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
  }
}
