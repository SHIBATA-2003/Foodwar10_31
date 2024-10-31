using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public Image setting;
    public Text text;
    private GameObject btnF;
    private GameObject btnW;

    private GameObject operation;

    private GameObject btnS;
    private GameObject btnO;


    private int count2;
    // Start is called before the first frame update
    void Start()
    {
        count2 = 0;
        btnF = GameObject.Find("SettingImage/ButtonF");
        btnW= GameObject.Find("SettingImage/ButtonW");
        setting.enabled = false;

        btnS = GameObject.Find("ButtonS");
        btnO= GameObject.Find("ButtonO");

        btnF.SetActive(false);
        btnW.SetActive(false);

        operation= GameObject.Find("Image");
        operation.SetActive(false);
    }

    public void SettingI()
    {
        if (count2==0)
        {
            count2=1;
            setting.enabled = true;
            btnF.SetActive(true);
            btnW.SetActive(true);
            btnO.SetActive(false);
            text.text = "もどる";
        }
        else
        {
            count2 = 0;
            setting.enabled = false;
            btnW.SetActive(false);
            btnF.SetActive(false);
            btnO.SetActive(true);
            text.text = "設定";
        }

    }
    public void OnClickFullScreenMode()
    {
        // フルスクリーンモードに切り替えます
        Screen.fullScreen = true;
    }

    public void OnClickWindowMode()
    {
        // ウィンドウモードに切り替えます
        Screen.fullScreen = false;
    }
    public void Operation()
    {
        if (count2 == 0)
        {
            count2 = 1;
            btnS.SetActive(false);
            operation.SetActive(true);

        }
        else if(count2 == 1)
        {
            count2 = 0;
            btnS.SetActive(true);
            operation.SetActive(false);

        }
    }
}
