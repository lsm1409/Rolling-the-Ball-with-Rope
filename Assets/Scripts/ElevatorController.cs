using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float Speed, LowerHeight, UpperHeight;

    private bool isUp;

    private void FixedUpdate()
    {
        if (isUp && transform.parent.position.y < UpperHeight)
        {
            transform.parent.Translate(Vector3.up * Time.deltaTime * Speed);
        }
        else if (!isUp && transform.parent.position.y > LowerHeight)
        {
            transform.parent.Translate(Vector3.down * Time.deltaTime * Speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isUp = false;
        }
    }
}
