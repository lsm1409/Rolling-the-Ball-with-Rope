using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Controller.
/// Canvas 오브젝트에 적용
/// 게임 전반적인 UI를 관리하는 스크립트
/// 
/// * 현재는 Game Over 텍스트 출력만 담당
/// </summary>
/// 
/// 
public class UIController : MonoBehaviour
{
    public static bool GameOver;
    public Text time_txt;
    public Sprite get_coin;
    public Image coin1;
    public Image coin2;
    public Image coin3;
    public static int coin_count;
    private float time;
    int min = 0, sec = 0;

    // Start is called before the first frame update
    private void Start()
    {
        time = 0;
        coin_count = 0;
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
        if (coin_count == 1)
            coin1.GetComponent<Image>().sprite = get_coin;
        if (coin_count == 2)
            coin2.GetComponent<Image>().sprite = get_coin;
        if (coin_count == 3)
            coin3.GetComponent<Image>().sprite = get_coin;
    }
}