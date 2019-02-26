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
    public GameObject Player;
    public float CameraSpeed;
    public float FieldOfView;
    public float MaxFieldOfView;
    public static int offsetNum;
    public static bool isForward;
    
    private Vector3[] cameraPositions = { new Vector3(4, 6, -20), new Vector3(-8, 2, 0), new Vector3(8, 6, 20), new Vector3(8, 2, 0) };
    private Vector3[] cameraReversePositions = { new Vector3(-4, 6, -20), new Vector3(-8, 2, 0), new Vector3(8, 6, 20), new Vector3(8, 2, 0) };
    private Vector3[] cameraRotations = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0) };


    private void Start()
    {
        offsetNum = 0;
        isForward = true;
    }

    private void FixedUpdate()
    {
        if (!UIController.GameOver)
        {
            Camera.main.transform.position = isForward
                ? Vector3.Lerp(Camera.main.transform.position, Player.transform.position + cameraPositions[offsetNum], Time.deltaTime * CameraSpeed)
                : Vector3.Lerp(Camera.main.transform.position, Player.transform.position + cameraReversePositions[offsetNum], Time.deltaTime * CameraSpeed);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(cameraRotations[offsetNum]), Time.deltaTime * CameraSpeed);
        }
        else
        {
            Camera.main.transform.RotateAround(Player.transform.position, Vector3.up, 90 * Time.deltaTime);
        }

        // 공의 속도가 빨라지면 카메라의 viewing field를 확장합니다.
        // 공의 속도가 최고 속도에 거의 다다르면 ... (최고 속도의 -0.5한 값보다 커지면 ...)
        if (Player.GetComponent<Rigidbody>().velocity.magnitude > (Player.GetComponent<PlayerController>().MaxVelocity - 0.5f))
        {
            // ... 카메라의 field of view를 넓힙니다.
            if (Camera.main.fieldOfView < MaxFieldOfView)
                Camera.main.fieldOfView += 0.5f;
        }
        else
        {
            // ... 그렇지 않으면 field of view를 다시 원래대로 줄입니다.
            if (Camera.main.fieldOfView > FieldOfView)
                Camera.main.fieldOfView -= 0.5f;
        }
    }
}