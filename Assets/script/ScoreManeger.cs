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

    //����[�h
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

    //�I�����[�h
    public bool all;
    public bool re;
    public bool at;
    public bool sp;

    //����悤
    public static bool ComsumAll;
    public static bool ComsumRe ;
    public static bool ComsumAt ;
    public static bool ComsumSp ;

    private int failure;

    //�J�E���g�_�E��
    [SerializeField] private float countdown;
    public static float counttimestatic;

    //�ꎞ��~.
    [SerializeField] private float Timezero = 1;

    private float time;

    //���Ԃ�\������Text�^�̕ϐ�
    public Text timeText;

    //�Q�[����Փx�擾�p.
    private int GameDifficulty;

    //�N�G�X�g�\���p.
    [SerializeField] private Text Questtext;
    //�N�G�X�g����.
    [SerializeField] private Text Questtiem;
    private float questtime;
    //�N�G�X�gtrue
    private bool Questtrue;
    private bool Queststart;

    //�N�G�X�g�����B
    private int start = 1;
    private int end = 10;
    //����������N�G�X�g�̐�.
    private int Questcount;

    private int index;
    private int ransu;

    List<int>Questnumber=new List<int>();

    //�N�G�X�g
    //�|��.
    private bool Knockdown;
    private int KnockDown;
    //�ړI�n�܂ňړ�.
    private bool Destinationtrue;
    private GameObject destination;
    private GameObject DestinationObj;
    [SerializeField] private float xMaxposition;
    [SerializeField] private float xMinposition;
    [SerializeField] private float yposition;
    [SerializeField] private float zMaxposition;
    [SerializeField] private float zMinposition;


    //�X�e�[�W�T�C�Y
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

        //�X�e�[�W�g��.
        //���[�J���ilossy�͓ǂݎ���p).
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

        scoreLabel.text = "KILL�F" + 0;
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

        Questtext.text = "�~�b�V�������N���A���Ȃ���\n�������������悤\n�c��"+Questcount+"��";
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
        //���Ԃ��J�E���g�_�E������
        countdown -= (Time.deltaTime * Timezero);
        counttimestatic += (Time.deltaTime * Timezero);

        //���Ԃ�\������
        timeText.text = countdown.ToString("f1") + "�b";
        //�~�j�N�G�X�g������.
        if (Queststart == true&&Questtrue==false)
        {
            questtime-= (Time.deltaTime * Timezero);
            Questtiem.text = questtime.ToString("f1");
            if(questtime<=0)
            {
                Questtext.text = "���s";
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

        //countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdown <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    void FixedUpdate()
    {
        // ���[�h.
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
        // ���[�h.
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
        // �����.
        if (Input.GetKeyDown("i") &&consumption<99)
        {
            consumption++;
            Consumption.text = "" + consumption;
            possi();
        }
        // �����.
        if (Input.GetKeyDown("k")&&consumption>1)
        {
            consumption--;
            Consumption.text = "" + consumption;
            possi();
        }
        //�ꎞ��~.
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
                    UnityEngine.Debug.Log("�ĂтȂ���");
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

        //�N�G�X�g,�{�X�Ă�.
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

    // �X�R�A�𑝉������郁�\�b�h
    // �O������A�N�Z�X���邽��public�Œ�`����
    public void AddScore(int amount)
    {
        score += amount;
        scoreLabel.text = "KILL�F" + score;
        if(Knockdown==true)
        {
            KnockDown -= amount;
            Questtext.text = "�N�G�X�g" + ransu + "\n�c��" + KnockDown + "������";
            if (KnockDown <= 0)
            {
                QuestClear();
            }
        }

        if (score % 20 == 0)
        {
            // Debug.Log("20�l�|����");
            food = UnityEngine.Random.Range(1, 8);
            if(food <= 3)
            {
             //   Debug.Log("�A�~�m�_�A�r�^�~������ɓ��ꂽ");
                recovery++;
                reLabel.text = "  :" + recovery;
            }
            else if(food <= 6)
            {
                // Debug.Log("�r�͂���ɓ���A�͂�������");
                attackup++;
                atLabel.text = "  :" + attackup;
            }
            else if (food <= 7)
            {
                // Debug.Log("�r�͂���ɓ���A���������Ȃ���");
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
        //UnityEngine.Debug.Log("couUI�Ă΂ꂽ");
        
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
            // UnityEngine.Debug.Log("���ׂđI���ŐH�ނ��g��ꂽ");
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
            //UnityEngine.Debug.Log("�񕜂̂ݑI���ŐH�ނ��g��ꂽ");
            recovery = recovery - consumption;
            reLabel.text = "  :" + recovery;
        }
        if (attackup >= consumption && at == true)
        {
            failure = 1;
            //UnityEngine.Debug.Log("�U���A�b�v�݂̂ŐH�ނ��g��ꂽ");
            attackup = attackup - consumption;
            atLabel.text = "  :" + attackup;
        }
        if (speedup >= consumption && sp == true)
        {
            failure = 1;
            //UnityEngine.Debug.Log("�X�s�[�h�A�b�v�ŐH�ނ��g��ꂽ");
            speedup = speedup - consumption;
            spLabel.text = "  :" + speedup;
        }
        if (failure == 0)
        {
            UnityEngine.Debug.Log("�������s");
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

    //�N�G�X�g�Ăяo��.
    void Questoccurs()
    {
        if (ransu == 1)
        {
            Knockdown = true;
            KnockDown = 30;
            Questtext.text = "�N�G�X�g" + ransu + "\n�c��" + KnockDown + "������\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 2)
        {
            Knockdown = true;
            KnockDown = 40;
            Questtext.text = "�N�G�X�g" + ransu + "\n�c��" + KnockDown + "������\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 3)
        {
            Knockdown = true;
            KnockDown = 50;
            Questtext.text = "�N�G�X�g" + ransu + "\n�c��" + KnockDown + "������\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 4)
        {
            Knockdown = true;
            KnockDown = 60;
            Questtext.text = "�N�G�X�g" + ransu + "\n�c��" + KnockDown + "������\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 5)
        {
            Knockdown = true;
            KnockDown = 70;
            Questtext.text = "�N�G�X�g" + ransu + "\n�c��" + KnockDown + "������\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 6)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "�N�G�X�g" + ransu + "\n�ړI�n�ɍs����\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 7)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "�N�G�X�g" + ransu + "\n�ړI�n�ɍs����\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 8)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "�N�G�X�g" + ransu + "\n�ړI�n�ɍs����\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 9)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "�N�G�X�g" + ransu + "\n�ړI�n�ɍs����\n�c��~�b�V����" + Questcount;
        }
        else if (ransu == 10)
        {
            Destinationtrue = true;
            float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
            float y = yposition;
            float z = UnityEngine.Random.Range(zMinposition, zMaxposition);
            DestinationObj = Instantiate(destination);
            DestinationObj.transform.position = new Vector3(x, y, z);

            Questtext.text = "�N�G�X�g" + ransu + "\n�ړI�n�ɍs����\n�c��~�b�V����" + Questcount;
        }
        Queststart = true;
        Questtiem.enabled = true;
        questtime = 100;
        Questcount--;
    }

    void Bosscall()
    {
        UnityEngine.Debug.Log("�~�j�N�G�X�g�I���");
        Questtiem.enabled = false;
        Questtext.text = "�{�X����������";
        DestroyVer2.InstanceDestroy.Bosssummon();
        MinimapCh.instance.Summon();
    }

    public void QuestClear()
    {
        Questtext.text = "�N�G�X�g�N���A";
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