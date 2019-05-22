using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CompareTag("PortalBlue"))
        {
            Debug.Log("this is blue");
            Vector3 portalOrange = GameObject.FindWithTag("PortalOrange").transform.GetChild(0).position;
            other.gameObject.transform.position = portalOrange;
        }

        if (other.gameObject.CompareTag("Player") && CompareTag("PortalOrange"))
        {
            Debug.Log("this is orange");
            Vector3 portalBlue = GameObject.FindWithTag("PortalBlue").transform.GetChild(0).position;
            other.gameObject.transform.position = portalBlue;
        }
    }
}
