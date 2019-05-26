using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBGMController : MonoBehaviour
{
    private static GameObject instance;
    private string sceneName;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "SelectStage" && sceneName != "Title")
        { 
            Destroy(transform.gameObject);
        }
    }
}
