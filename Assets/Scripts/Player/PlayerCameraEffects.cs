using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Player {
  public class PlayerCameraEffects : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera cinemashine_camera;
    private Coroutine camera_shake;
    public void ShakeCamera(float amplitude = 0.18f , float frequency = 6, float time = 0.115f) {
      if (camera_shake != null)
        StopCoroutine(camera_shake);
      camera_shake = StartCoroutine(MakeScreenShake(amplitude , frequency, time));
    }

    private IEnumerator MakeScreenShake(float amplitude, float frequency, float time) {
      var cin = cinemashine_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
      cin.m_FrequencyGain = frequency;
      cin.m_AmplitudeGain = amplitude;
      yield return new WaitForSeconds(time);
      cin.m_FrequencyGain = 0;
      cin.m_AmplitudeGain = 0;
      camera_shake = null;
      yield break;
    }
  }
}
