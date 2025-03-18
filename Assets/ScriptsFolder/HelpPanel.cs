using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    public GameObject Help; //Stores the Game Object Help Panel
    private bool isHelp; // bool variable to determine if the panel is showing or not
    void Start()
    {
        Help.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)&&isHelp==true)
        {
            CloseHelp();
        }
    }

    public void OpenHelp() //Help Button
    {
        Help.SetActive(true);
        isHelp = true;
    }
    public void CloseHelp() //Back Button
    {
        Help.SetActive(false);
        isHelp = false;
    }
}
