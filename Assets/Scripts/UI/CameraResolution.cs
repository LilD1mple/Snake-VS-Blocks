using UnityEngine;

namespace SnakeVSBlocks.UI
{
    public class CameraResolution : MonoBehaviour
    {
        private Camera _mainCamera;

        private float _defaultResolution;

        private void Start()
        {
            _mainCamera = Camera.main;

            _defaultResolution = _mainCamera.aspect * _mainCamera.orthographicSize;
        }

        private void Update()
        {
            _mainCamera.orthographicSize = _defaultResolution / _mainCamera.aspect;
        }
    }
}
