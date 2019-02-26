using UnityEngine;

/// <summary>
/// Camera controller.
/// Camera Rig 오브젝트에 적용
/// 
/// Camera Rig (부모)
///   - Main Camera (자식) * 필수요소
/// 
/// Main Camera가 달린 Camera Rig를 공 위치로 이동시키고 카메라의 위치를 조작하는 스크립트
/// </summary>
/// 
/// 
public class CameraController : MonoBehaviour
{
    public GameObject _ball; // 카메라가 쫒아다닐 오브젝트 공
    public float _moveSpeed = 10.0f; // 카메라가 움직이는 속도
    public float _fieldOfView = 50.0f; // 카메라의 기본 field of view
    public float _maxFieldOfView = 70.0f;   // 카메라의 최대 field of view
    public static int offsetNum;
    public static bool isForward;

    private Camera _camera;
    private Rigidbody _ballRigidbody;
    private Vector3[] cameraPositions = { new Vector3(4, 6, -20), new Vector3(-8, 2, 0), new Vector3(8, 6, 20), new Vector3(8, 2, 0) };
    private Vector3[] cameraReversePositions = { new Vector3(-4, 6, -20), new Vector3(-8, 2, 0), new Vector3(8, 6, 20), new Vector3(8, 2, 0) };
    private Vector3[] cameraRotations = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0) };


    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _ballRigidbody = _ball.GetComponent<Rigidbody>();
        _fieldOfView = _camera.fieldOfView;

        offsetNum = 0;
        isForward = true;
    }

    private void FixedUpdate()
    {
        if (!UIController.gameOver)
        {
            _camera.transform.position = isForward
                ? Vector3.Lerp(_camera.transform.position, _ball.transform.position + cameraPositions[offsetNum], Time.deltaTime * _moveSpeed)
                : Vector3.Lerp(_camera.transform.position, _ball.transform.position + cameraReversePositions[offsetNum], Time.deltaTime * _moveSpeed);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, Quaternion.Euler(cameraRotations[offsetNum]), Time.deltaTime * _moveSpeed);
        }
        else
        {
            _camera.transform.RotateAround(_ball.transform.position, Vector3.up, 90 * Time.deltaTime);
        }

        // 공의 속도가 빨라지면 카메라의 viewing field를 확장합니다.
        // 공의 속도가 최고 속도에 거의 다다르면 ... (최고 속도의 -0.5한 값보다 커지면 ...)
        if (_ballRigidbody.velocity.magnitude > (_ball.GetComponent<PlayerController>().MaxVelocity - 0.5f))
        {
            // ... 카메라의 field of view를 넓힙니다.
            if (_camera.fieldOfView < _maxFieldOfView)
                _camera.fieldOfView += 0.5f;
        }
        else
        {
            // ... 그렇지 않으면 field of view를 다시 원래대로 줄입니다.
            if (_camera.fieldOfView > _fieldOfView)
                _camera.fieldOfView -= 0.5f;
        }
    }
}