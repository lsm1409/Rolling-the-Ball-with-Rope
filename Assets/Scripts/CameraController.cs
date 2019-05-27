﻿using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float CameraSpeed;
    public float FieldOfView;
    public float MaxFieldOfView;
    public static int offsetNum;
    public static bool isForward;
    public static bool isChangingDirection;

    private Vector3[,] cameraPositions = { { new Vector3(4, 6, -20), new Vector3(-8, 2, 0), new Vector3(8, 6, 20), new Vector3(8, 2, 0) }, { new Vector3(-4, 6, -20), new Vector3(-8, 2, 0), new Vector3(8, 6, 20), new Vector3(8, 2, 0) } };
    private Vector3[] cameraRotations = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0) };
    private bool isStart;
    private int viewDirection;

    private void Start()
    {
        offsetNum = 0;
        isForward = true;
        isStart = true;

        if (isChangingDirection)
        {
            Invoke("SetToFalse", 3F);
        }
        Invoke("tutomove_start", 3F);
    }

    private void tutomove_start()
    {
        TutorialController.tuto_move = true;
    }

    private void LateUpdate()
    {
        viewDirection = isForward == true ? 0 : 1;

        if (isStart || isChangingDirection)
        {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position + cameraPositions[viewDirection, offsetNum], Time.deltaTime * CameraSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(cameraRotations[offsetNum]), Time.deltaTime * CameraSpeed);
        }
        //else if (UIController.GameOver)
        //{
        //    transform.RotateAround(Player.transform.position, Vector3.up, 90 * Time.deltaTime);
        //}
        else
        {
            transform.position = Player.transform.position + cameraPositions[viewDirection, offsetNum];
            transform.rotation = Quaternion.Euler(cameraRotations[offsetNum]);
        }

        if (Player.GetComponent<Rigidbody>().velocity.magnitude > (Player.GetComponent<PlayerController>().MaxVelocity * 0.95F))
        {
            // ... 카메라의 field of view를 넓힙니다.
            if (Camera.main.fieldOfView < MaxFieldOfView)
                Camera.main.fieldOfView += 0.1f;
        }
        else
        {
            // ... 그렇지 않으면 field of view를 다시 원래대로 줄입니다.
            if (Camera.main.fieldOfView > FieldOfView)
                Camera.main.fieldOfView -= 0.1f;
        }
    }

    private void SetToFalse()
    {
        isStart = false;
        isChangingDirection = false;
    }
}