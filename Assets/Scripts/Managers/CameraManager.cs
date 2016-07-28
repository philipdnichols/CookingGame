using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    #region Fields

    public float fastSpeedMultiplier = 2.0f;
    public float keyScrollSpeed = 2.5f;

    public float zoomSpeed = 20.0f;
    public float zoomMax = 3.0f;
    public float zoomMin = 25.0f;

    Camera mainCamera;
    Transform mainCameraTransform;

    Vector3 lastFramePosition;

    #endregion

    #region MonoBehaviour Implementations

    void Start() {
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;
	}
	
	void Update() {
        CheckKeyboardMovement(Time.deltaTime);
        CheckZoom(Time.deltaTime);
        CheckMouseMovement();
	}

    #endregion

    #region Helper Methods

    void CheckKeyboardMovement(float deltaTime) {
        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");

        bool leftShift = Input.GetKey(KeyCode.LeftShift);

        mainCameraTransform.Translate(
            translationX * keyScrollSpeed * (leftShift ? fastSpeedMultiplier : 1.0f) * mainCamera.orthographicSize * deltaTime, 
            translationY * keyScrollSpeed * (leftShift ? fastSpeedMultiplier : 1.0f) * mainCamera.orthographicSize * deltaTime, 
            0.0f);
    }

    void CheckZoom(float deltaTime) {
        mainCamera.orthographicSize -= mainCamera.orthographicSize * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * deltaTime;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, zoomMax, zoomMin);
    }

    void CheckMouseMovement() {
        Vector3 currFramePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Middle Mouse
        if (Input.GetMouseButton(2)) {
            Vector3 diff = lastFramePosition - currFramePosition;
            mainCameraTransform.Translate(diff);
        }

        lastFramePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    #endregion
}
