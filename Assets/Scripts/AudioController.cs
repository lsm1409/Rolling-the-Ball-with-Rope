using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SelectStage")
        {
            DontDestroyOnLoad(transform.gameObject);
            //Destroy(transform.gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "Title")
        {
            DontDestroyOnLoad(transform.gameObject);
            //Destroy(transform.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
