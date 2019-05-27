using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OpenMenu()
    {
        GameDirector.isPaused = true;
        this.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        GameDirector.isPaused = false;
        this.gameObject.SetActive(false);
    }
}
