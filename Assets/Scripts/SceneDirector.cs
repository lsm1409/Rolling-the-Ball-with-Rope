using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void TitleScene()
    {
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        SceneManager.LoadScene("Title");
    }

    public void StageSelect()
    {
        PlayerController.coinCount = 0;
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        SceneManager.LoadScene("SelectStage");
    }

    public void PlayStage0()
    {
        PlayerController.coinCount = 0;
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        SceneManager.LoadScene("Stage#0");
    }

    public void PlayStage1()
    {
        PlayerController.coinCount = 0;
        Destroy(GameObject.Find("Button Sound"), 0.3f);
        SceneManager.LoadScene("Stage#1");
    }
}
