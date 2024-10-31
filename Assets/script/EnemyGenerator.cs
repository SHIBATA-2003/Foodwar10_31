using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    Transform target;
    //�G���A�{�X.
    private GameObject AreaBoss;
    //�G�v���n�u
    [SerializeField] private GameObject enemyPrefab;
    //���ԊԊu�̍ŏ��l
    [SerializeField] private float minTime;
    //���ԊԊu�̍ő�l
    [SerializeField] private float maxTime;
    //X���W�̍ŏ��l
    [SerializeField] private float xMinPosition;
    //X���W�̍ő�l
    [SerializeField] private float xMaxPosition;
    //Y���W�̍ŏ��l
    [SerializeField] private float yMinPosition;
    //Y���W�̍ő�l
    [SerializeField] private float yMaxPosition;
    //Z���W�̍ŏ��l
    [SerializeField] private float zMinPosition;
    //Z���W�̍ő�l
    [SerializeField] private float zMaxPosition;
    //�G�������ԊԊu
    private float interval;
    //�o�ߎ���
    private float time = 0f;

    private Vector3 mytransform;

    //�ꎞ��~.
    [SerializeField] private float Timezero = 1;

    //�������鐔;
    public int enemy;
    //�������.
    public static int enemycount;

    [SerializeField] private string Object;
    [SerializeField] private int Areanumber=0;

    //��Փx�擾.
    private int gameD;
    private float GameD;

    void Start()
    {
        gameD=BackgroundCl.Gamedifficulty;
        GameD = gameD;
        mytransform = this.transform.position;
        Object = name;
        AreaNumber();
        //���ԊԊu�����肷��
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
                //���Ԍv��
                time += Time.deltaTime;

                //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
                if (time > interval)
                {
                    for (int i = 0; i < enemy; i++)
                    {
                        //enemy���C���X�^���X������(��������)
                        GameObject enemy = Instantiate(enemyPrefab);
                        //���������G�̈ʒu�������_���ɐݒ肷��
                        enemy.transform.position = GetRandomPosition();

                        enemycount++;
                    }
                    //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
                    time = 0f;
                    //���ɔ������鎞�ԊԊu�����肷��
                    interval = GetRandomTime();
                }
            }
            else if (target.GetComponent<playerocnt>().invadedA == true && Areanumber == 2)
            {
                //���Ԍv��
                time += Time.deltaTime;

                //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
                if (time > interval)
                {
                    for (int i = 0; i < enemy; i++)
                    {
                        //enemy���C���X�^���X������(��������)
                        GameObject enemy = Instantiate(enemyPrefab);
                        //���������G�̈ʒu�������_���ɐݒ肷��
                        enemy.transform.position = GetRandomPosition();

                        enemycount++;
                    }
                    //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
                    time = 0f;
                    //���ɔ������鎞�ԊԊu�����肷��
                    interval = GetRandomTime();
                }
            }
            else if (target.GetComponent<playerocnt>().invadedB == true && Areanumber == 3)
            {
                //���Ԍv��
                time += Time.deltaTime;

                //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
                if (time > interval)
                {
                    for (int i = 0; i < enemy; i++)
                    {
                        //enemy���C���X�^���X������(��������)
                        GameObject enemy = Instantiate(enemyPrefab);
                        //���������G�̈ʒu�������_���ɐݒ肷��
                        enemy.transform.position = GetRandomPosition();

                        enemycount++;
                    }
                    //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
                    time = 0f;
                    //���ɔ������鎞�ԊԊu�����肷��
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
    //�����_���Ȏ��Ԃ𐶐�����֐�
    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(minTime, maxTime);
    }

    //�����_���Ȉʒu�𐶐�����֐�
    private Vector3 GetRandomPosition()
    {
        //���ꂼ��̍��W�������_���ɐ�������
        float x = UnityEngine.Random.Range(xMinPosition, xMaxPosition);
        float y = UnityEngine.Random.Range(yMinPosition, yMaxPosition);
        float z = UnityEngine.Random.Range(zMinPosition, zMaxPosition);

        //Vector3�^��Position��Ԃ�
        return new Vector3(x, y, z);
    }

    //�G�l�~�[�̃X�N���v�g�Ŏg�p.
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