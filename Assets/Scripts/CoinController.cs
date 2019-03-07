using UnityEngine;

/// <summary>
/// Coin controller.
/// 게임 속 Coin을 y축 회전시키는 스크립트
/// </summary>
/// 
/// 
public class CoinController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Time.deltaTime * 45, 0, 0);
    }
}
