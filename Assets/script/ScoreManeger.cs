using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger Instance;

    //消費モード
    private int count;

    public static int consumption;

    public static int score;
    public static int recovery;
    public static int attackup;
    public static float speedup;
    private Text scoreLabel;
    private Text reLabel;
    private Text atLabel;
    private Text spLabel;

    private GameObject rebg;
    private GameObject atbg;
    private GameObject spbg;
    
    private Image rebg_component;
    private Image atbg_component;
    private Image spbg_component;

    private Text Consumption;
    private Text Possible;
    private Image panelimage;
    [SerializeField] private bool panelimagetrue;

    private int food;

    //選択モード
    public bool all;
    public bool re;
    public bool at;
    public bool sp;

    //送るよう
    public static bool ComsumAll;
    public static bool ComsumRe ;
    public static bool ComsumAt ;
    public static bool ComsumSp ;

    private int failure;

    //カウントダウン
    [SerializeField] private float countdown;
    public static float counttimestatic;

    //一時停止.
    [SerializeField] private float Timezero = 1;

    private float time;

    //時間を表示するText型の変数
    public Text timeText;

    //ゲーム難易度取得用.
    private int GameDifficulty;

    //クエスト表示用.
    [SerializeField] private Text Questtext;
    //クエスト時間.
    [SerializeField] private Text Questtiem;
    private float questtime;
    //クエストtrue
    private bool Questtrue;
    private bool Queststart;

    //クエスト総数。
    private int start = 1;
    private int end = 10;
    //発生させるクエストの数.
    private int Questcount;

    private int index;
    private int ransu;

    List<int>Questnumber=new List<int>();

    //クエスト
    //倒す.
    private bool Knockdown;
    private int KnockDown;
    //目的地まで移動.
    private bool Destinationtrue;
    private GameObject destination;
    private GameObject DestinationObj;
    [SerializeField] private float xMaxposition;
    [SerializeField] private float xMinposition;
    [SerializeField] private float yposition;
    [SerializeField] private float zMaxposition;
    [SerializeField] private float zMinposition;


    //ステージサイズ
    [SerializeField] private float stagesize=1.0f;

    //
    [SerializeField] private Transform stage;
    [SerializeField] private Vector3 Stagescale;

    void Awake()
    {
        Instance = this;

        GameDifficulty = BackgroundCl.Gamedifficulty;
        //UnityEngine.Debug.Log(GameDifficulty);

        stage= GameObject.Find("StartingPoint").transform;
        Stagescale =stage.lossyScale;

        if (GameDifficulty == 1)
        {
            stagesize = 1.0f;
            Questcount = 3;
            recovery = 5;
            attackup = 5;
            speedup = 5;
        }
        else if(GameDifficulty == 2)
        {
            stagesize = 2.0f;
            Questcount = 4;
            recovery = 3;
            attackup = 3;
            speedup = 3;
        }
        else if( GameDifficulty == 3)
        {
            stagesize = 3.0f;
            Questcount = 5;
            recovery = 1;
            attackup = 1;
            speedup = 1;
        }
        Stagescale.x *= stagesize;
        Stagescale.z *= stagesize;

        //ステージ拡大.
        //ローカル（lossyは読み取り専用).
        stage.localScale = Stagescale;

        xMaxposition= GameObject.Find("position/xmaxposition").transform.position.x;
        xMinposition= GameObject.Find("position/xminposition").transform.position.x;
        yposition= GameObject.Find("position/yposition").transform.position.y;
        zMaxposition= GameObject.Find("position/zmaxposition").transform.position.z;
        zMinposition= GameObject.Find("position/zminposition").transform.position.z;

        destination = (GameObject)Resources.Load("Destination");

    }

    void Start()
    {
        score = 0;
        count = 1;
        consumption = 1;

        ComsumAll = false;
        ComsumRe = false;
        ComsumAt = false;
        ComsumSp = false;

        scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
        reLabel = GameObject.Find("Recovery/ReLabel").GetComponent<Text>();
        atLabel = GameObject.Find("AttackUP/AtLabel").GetComponent<Text>();
        spLabel = GameObject.Find("SpeedUP/SpLabel").GetComponent<Text>();

        Questtext = GameObject.Find("Quest_text").GetComponent<Text>();
        Questtiem = GameObject.Find("Quest_text/time").GetComponent<Text>();

        Possible = GameObject.Find("possible").GetComponent<Text>();
        Possible.enabled = false; 

        rebg = GameObject.Find("Recovery/Rebg");
        rebg_component = rebg.GetComponent<Image>();
        atbg = GameObject.Find("AttackUP/Atbg");
        atbg_component = atbg.GetComponent<Image>();
        spbg = GameObject.Find("SpeedUP/Spbg");
        spbg_component = spbg.GetComponent<Image>();

        panelimage = GameObject.Find("Panelimage").GetComponent<Image>();
        panelimage.GetComponent<RectTransform>().anchoredPosition =new Vector2(0.0f,100.0f);
        panelimagetrue = false;

        scoreLabel.text = "KILL：" + 0;
        reLabel.text = " :" + recovery;
        atLabel.text = " :" + attackup;
        spLabel.text = " :" + speedup;

        Consumption = GameObject.Find("cons").GetComponent<Text>();
        Consumption.text = "" + consumption;

        all = true;
        re = false;
        at = false;
        sp = false;
        
        failure = 0;

        countdown = 600.0f;
        counttimestatic=0.0f;

        time = 0;

        //
        Questtrue = false;
        Queststart = false;
        for(int i = start; i <= end; i++)
        {
            Questnumber.Add(i);
        }

        Questtiem.enabled = false;

        Questtext.text = "ミッションをクリアしながら\n自分を強化しよう\n残り"+Questcount+"個";
        index = UnityEngine.Random.Range(0, Questnumber.Count);
        ransu = Questnumber[index];
        Invoke("Questoccurs", 2);
        //  UnityEngine.Debug.Log(ransu);

        Questnumber.RemoveAt(index);
        Questcount--;

        possi();
    }

    void Update()
    {
        //時間をカウントダウンする
        countdown -= (Time.deltaTime * Timezero);
        counttimestatic += (Time.deltaTime * Timezero);

        //時間を表示する
        timeText.text = countdown.ToString("f1") + "秒";
        //ミニクエスト発生中.
        if (Queststart == true&&Questtrue==false)
        {
            questtime-= (Time.deltaTime * Timezero);
            Questtiem.text = questtime.ToString("f1");
            if(questtime<=0)
            {
                Questtext.text = "失敗";
                if (Knockdown == true)
                {
                    Knockdown = false;
                }
                if (Destinationtrue == true)
                {
                    Destination.InstanceDe.DestroyDestination();
                    Destinationtrue = false;
                }
                Questtrue = true;
                Queststart=false;
            }
        }


        if (panelimagetrue==true&&Timezero==1)
        {
            time += Time.deltaTime;
            panelimage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, panelimage.GetComponent<RectTransform>().anchoredPosition.y+(100.0f/6.0f)*Time.deltaTime);
        }

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    void FixedUpdate()
    {
        // モード.
        if (Input.GetKeyDown("j"))
        {
            count--;
            if (count == 0)
            {
                count = 4;
            }
            conUI();
            possi();

        }
        // モード.
        if (Input.GetKeyDown("l"))
        {
            count++;
            if (count == 5)
            {
                count = 1;
            }
            conUI();
            possi();
        }
        // 消費量.
        if (Input.GetKeyDown("i") &&consumption<99)
        {
            consumption++;
            Consumption.text = "" + consumption;
            possi();
        }
        // 消費量.
        if (Input.GetKeyDown("k")&&consumption>1)
        {
            consumption--;
            Consumption.text = "" + consumption;
            possi();
        }
        //一時停止.
        if (Input.GetKeyDown("t"))
        {
            if (Timezero == 1)
            {
                Timezero = 0;
                if (panelimagetrue == true||Questtrue==false)
                {
                    CancelInvoke();
                }
            }
            else
            {
                Timezero = 1;
                if (panelimagetrue == true)
                {
                    Invoke("ReSet", 6.0f - time);
                }

                if (Questtrue == false&&Queststart==false)
                {
                    UnityEngine.Debug.Log("呼びなおし");
                    if (Questcount > 0)
                    {
                        Invoke("Questoccurs", 2);
                    }
                    else
                    {
                        Invoke("Bosscall", 2);
                    }
                }
            }
        }

        //クエスト,ボス呼び.
        if (Questtrue==true)
        {
            Questtrue = false;
            UnityEngine.Debug.Log(Questcount);
            if(Questnumber.Count > 0&&Questcount>=0)
            {
                index = UnityEngine.Random.Range(0, Questnumber.Count);
                ransu = Questnumber[index];
                Invoke("Questoccurs", 2);

                Questnumber.RemoveAt(index);
            }
            else
            {
                Invoke("Bosscall",2);
            }
        }

    }

    // スコアを増加させるメソッド
    // 外部からアクセスするためpublicで定義する
    public void AddScore(int amount)
    {
        score += amount;
        scoreLabel.text = "KILL：" + score;
        if(Knockdown==true)
        {
            KnockDown -= amount;
            Questtext.text = "クエスト" + ransu + "\n残り" + KnockDown + "たおせ";
            if (KnockDown <= 0)
            {
                QuestClear();
            }
        }

        if (score % 20 == 0)
        {
            // Debug.Log("20人倒した");
            food = UnityEngine.Random.Range(1, 8);
            if(food <= 3)
            {
             //   Debug.Log("アミノ酸、ビタミンを手に入れた");
                recovery++;
                reLabel.text = "  :" + recovery;
            }
            else if(food <= 6)
            {
                // Debug.Log("腕力を手に入れ、力が増えた");
                attackup++;
                atLabel.text = "  :" + attackup;
            }
            else if (food <= 7)
            {
                // Debug.Log("脚力を手に入れ、足が速くなった");
                speedup++;
                spLabel.text = "  :" + speedup;
            }
        }
        if (score % 100 == 0)
        {
            recovery++;
            reLabel.text = "  :" + recovery;
            attackup++;
            atLabel.text = "  :" + attackup;
            speedup++;
            spLabel.text = "  :" + speedup;
        }
        possi();
    }
    
    void conUI()
    {
        //UnityEngine.Debug.Log("couUI呼ばれた");
        
        if(count == 1)
        {
            rebg_component.enabled= true;
            atbg_component.enabled = true;
            spbg_component.enabled = true;
            all = true;
            re = false;
            at = false;
            sp = false;
        }
        if(count == 2)
        {
            rebg_component.enabled = true;
            atbg_component.enabled = false;
            spbg_component.enabled = false;
            all = false;
            re = true;
            at = false;
            sp = false;
        }
        if(count == 3)
        {
            rebg_component.enabled = false;
            atbg_component.enabled = true;
            spbg_component.enabled = false;
            all = false;
            re = false;
            at = true;
            sp = false;
        }
        if (count == 4)
        {
            rebg_component.enabled = false;
            atbg_component.enabled = false;
            spbg_component.enabled = true;
            all = false;
            re = false;
            at = false;
            sp = true;
        }
    }
    //
    void possi()
    {
        Possible.enabled = false;
        ComsumAll = false;
        ComsumRe = false;
        ComsumAt = false;
        ComsumSp = false;
        if (recovery >= consumption && attackup >= consumption && speedup >= consumption && all == true)
        {
            Possible.enabled = true;
            ComsumAll = true;
        }
        if (recovery >= consumption && re == true)
        {
            Possible.enabled = true;
            ComsumRe = true;
        }
        if (attackup >= consumption && at == true)
        {
            Possible.enabled = true;
            ComsumAt = true;
        }
        if (speedup >= consumption&&sp==true)
        {
            Possible.enabled = true;
            ComsumSp = true;
        }
        if (failure >= 1)
        {
            Possible.enabled = false;
        }
    }

    public void Cooking()
    {
        panelimagetrue = true;
        panelimage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 0.0f);
        if (recovery >= consumption && attackup >= consumption && speedup >= consumption && all == true)
        {
            failure = 1;
            // UnityEngine.Debug.Log("すべて選択で食材が使われた");
            recovery = recovery - consumption;
            attackup = attackup - consumption;
            speedup = speedup - consumption;
            reLabel.text = "  :" + recovery;
            atLabel.text = "  :" + attackup;
            spLabel.text = "  :" + speedup;
        }
        if (recovery >= consumption && re == true)
        {
            failure = 1;
            //UnityEngine.Debug.Log("回復のみ選択で食材が使われた");
            recovery = recovery - consumption;
            reLabel.text = "  :" + recovery;
        }
        if (attackup >= consumption && at == true)
        {
            failure = 1;
            //UnityEngine.Debug.Log("攻撃アップのみで食材が使われた");
            attackup = attackup - consumption;
            atLabel.text = "  :" + attackup;
        }
        if (speedup >= consumption && sp == true)
        {
            failure = 1;
            //UnityEngine.Debug.Log("スピードアップで食材が使われた");
            speedup = speedup - consumption;
            spLabel.text = "  :" + speedup;
        }
        if (failure == 0)
        {
            UnityEngine.Debug.Log("料理失敗");
            panelimage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 100.0f);
            ReSet();
            return;
        }
        Possible.enabled = false;
        Invoke("ReSet", 6.0f);
    }
    //
    void ReSet()
    {
        Possible.enabled = true;
        panelimagetrue = false;
        time = 0;
        failure = 0;
        possi();
    }

    //クエスト呼び出し.
    void Questoccurs()
    {
        if (ransu == 1)
        {
            Knockdown = true;
            KnockDown = 30;
            Questtext.text = "クエスト" + ransu + "\n残り" + KnockDown + "たおせ\n残りミッション" + Questcount;
        }
        else if (ransu == 2)
        {
            Knockdown = true;
            KnockDown = 40;
            Questtext.text = "クエスト" + ransu + "\n残り" + KnockDown + "たおせ\n残りミッション" + Questcount;
        }
        else if (ransu == 3)
        {
            Knockdown = true;
            KnockDown = 50;
            Questtext.text = "クエスト" + ransu + "\n残り" + KnockDown + "たおせ\n残りミッション" + Questcount;
        }
        else if (ransu == 4)
        {
            Knockdown = true;
            KnockDown = 60;
            Questtext.text = "クエスト" + ransu + "\n残り" + KnockDown + "たおせ\n残りミッション" + Questcount;
        }
        else if (ransu == 5)
        {
            Knockdown = true;
            KnockDown = 70;
            Questtext.text = "クエスト" + ransu + "\n残り" + KnockDown + "たおせ\n残りミッション" + Questcount;
        }
        else if (ransu == 6)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "クエスト" + ransu + "\n目的地に行こう\n残りミッション" + Questcount;
        }
        else if (ransu == 7)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "クエスト" + ransu + "\n目的地に行こう\n残りミッション" + Questcount;
        }
        else if (ransu == 8)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "クエスト" + ransu + "\n目的地に行こう\n残りミッション" + Questcount;
        }
        else if (ransu == 9)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "クエスト" + ransu + "\n目的地に行こう\n残りミッション" + Questcount;
        }
        else if (ransu == 10)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "クエスト" + ransu + "\n目的地に行こう\n残りミッション" + Questcount;
        }
        Queststart = true;
        Questtiem.enabled = true;
        questtime = 100;
        Questcount--;
    }

    void Bosscall()
    {
        UnityEngine.Debug.Log("ミニクエスト終わり");
        Questtiem.enabled = false;
        Questtext.text = "ボスをたおそう";
        DestroyVer2.InstanceDestroy.Bosssummon();
        MinimapCh.instance.Summon();
    }

    public void QuestClear()
    {
        Questtext.text = "クエストクリア";
        Questtiem.enabled = false;
        Knockdown =false;
        Destinationtrue= false;
        Questtrue = true;
        Queststart = false;

        recovery +=3;
        attackup+=3;
        speedup+=3;
        reLabel.text = "  :" + recovery;
        atLabel.text = "  :" + attackup;
        spLabel.text = "  :" + speedup;
        possi();
    }
}