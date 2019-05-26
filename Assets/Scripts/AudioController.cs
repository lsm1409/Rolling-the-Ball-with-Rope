using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;

    void Start()
    {
        if (!AudioListener.pause)
        {
            GetComponent<Image>().sprite = soundOn;
        }
        else if (AudioListener.pause)
        {
            GetComponent<Image>().sprite = soundOff;
        }
    }

    public void SoundOnOff()
    {
        if(AudioListener.pause)
        {
            AudioListener.pause = false;
            GetComponent<Image>().sprite = soundOn;
        }
        else if (!AudioListener.pause)
        {
            AudioListener.pause = true;
            GetComponent<Image>().sprite = soundOff;
        }
    }
}
