using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MinimapCh : MonoBehaviour
{
    public static MinimapCh instance;
     
    private Transform player;
    public static int minimapnamber;

    //識別;
    private int mynamber;

    // Start is called before the first frame update
    void Start()
    {
        ObjectChecker();
    }

    void ObjectChecker()
    {
        minimapnamber++;
        mynamber = minimapnamber;
        int GameDifficulty = BackgroundCl.Gamedifficulty;
        float Magnificationrate =(float)GameDifficulty;

        //ミニマップ,ステージ拡大に合わせて.
        Vector3 minimapplayer = this.transform.lossyScale;
        minimapplayer.x = Magnificationrate;
        minimapplayer.y = Magnificationrate;
        minimapplayer.z = Magnificationrate;

        this.transform.Rotate(90.0f, 0.0f, 0.0f);
        this.transform.localScale = minimapplayer;
        if (minimapnamber == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else if (minimapnamber == 2)
        {
            player = GameObject.Find("BossEnemy(Clone)").transform;
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            instance = this;
            this.gameObject.SetActive(false);
            DestroyVer2.InstanceDestroy.Bosserase();

        }
        else if (minimapnamber == 3)
        {
            player = GameObject.Find("MediumEnemy(1)(Clone)").transform;
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
        else if (minimapnamber == 4)
        {
            player = GameObject.Find("MediumEnemy(2)(Clone)").transform;
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            minimapnamber = 0;
        }
        this.transform.position=player.position;
        transform.parent = player;
    }

    public void Summon()
    {
        if (mynamber == 2)
        {
            this.gameObject.SetActive(true);
        }
    }
}
