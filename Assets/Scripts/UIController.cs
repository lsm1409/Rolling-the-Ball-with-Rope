using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Controller.
/// 게임 전반적인 UI를 관리하는 스크립트
/// 
/// </summary>
/// 
/// 
public class UIController : MonoBehaviour
{
    public static bool GameOver;
    public Canvas ClearCanvas;
    public Text time_txt;
    public Sprite get_coin;
    public Image coin1;
    public Image coin2;
    public Image coin3;
    private float time;
    int min = 0, sec = 0;

    // Start is called before the first frame update
    private void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime;
        min = (int)(time / 60);
        sec = (int)time % 60;
        if(min < 10 && sec < 10)
            time_txt.text = "0" + min + " : 0" + sec;
        else if(min >= 10 && sec < 10)
            time_txt.text = min + " : 0" + sec;
        else if(min < 10 && sec >= 10)
            time_txt.text = "0" + min + " : " + sec;
        else
            time_txt.text = min + " : " + sec;
        if (PlayerController.coinCount == 1)
            coin1.GetComponent<Image>().sprite = get_coin;
        if (PlayerController.coinCount == 2)
            coin2.GetComponent<Image>().sprite = get_coin;
        if (PlayerController.coinCount == 3)
            coin3.GetComponent<Image>().sprite = get_coin;

        if (GameOver)
        {
            GameDirector.isPaused = true;
            ClearCanvas.gameObject.SetActive(true);
            ClearCanvas.GetComponentInChildren<Text>().text = time_txt.text;
            for (int i = 1; i <= PlayerController.coinCount; i++)
            {
                ClearCanvas.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}