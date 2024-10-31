using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Collections.Specialized;
using UnityEngine;

public class carsol : MonoBehaviour
{
    private int nunber;
    // Start is called before the first frame update
    void Start()
    {
        nunber = 1;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("t"))
        {
            if(nunber == 0)
            {
                nunber=1;
                Cursor.lockState  = CursorLockMode.None; //標準モード
            }
            else if (nunber == 1)
            {
                nunber = 0;
                Cursor.lockState = CursorLockMode.Confined; //はみ出さないモード
            }
        }
    }
}
