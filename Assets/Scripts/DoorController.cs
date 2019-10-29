using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int doorNum;
    public int switchNum;
    public float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.switches[switchNum] && !GameDirector.doors[doorNum])
        {
            StartCoroutine(Rotate(Vector3.forward * rotationAngle, 2.0f));
            GameDirector.doors[doorNum] = true;
            this.GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator Rotate(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}
