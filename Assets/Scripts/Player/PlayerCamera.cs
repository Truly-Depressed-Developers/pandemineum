using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
  public class PlayerCamera : MonoBehaviour {
    public Camera player_camera;
    [SerializeField] private Transform player;

    private float player_lerp_speed = 14f;
    private float max_lookout_dist = 7f;
    private Vector2 wishcamera_pos;
    private Vector2 mouse_pos, world_m_pos, outlook_dir;

    private void Update() {
      CalculateCameraWishPosition();
    }

    private void CalculateCameraWishPosition() {
      mouse_pos = Mouse.current.position.ReadValue();
      world_m_pos = player_camera.ScreenToWorldPoint(mouse_pos);
      outlook_dir = world_m_pos - (Vector2)player.position;
      wishcamera_pos = (Vector2)player.position + outlook_dir.normalized *
        (Mathf.Min(outlook_dir.magnitude, max_lookout_dist) / max_lookout_dist) * 0.65f;
      transform.position = Vector2.Lerp(transform.position, wishcamera_pos, Time.deltaTime * player_lerp_speed);
    }
  }
}
