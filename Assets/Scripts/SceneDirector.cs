using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void TitleScene()
    {
        UIController.GameOver = false;
        GameDirector.isPaused = false;
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        SceneManager.LoadScene("Title");
    }

    public void StageSelect()
    {
        UIController.GameOver = false;
        GameDirector.isPaused = false;
        PlayerController.coinCount = 0;
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        SceneManager.LoadScene("SelectStage");
    }

    public void PlayStage0()
    {
        UIController.GameOver = false;
        GameDirector.isPaused = false;
        PlayerController.coinCount = 0;
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        //SceneManager.LoadScene("Stage#0");
        LoadingController.LoadScene("Stage#0");
    }

    public void PlayStage1()
    {
        UIController.GameOver = false;
        GameDirector.isPaused = false;
        PlayerController.coinCount = 0;
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        //SceneManager.LoadScene("Stage#1");
        LoadingController.LoadScene("Stage#1");
    }
}
