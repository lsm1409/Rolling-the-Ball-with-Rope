using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("Title");
        DontDestroyOnLoad(gameObject);
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("SelectStage");
        DontDestroyOnLoad(gameObject);
    }

    public void PlayStage0()
    {
        SceneManager.LoadScene("Stage#0");
        DontDestroyOnLoad(gameObject);
    }
}
