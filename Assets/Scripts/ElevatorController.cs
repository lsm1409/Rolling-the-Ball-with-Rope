using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float Speed, UpperHeight, LowerHeight;

    private bool isUp;

    private void Start()
    {
        isUp = true;
    }

    private void FixedUpdate()
    {
        if (isUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * Speed);
            isUp = transform.position.y < UpperHeight ? true : false;
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * Speed);
            isUp = transform.position.y < LowerHeight ? true : false;
        }
    }
}
