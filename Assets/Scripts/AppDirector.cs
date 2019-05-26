using UnityEngine;

/// <summary>
/// App controller.
/// 모바일 환경에서 게임 화면 방향과 프레임레이트 지정을 위한 스크립트
/// </summary>
/// 
/// 
public class AppDirector : MonoBehaviour
{
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;   // 홈버튼이 오른쪽으로 오는 가로방향을 기본으로 설정
        // 가로방향 회전 허용,  세로방향 회전 불가
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        // 해상도 조절
        Screen.SetResolution(2560, 1440, true);

        // 프레임레이트를 60으로 설정
        Application.targetFrameRate = 60;
    }
}
