using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Vector3 normal;

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject;

        if (CompareTag("PortalBlue"))
        {
            Vector3 portalOrange = GameObject.FindWithTag("PortalOrange").transform.GetChild(0).position;
            player.transform.position = portalOrange;
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity.magnitude * normal.normalized;
            this.GetComponent<AudioSource>().Play();
        }

        if (CompareTag("PortalOrange"))
        {
            Vector3 portalBlue = GameObject.FindWithTag("PortalBlue").transform.GetChild(0).position;
            other.gameObject.transform.position = portalBlue;
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity.magnitude * normal.normalized;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
