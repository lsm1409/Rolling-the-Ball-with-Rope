using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void TitleScene()
    {
        Destroy(GameObject.Find("Button Sound"), 0.5f);
        SceneManager.LoadScene("Title");
    }

    public void StageSelect()
    {
        Destroy(GameObject.Find("Button Sound"), 0.5f);
        SceneManager.LoadScene("SelectStage");
    }

    public void PlayStage0()
    {
        Destroy(GameObject.Find("Button Sound"), 0.5f);
        SceneManager.LoadScene("Stage#0");
    }
}
