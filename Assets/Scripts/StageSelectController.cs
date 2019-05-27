using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{

    private int stageNumber = 0;

    // Update is called once per frame
    void Update()
    {
        switch(stageNumber)
        {
            case 0:
                transform.Find("Stage0").gameObject.SetActive(true);
                transform.Find("Stage1").gameObject.SetActive(false);
                break;
            case 1:
                transform.Find("Stage0").gameObject.SetActive(false);
                transform.Find("Stage1").gameObject.SetActive(true);
                break;
        }   
    }

    public void TabRightArrow()
    {
        if (stageNumber < 1)
            stageNumber++;
    }

    public void TabLeftArrow()
    {
        if (stageNumber > 0)
            stageNumber--;
    }
}
