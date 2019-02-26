using UnityEngine;

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
            transform.position = isForward
                ? Vector3.Lerp(transform.position, Player.transform.position + cameraPositions[offsetNum], Time.deltaTime * CameraSpeed)
                : Vector3.Lerp(transform.position, Player.transform.position + cameraReversePositions[offsetNum], Time.deltaTime * CameraSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraRotations[offsetNum]), Time.deltaTime * CameraSpeed);
        }
        else
        {
            transform.RotateAround(Player.transform.position, Vector3.up, 90 * Time.deltaTime);
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