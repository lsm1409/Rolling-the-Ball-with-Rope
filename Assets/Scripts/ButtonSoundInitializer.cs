using UnityEngine;

public class ButtonSoundInitializer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
