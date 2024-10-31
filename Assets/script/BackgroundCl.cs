using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCl : MonoBehaviour
{
    public static int Gamedifficulty;

    public void SwitchButtonBackground(int buttonNumber)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == buttonNumber-1)
            {
                transform.GetChild(i).Find("Background").gameObject.SetActive(true);
                Gamedifficulty = i+1;
            }
            else
            {
                transform.GetChild(i).Find("Background").gameObject.SetActive(false);
            }
        }
    }
}
