using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public int switchNum;

    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            GameDirector.switches[switchNum] = true;
        }
    }
    public bool getClick()
    {
        return isClicked;
    }
    public void setClick(bool a)
    {
        isClicked = a;
    }
}
