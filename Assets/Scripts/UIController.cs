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
    public Text text;
    public static bool gameOver;

    // Start is called before the first frame update
    private void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameOver)
            text.enabled = true;
    }
}