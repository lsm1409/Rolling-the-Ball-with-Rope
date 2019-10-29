using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
