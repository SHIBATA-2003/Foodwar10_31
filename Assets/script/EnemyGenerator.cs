using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    Transform target;
    //エリアボス.
    private GameObject AreaBoss;
    //敵プレハブ
    [SerializeField] private GameObject enemyPrefab;
    //時間間隔の最小値
    [SerializeField] private float minTime;
    //時間間隔の最大値
    [SerializeField] private float maxTime;
    //X座標の最小値
    [SerializeField] private float xMinPosition;
    //X座標の最大値
    [SerializeField] private float xMaxPosition;
    //Y座標の最小値
    [SerializeField] private float yMinPosition;
    //Y座標の最大値
    [SerializeField] private float yMaxPosition;
    //Z座標の最小値
    [SerializeField] private float zMinPosition;
    //Z座標の最大値
    [SerializeField] private float zMaxPosition;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    private Vector3 mytransform;

    //一時停止.
    [SerializeField] private float Timezero = 1;

    //生成する数;
    public int enemy;
    //生成上限.
    public static int enemycount;

    [SerializeField] private string Object;
    [SerializeField] private int Areanumber=0;

    //難易度取得.
    private int gameD;
    private float GameD;

    void Start()
    {
        gameD=BackgroundCl.Gamedifficulty;
        GameD = gameD;
        mytransform = this.transform.position;
        Object = name;
        AreaNumber();
        //時間間隔を決定する
        minTime = 1.0f;
        maxTime = 1.0f;
        xMaxPosition = mytransform.x + 10.0f*GameD;
        xMinPosition = mytransform.x - 10.0f*GameD;
        yMaxPosition = mytransform.y + 1.0f;
        yMinPosition = mytransform.y;
        zMaxPosition = mytransform.z + 10.0f*GameD;
        zMinPosition = mytransform.z - 10.0f*GameD;

        interval = GetRandomTime();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        enemycount = 0;
        enemy = 5*gameD;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timezero == 1 && enemycount <= 150)
        {
            
            if (target.GetComponent<playerocnt>().invaded == true&&Areanumber==1)
            {
                //時間計測
                time += Time.deltaTime;

                //経過時間が生成時間になったとき(生成時間より大きくなったとき)
                if (time > interval)
                {
                    for (int i = 0; i < enemy; i++)
                    {
                        //enemyをインスタンス化する(生成する)
                        GameObject enemy = Instantiate(enemyPrefab);
                        //生成した敵の位置をランダムに設定する
                        enemy.transform.position = GetRandomPosition();

                        enemycount++;
                    }
                    //経過時間を初期化して再度時間計測を始める
                    time = 0f;
                    //次に発生する時間間隔を決定する
                    interval = GetRandomTime();
                }
            }
            else if (target.GetComponent<playerocnt>().invadedA == true && Areanumber == 2)
            {
                //時間計測
                time += Time.deltaTime;

                //経過時間が生成時間になったとき(生成時間より大きくなったとき)
                if (time > interval)
                {
                    for (int i = 0; i < enemy; i++)
                    {
                        //enemyをインスタンス化する(生成する)
                        GameObject enemy = Instantiate(enemyPrefab);
                        //生成した敵の位置をランダムに設定する
                        enemy.transform.position = GetRandomPosition();

                        enemycount++;
                    }
                    //経過時間を初期化して再度時間計測を始める
                    time = 0f;
                    //次に発生する時間間隔を決定する
                    interval = GetRandomTime();
                }
            }
            else if (target.GetComponent<playerocnt>().invadedB == true && Areanumber == 3)
            {
                //時間計測
                time += Time.deltaTime;

                //経過時間が生成時間になったとき(生成時間より大きくなったとき)
                if (time > interval)
                {
                    for (int i = 0; i < enemy; i++)
                    {
                        //enemyをインスタンス化する(生成する)
                        GameObject enemy = Instantiate(enemyPrefab);
                        //生成した敵の位置をランダムに設定する
                        enemy.transform.position = GetRandomPosition();

                        enemycount++;
                    }
                    //経過時間を初期化して再度時間計測を始める
                    time = 0f;
                    //次に発生する時間間隔を決定する
                    interval = GetRandomTime();
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("t"))
        {
            if (Timezero == 1)
            {
                Timezero = 0;
            }
            else
            {
                Timezero = 1;
            }
        }
    }
    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(minTime, maxTime);
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = UnityEngine.Random.Range(xMinPosition, xMaxPosition);
        float y = UnityEngine.Random.Range(yMinPosition, yMaxPosition);
        float z = UnityEngine.Random.Range(zMinPosition, zMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, y, z);
    }

    //エネミーのスクリプトで使用.
    public void AddCount(int count)
    {
        enemycount += count;
    }

    private void AreaNumber()
    {
        if (Object == "BossArea")
        {
            Areanumber = 1;
            AreaBoss = (GameObject)Resources.Load("BossEnemy");
            enemyPrefab= (GameObject)Resources.Load("Enemy10");
        }
        else if(Object == "CautionArea(A)")
        {
            Areanumber = 2;
            AreaBoss = (GameObject)Resources.Load("MediumEnemy(1)");
            enemyPrefab = (GameObject)Resources.Load("Enemy1");
        }
        else if(Object == "CautionArea(B)")
        {
            Areanumber= 3;
            AreaBoss = (GameObject)Resources.Load("MediumEnemy(2)");
            enemyPrefab = (GameObject)Resources.Load("Enemy2");
        }
        Instantiate(AreaBoss, new Vector3(mytransform.x,mytransform.y,mytransform.z), Quaternion.Euler(0.0f,0.0f,0.0f));
    }
}