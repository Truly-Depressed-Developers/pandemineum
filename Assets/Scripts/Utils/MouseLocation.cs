using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils {
  public class MouseLocation : MonoSingleton<MouseLocation> {
    public Vector3 Position { get; private set; }
    private new Camera camera;

    private void Start() {
      TryGetComponent(out camera);

      if (!camera) {
        throw new Exception("Could not find camera component");
      }
    }

    private void FixedUpdate() {
      Position = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
  }
}
