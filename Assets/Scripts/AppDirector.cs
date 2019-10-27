using System.IO;
using UnityEngine;

/// <summary>
/// App controller.
/// 모바일 환경 관리를 위한 스크립트
/// </summary>
/// 
/// 
public class AppDirector : MonoBehaviour
{
    public static string path;
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

        //운영체제 별 파일경로
        path = Application.persistentDataPath;
        //폴더가 없다면 생성
        if (!Directory.Exists(path + "/data"))
        {
            Directory.CreateDirectory(path + "/data");
            Debug.Log("폴더 생성 : " + path);
        }
        //파일이 없다면 생성
        if (!File.Exists(path + "/data/player.txt"))
        {
            StreamWriter textWrite = File.CreateText(path + "/data/player.txt");
            textWrite.WriteLine("0000");
            textWrite.WriteLine("1000");
            textWrite.WriteLine("2000");
            textWrite.Dispose();
            Debug.Log("파일 생성 : " + path);
        }
    }
}
