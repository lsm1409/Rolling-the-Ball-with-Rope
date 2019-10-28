using UnityEngine;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{
    private int stageNumber = 0;
    //static string path = "Assets/data/player.txt";
    static string path = AppDirector.path + "/data/player.txt";
    static string line, temp;
    static int[,] record = new int[3, 4]; //순서대로 클리어 여부 별의 갯수와 시간 no클리어 0 클리어 1
    static int cnt;
    public Image st0_star1;
    public Image st0_star2;
    public Image st0_star3;
    public Image st1_star1;
    public Image st1_star2;
    public Image st1_star3;
    public Image st2_star1;
    public Image st2_star2;
    public Image st2_star3;
    public Image st0_lock;
    public Image st1_lock;
    public Image st2_lock;
    public Text st0_time;
    public Text st1_time;
    public Text st2_time;


    public static void recordUpdate()
    {
        System.IO.StreamReader file = new System.IO.StreamReader(@path);
        cnt = 0;
        while ((line = file.ReadLine()) != null)
        {
            temp = line.Substring(1, 1);
            record[cnt, 1] = int.Parse(temp);
            temp = line.Substring(2, 1);
            record[cnt, 2] = int.Parse(temp);
            temp = line.Substring(3);
            record[cnt, 3] = int.Parse(temp);
            cnt++;
        }
        file.Close();
    }

    private void Start()
    {
        recordUpdate();
        if (record[0, 1] == 1) st1_lock.gameObject.SetActive(false);
        if (record[1, 1] == 1) st2_lock.gameObject.SetActive(false);
        if (record[0, 2] >= 1) st0_star1.gameObject.SetActive(true);
        if (record[0, 2] >= 2) st0_star2.gameObject.SetActive(true);
        if (record[0, 2] == 3) st0_star3.gameObject.SetActive(true);
        if (record[1, 2] >= 1) st1_star1.gameObject.SetActive(true);
        if (record[1, 2] >= 2) st1_star2.gameObject.SetActive(true);
        if (record[1, 2] == 3) st1_star3.gameObject.SetActive(true);
        if (record[2, 2] >= 1) st2_star1.gameObject.SetActive(true);
        if (record[2, 2] >= 2) st2_star2.gameObject.SetActive(true);
        if (record[2, 2] == 3) st2_star3.gameObject.SetActive(true);
        if (record[0, 3] > 0)
        {
            int min = 0, sec = 0;
            min = record[0, 3] / 60;
            sec = record[0, 3] % 60;
            if (min < 10 && sec < 10)
                st0_time.text = "0" + min + ":0" + sec;
            else if (min >= 10 && sec < 10)
                st0_time.text = min + ":0" + sec;
            else if (min < 10 && sec >= 10)
                st0_time.text = "0" + min + ":" + sec;
            else
                st0_time.text = min + ":" + sec;
        }
        if (record[1, 3] > 0)
        {
            int min = 0, sec = 0;
            min = record[1, 3] / 60;
            sec = record[1, 3] % 60;
            if (min < 10 && sec < 10)
                st1_time.text = "0" + min + ":0" + sec;
            else if (min >= 10 && sec < 10)
                st1_time.text = min + ":0" + sec;
            else if (min < 10 && sec >= 10)
                st1_time.text = "0" + min + ":" + sec;
            else
                st1_time.text = min + ":" + sec;
        }
        if (record[2, 3] > 0)
        {
            int min = 0, sec = 0;
            sec = record[2, 3] % 60;
            min = record[2, 3] / 60;
            if (min < 10 && sec < 10)
                st2_time.text = "0" + min + ":0" + sec;
            else if (min >= 10 && sec < 10)
                st2_time.text = min + ":0" + sec;
            else if (min < 10 && sec >= 10)
                st2_time.text = "0" + min + ":" + sec;
            else
                st2_time.text = min + ":" + sec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(stageNumber)
        {
            case 0:
                transform.Find("Stage0").gameObject.SetActive(true);
                transform.Find("Stage1").gameObject.SetActive(false);
                transform.Find("Stage2").gameObject.SetActive(false);
                break;
            case 1:
                transform.Find("Stage0").gameObject.SetActive(false);
                transform.Find("Stage1").gameObject.SetActive(true);
                transform.Find("Stage2").gameObject.SetActive(false);
                break;
            case 2:
                transform.Find("Stage0").gameObject.SetActive(false);
                transform.Find("Stage1").gameObject.SetActive(false);
                transform.Find("Stage2").gameObject.SetActive(true);
                break;
        }   

    }

    public static int getCoin(int i)
    {
        return record[i, 2];
    }

    public static int getTime(int i)
    {
        return record[i, 3];
    }

    public static bool doStage(int n)
    {
        if (record[n - 1, 1] == 1) return true;
        else return false;
    }

    public void TabRightArrow()
    {
        if (stageNumber < 2)
            stageNumber++;
    }

    public void TabLeftArrow()
    {
        if (stageNumber > 0)
            stageNumber--;
    }
}
