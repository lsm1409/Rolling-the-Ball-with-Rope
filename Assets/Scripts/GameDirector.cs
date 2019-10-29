using UnityEngine;

/// <summary>
/// Game director.
/// 게임의 총제적인 지휘
/// 
/// </summary>
///
public class GameDirector : MonoBehaviour
{
    public static int RespawnPoint;     // 리스폰
    public static bool isPaused;        // 게임 일시정지
    public static bool[] switches = new bool[3];
    public static bool[] doors = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        RespawnPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
