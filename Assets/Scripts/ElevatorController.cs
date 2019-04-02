using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float Speed, LowerHeight, UpperHeight;
    public AudioClip move;
    public AudioClip stop;

    private bool isUp;

    private void FixedUpdate()
    {
        if (isUp && transform.parent.position.y < UpperHeight)
        {
            transform.parent.Translate(Vector3.up * Time.deltaTime * Speed);
            if(this.transform.parent.position.y >= UpperHeight)
            {
                this.GetComponent<AudioSource>().clip = stop;
                this.GetComponent<AudioSource>().Play();
            }
        }
        else if (!isUp && transform.parent.position.y > LowerHeight)
        {
            transform.parent.Translate(Vector3.down * Time.deltaTime * Speed);
            if (this.transform.parent.position.y <= LowerHeight)
            {
                this.GetComponent<AudioSource>().clip = stop;
                this.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isUp = true;
            this.GetComponent<AudioSource>().clip = move;
            this.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isUp = false;
            this.GetComponent<AudioSource>().clip = move;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
