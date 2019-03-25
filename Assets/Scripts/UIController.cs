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
    public Text Text;
    public static bool GameOver;
    public Image handle_move;
    public Image handle_rope;
    public Image finger_jump;
    public Image finger_touch;
    public Image tuto_move_back;
    public Image tuto_jump_back;
    public Image tuto_touch_back;
    public Image tuto_rope_back;
    public static bool tuto_move;
    public static bool tuto_jump;
    public static bool tuto_rope;
    public static bool tuto_touch;
    Vector3 tuto_move_posi1 = new Vector3(-515, -200, 0);
    Vector3 tuto_move_posi2 = new Vector3(-470, -200, 0);
    private int count_move = 0;
    private int count_jump = 0;
    private int count_rope = 0;
    private int count_touch = 0;

    // Start is called before the first frame update
    private void Start()
    {
        Text.enabled = false;
        handle_move.enabled = false;
        handle_rope.enabled = false;
        finger_jump.enabled = false;
        finger_touch.enabled = false;
        tuto_move_back.enabled = false;
        tuto_jump_back.enabled = false;
        tuto_rope_back.enabled = false;
        tuto_touch_back.enabled = false;
        tuto_move = true;
        tuto_rope = false;
        tuto_jump = false;
        tuto_touch = false;

    }

    // Update is called once per frame
    private void Update()
    {
        if (GameOver)
            Text.enabled = true;
        if (tuto_move)
        {
            Time.timeScale = 0;
            handle_move.enabled = true;
            tuto_move_back.enabled = true;
            Vector3 temp = handle_move.rectTransform.position;
            temp.x += 1;
            handle_move.rectTransform.position = temp;
            count_move++;
            if (count_move >= 45)
            {
                Vector3 temp1 = handle_move.rectTransform.parent.position;
                temp1.x -= 515;
                temp1.y -= 200;
                handle_move.rectTransform.position = temp1;
                count_move = 0;
            }
        }
        else if (tuto_jump)
        {
            Time.timeScale = 0;
            tuto_jump_back.enabled = true;
            count_jump++;
            if (count_jump >= 30)
            {
                if (finger_jump.enabled == true)
                    finger_jump.enabled = false;
                else
                    finger_jump.enabled = true;
                count_jump = 0;
            }
        }
        else if (tuto_rope)
        {
            //Time.timeScale = 0;
            handle_rope.enabled = true;
            tuto_rope_back.enabled = true;
            Vector3 temp = handle_rope.rectTransform.position;
            temp.x += 1;
            temp.y += 1;
            handle_rope.rectTransform.position = temp;
            count_rope++;
            if (count_rope >= 45)
            {
                Vector3 temp1 = handle_rope.rectTransform.parent.position;
                temp1.x += 485;
                temp1.y -= 200;
                handle_rope.rectTransform.position = temp1;
                count_rope = 0;
            }
        }
        else if (tuto_touch)
        {
            Time.timeScale = 0;
            tuto_touch_back.enabled = true;
            count_touch++;
            if (count_touch >= 30)
            {
                if (finger_touch.enabled == true)
                    finger_touch.enabled = false;
                else
                    finger_touch.enabled = true;
                count_touch = 0;
            }
        }
        else
        {
            Time.timeScale = 1;
            handle_move.enabled = false;
            handle_rope.enabled = false;
            finger_jump.enabled = false;
            finger_touch.enabled = false;
            tuto_move_back.enabled = false;
            tuto_jump_back.enabled = false;
            tuto_rope_back.enabled = false;
            tuto_touch_back.enabled = false;
        }
    }
}