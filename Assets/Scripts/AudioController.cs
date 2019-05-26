using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    static bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        if (isFirst)
        {
            this.GetComponent<AudioSource>().Play();
            isFirst = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SelectStage")
        {
            DontDestroyOnLoad(transform.gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "Title")
        {
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    public void SoundOnOff()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
