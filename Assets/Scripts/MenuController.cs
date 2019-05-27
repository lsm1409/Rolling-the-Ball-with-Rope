using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static bool isOpendMenu = false;

    private void Update()
    {
        if (isOpendMenu)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void OpenMenu()
    {
        isOpendMenu = true;
        this.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        isOpendMenu = false;
        this.gameObject.SetActive(false);
    }
}
