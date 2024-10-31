using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Versioning;

public class DestroyVer2 : MonoBehaviour
{
    public static DestroyVer2 InstanceDestroy;

    [SerializeField] Transform playerTr; // プレイヤーのTransform
    [SerializeField] float speed; // 敵の動くスピード
    [SerializeField] private string Object;
    [SerializeField] private int ObjectNumber;

    [SerializeField] BoxCollider boxCol;

    //ボス識別.
    private int identification = 0;

    //MaxHPと現在値
    private int objectHP;

    //　敵の最大HP保存用
    private int hp;

    private ScoreManeger sm;
    private int scoreValue=1;  // これが敵を倒すと得られる点数になる

    private EnemyGenerator eg;
    private int enemyCount_generator = -1;
    private int onecount;

    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    //吹っ飛ぶ力
    float impulse = 30;

    //playercontで使う.
    private int SwitchA;
    private int SwitchB;

    //一時停止.
    [SerializeField] private float Timezero = 1;

    //Invokc.
    private float Recasttime;
    private float time;
    private bool timetrue;
    private bool attacktimetrue;

    //　HP表示用UI
    [SerializeField] private GameObject HPUI;
    private Text enemyHptext;
    public static int countHPBar=0;
    //　HP表示用スライダー
    [SerializeField] private Slider hpSlider;

    private int GameDifficulty;
    //行動.
    private int Actionvalue;
    private int Actionvaluemin;
    private int Actionvaluemax;

    private GameObject BossEnemyMini;

    //初期位置.
    private Vector3 mytransform;
    private bool Reset;

    //射出攻撃手段.
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float Objectspeed = 40f;

    //ｵﾌﾞｼﾞｪｸﾄ攻撃.
    [SerializeField] private GameObject enemyweapon;
    private Vector3 assaultposition;
    [SerializeField] private float Attackspeed;

    private BoxCollider weapponboxCol;

    void Awake()
    {
        Object = name;
        EnemyNunber();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        // 「ScoreManagerオブジェクト」に付いている「ScoreManagerスクリプト」の情報を取得して「sm」の箱に入れる。
        sm = GameObject.Find("ScoreManeger").GetComponent<ScoreManeger>();

        eg = GameObject.Find("CautionArea(A)").GetComponent<EnemyGenerator>();

        onecount = 0;

        time = 0;
        timetrue = false;
        attacktimetrue = false;

        boxCol = this.gameObject.GetComponent<BoxCollider>();

        speed = 10;

        Reset = false;
        mytransform = this.transform.position;

        playerTr =GameObject.FindGameObjectWithTag("Player").transform;

        Actionvaluemin = 1;
        Actionvaluemax = 101;
        Actionvalue = UnityEngine.Random.Range(Actionvaluemin, Actionvaluemax);

    }

    void Update()
    {
        if (Timezero == 1)
        {
            //ボスエリア.
            if (playerTr.GetComponent<playerocnt>().invaded == true && ObjectNumber == 10)
            {
                if (identification == 10)
                {
                    HPUI.SetActive(true);
                    enemyHptext.text = "ラスボス";
                    hpSlider.value = (float)objectHP / (float)hp;
                }
                Reset = true;
                // プレイヤーとの距離が○.○f未満になったらそれ以上実行しない
                if (Vector3.Distance(transform.position, playerTr.position) < 7.5f)
                {
                    if(timetrue == false && attacktimetrue == false)
                    {
                        if (Actionvalue <= 20)
                        {
                            Recasttime = 2.0f;
                            Invoke("RandomRest", Recasttime);
                            timetrue = true;
                        }
                        else if (Actionvalue <= 100)
                        {
                            Recasttime = 1.0f;
                            Invoke("EnemyAttack", Recasttime);
                            GetComponent<Renderer>().material.color = Color.red;
                            attacktimetrue = true;
                        }
                    }
                }
                else
                {
                    // プレイヤーに向けて進む
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        new Vector3(playerTr.position.x, playerTr.position.y, playerTr.position.z),
                     speed * Time.deltaTime);
                }
                Direction();
            }
            else if (playerTr.GetComponent<playerocnt>().invaded == false && ObjectNumber == 10 && Reset == true)
            {
                if (Vector3.Distance(transform.position, mytransform) < 2.0f)
                {
                    Reset = false;
                    return;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(
                      transform.position,
                      new Vector3(mytransform.x, mytransform.y, mytransform.z),
                      speed * Time.deltaTime);
                    DirectionReset();
                }
            }

            //エリアA.
            if (playerTr.GetComponent<playerocnt>().invadedA == true && ObjectNumber == 1)
            {
                if(SwitchA == 0&&identification==1)
                {
                    HPUI.SetActive(true);
                    enemyHptext.text = "小ボス";
                    hpSlider.value = (float)objectHP / (float)hp;
                }
                Reset =true;
                // プレイヤーとの距離が○.○f未満になったらそれ以上実行しない

                if (Vector3.Distance(transform.position, playerTr.position) < 7.0f)
                {
                    if (timetrue == false && attacktimetrue == false)
                    {
                        if (Actionvalue <= 20)
                        {
                            Recasttime = 2.0f;
                            Invoke("RandomRest", Recasttime);
                            timetrue = true;
                        }
                        else if (Actionvalue <= 100)
                        {
                            Recasttime = 1.0f;
                            Invoke("EnemyAttack", Recasttime);
                            GetComponent<Renderer>().material.color = Color.red;
                            attacktimetrue = true;
                        }
                    }
                }
                else
                {
                    // プレイヤーに向けて進む
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        new Vector3(playerTr.position.x, playerTr.position.y, playerTr.position.z),
                     speed * Time.deltaTime);
                }
                Direction();
            }
            else if (playerTr.GetComponent<playerocnt>().invadedA == false && ObjectNumber == 1 && Reset == true)
            {
                if (Vector3.Distance(transform.position, mytransform) < 2.0f)
                {
                    Reset = false;
                    return;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(
                      transform.position,
                      new Vector3(mytransform.x, mytransform.y, mytransform.z),
                      speed * Time.deltaTime);
                    DirectionReset();
                }
            }

            //エリアB.
            if (playerTr.GetComponent<playerocnt>().invadedB == true && ObjectNumber == 2)
            {
                if (SwitchB == 0&&identification==2)
                {
                    HPUI.SetActive(true);
                    enemyHptext.text = "小ボス";
                    hpSlider.value = (float)objectHP / (float)hp;
                }
                Reset = true;
                // プレイヤーとの距離が○.○f未満になったらそれ以上実行しない
                if (Vector3.Distance(transform.position, playerTr.position) < 7.0f)
                {
                    if (Actionvalue <= 20)
                    {
                        Recasttime = 2.0f;
                        Invoke("RandomRest", Recasttime);
                        Direction();
                        timetrue = true;
                    }
                    else if (Actionvalue <= 100)
                    {
                        Recasttime = 1.0f;
                        Invoke("EnemyAttack", Recasttime);
                        GetComponent<Renderer>().material.color = Color.red;
                        attacktimetrue = true;
                    }
                }
                else if(timetrue==false)
                {
                    // プレイヤーに向けて進む
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        new Vector3(playerTr.position.x, playerTr.position.y, playerTr.position.z),
                     speed * Time.deltaTime);
                    Direction();
                }
            }
            else if (playerTr.GetComponent<playerocnt>().invadedB == false && ObjectNumber == 2 && Reset == true)
            {
                if (Vector3.Distance(transform.position, mytransform) < 2.0f)
                {
                    Reset=false;
                    return;
                }
                else 
                {

                    transform.position = Vector3.MoveTowards(
                      transform.position,
                      new Vector3(mytransform.x, mytransform.y, mytransform.z),
                      speed * Time.deltaTime);
                    DirectionReset();
                }
            }

            if(identification >= 1&& playerTr.GetComponent<playerocnt>().invaded == false && playerTr.GetComponent<playerocnt>().invadedA == false && playerTr.GetComponent<playerocnt>().invadedB == false)
            {
                HPUI.SetActive(false);
            }
        }

        if (Timezero == 1 && timetrue == true||attacktimetrue==true)
        {
            time += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("t"))
        {
            if (Timezero == 1)
            {
                Timezero = 0;
                rigidBody.constraints = RigidbodyConstraints.FreezeAll;
                if (timetrue == true&&attacktimetrue==true)
                {
                    CancelInvoke();
                }

            }
            else
            {
                Timezero = 1;
                rigidBody.constraints = RigidbodyConstraints.None;
                rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                if (timetrue == true)
                {
                    Invoke("RandomRest", Recasttime-time);
                }
                if (attacktimetrue == true)
                {
                    Invoke("EnemyAttack", Recasttime-time);
                }
            }
        }
    }
    
    /// <summary>
    /// 衝突した時
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手にweaponタグが付いているとき
        if (collision.gameObject.name == "weapon")
        {
            int Attack = playerocnt.at;
            playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            objectHP = objectHP - Attack;
            
            if(identification>=1)
            {
                hpSlider.value = (float)objectHP / (float)hp;
            }

            if (objectHP <= 0)
            {

                if (identification == 1)
                {
                    SwitchA = 1;
                    HPUI.SetActive(false);
                }
                else if (identification == 2)
                {
                    SwitchB = 1;
                    HPUI.SetActive(false);
                }

                if (gameObject.tag == "BossEnemy")
                {
                    UnityEngine.Debug.Log("ゲームクリア");
                    SceneManager.LoadScene("GameClear");
                }
                //吹っ飛ばす
                boxCol.isTrigger = false;
                blowaway();

                if (onecount == 0)
                {
                    onecount = 1;

                    sm.AddScore(scoreValue);

                    eg.AddCount(enemyCount_generator);
                }
                Destroy(gameObject, 1.0f);
            }
        }
    }
    //
    void EnemyNunber()
    {
        //難易度取得HP.
        GameDifficulty = BackgroundCl.Gamedifficulty;

        if (Object == "BossEnemy(Clone)")
        {
            InstanceDestroy = this;

            GameObject child = transform.Find("Collision").gameObject;
            enemyweapon = child;
            weapponboxCol = enemyweapon.GetComponent<BoxCollider>();
            weapponboxCol.enabled = false;
            enemyweapon.SetActive(false);

            bullet= (GameObject)Resources.Load("Thorn");

            Attackspeed = 300.0f;

            ObjectNumber = 10;
            identification = 10;
            if (GameDifficulty == 1)
            {
                objectHP = 9000;
            }
            else if (GameDifficulty == 2)
            {
                objectHP = 18000;
            }
            else if (GameDifficulty == 3)
            {
                objectHP = 27000;
            }
        }
        else if (Object == "Enemy10(Clone)")
        {
            GameObject grandChild = transform.Find("Muzzle/FiringPoint").gameObject;
            firingPoint = grandChild;
            bullet = (GameObject)Resources.Load("Bullet");
            ObjectNumber = 10;
            if (GameDifficulty == 1)
            {
                objectHP = 150;
            }
            else if (GameDifficulty == 2)
            {
                objectHP = 250;
            }
            else if (GameDifficulty == 3)
            {
                objectHP = 350;
            }
        }
        else if (Object == "Enemy1(Clone)")
        {
            GameObject grandChild = transform.Find("Muzzle/FiringPoint").gameObject;
            firingPoint = grandChild;
            bullet = (GameObject)Resources.Load("Bullet");
            ObjectNumber = 1;
            if (GameDifficulty == 1)
            {
                objectHP = 150;
            }
            else if (GameDifficulty == 2)
            {
                objectHP = 250;
            }
            else if (GameDifficulty == 3)
            {
                objectHP = 350;
            }
        }
        else if (Object == "MediumEnemy(1)(Clone)")
        {
            GameObject child = transform.Find("Collision").gameObject;
            enemyweapon = child;
            weapponboxCol = enemyweapon.GetComponent<BoxCollider>();
            weapponboxCol.enabled = false;
            enemyweapon.SetActive(false);
            Attackspeed = 7.0f;

            ObjectNumber = 1;
            SwitchA = 0;
            identification = 1;
            if (GameDifficulty == 1)
            {
                objectHP = 7000;
            }
            else if (GameDifficulty == 2)
            {
                objectHP = 14000;
            }
            else if (GameDifficulty == 3)
            {
                objectHP = 21000;
            }
        }
        else if (Object == "Enemy2(Clone)")
        {
            GameObject child = transform.Find("enemyweapon").gameObject;
            enemyweapon = child;
            weapponboxCol=enemyweapon.GetComponent<BoxCollider>();
            weapponboxCol.enabled = false;
            enemyweapon.SetActive(false);
            Attackspeed = 5.0f;
            ObjectNumber = 2;
            if (GameDifficulty == 1)
            {
                objectHP = 150;
            }
            else if (GameDifficulty == 2)
            {
                objectHP = 250;
            }
            else if (GameDifficulty == 3)
            {
                objectHP = 350;
            }
        }
        else if (Object == "MediumEnemy(2)(Clone)")
        {
            GameObject child = transform.Find("Collision").gameObject;
            enemyweapon = child;
            weapponboxCol = enemyweapon.GetComponent<BoxCollider>();
            weapponboxCol.enabled = false;
            enemyweapon.SetActive(false);
            Attackspeed = 7.0f;

            ObjectNumber = 2;
            SwitchB = 0;
            identification = 2;
            if (GameDifficulty == 1)
            {
                objectHP = 7000;
            }
            else if (GameDifficulty == 2)
            {
                objectHP = 14000;
            }
            else if (GameDifficulty == 3)
            {
                objectHP = 21000;
            }
        }

        //ボスなら.
        if (identification >= 1)
        {
            HPUI = GameObject.Find("HPUI");
            enemyHptext = GameObject.Find("HPUI/Text").GetComponent<Text>();
            hp = objectHP;
            // HPをマックスに
            hpSlider = HPUI.transform.Find("MobeHPBar").GetComponent<Slider>();
            hpSlider.value = 1f;

            countHPBar++;
            BossEnemyMini = (GameObject)Resources.Load("MiniPlayer");
            Instantiate(BossEnemyMini, new Vector3(mytransform.x, mytransform.y, mytransform.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));

            if (GameDifficulty == 1&&countHPBar==3)
            {
                //UnityEngine.Debug.Log("難易度1 3体カウント完了");
                HPUI.SetActive(false);
                countHPBar = 0;
            }
            else if(GameDifficulty == 2 && countHPBar == 3)
            {
                //UnityEngine.Debug.Log("難易度2 3体カウント完了");
                HPUI.SetActive(false);
                countHPBar = 0;
            }
            else if (GameDifficulty == 3 && countHPBar == 3)
            {
                //UnityEngine.Debug.Log("難易度3 3体カウント完了");
                HPUI.SetActive(false);
                countHPBar = 0;
            }
        }
    }
    //吹っ飛ばし.
    void blowaway()
    {
        Vector3 playerVelocity = playerRigidBody.velocity;
        rigidBody.AddForce(playerVelocity * impulse, ForceMode.Impulse);
        if (playerTr.position.x > mytransform.x && playerTr.position.z > mytransform.z)
        {
            rigidBody.AddForce(Vector3.left * impulse, ForceMode.Impulse);
            rigidBody.AddForce(Vector3.back * impulse, ForceMode.Impulse);
        }
        else if (playerTr.position.x > mytransform.x && playerTr.position.z < mytransform.z)
        {
            rigidBody.AddForce(Vector3.left * impulse, ForceMode.Impulse);
            rigidBody.AddForce(Vector3.forward * impulse, ForceMode.Impulse);
        }

        else if (playerTr.position.x < mytransform.x && playerTr.position.z > mytransform.z)
        {
            rigidBody.AddForce(Vector3.right * impulse, ForceMode.Impulse);
            rigidBody.AddForce(Vector3.back * impulse, ForceMode.Impulse);
        }
        else if (playerTr.position.x < mytransform.x && playerTr.position.z < mytransform.z)
        {
            rigidBody.AddForce(Vector3.right * impulse, ForceMode.Impulse);
            rigidBody.AddForce(Vector3.forward * impulse, ForceMode.Impulse);
        }

        else if (playerTr.position.x == mytransform.x && playerTr.position.z > mytransform.z)
        {
            rigidBody.AddForce(Vector3.back * impulse, ForceMode.Impulse);
        }
        else if (playerTr.position.x == mytransform.x && playerTr.position.z < mytransform.z)
        {
            rigidBody.AddForce(Vector3.forward * impulse, ForceMode.Impulse);
        }

        else if (playerTr.position.x > mytransform.x && playerTr.position.z == mytransform.z)
        {
            rigidBody.AddForce(Vector3.left * impulse, ForceMode.Impulse);
        }
        else if (playerTr.position.x > mytransform.x && playerTr.position.z == mytransform.z)
        {
            rigidBody.AddForce(Vector3.right * impulse, ForceMode.Impulse);
        }
    }

    void Direction()
    {
        var direction=playerTr.transform.position-transform.position;
        direction.y = 0;

        var lookRotation= Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
    }

    void DirectionReset()
    {
        var direction = mytransform - transform.position;
        direction.y = 0;

        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
    }

    void EnemyAttack()
    {
        time = 0;
        timetrue = true;
        attacktimetrue = false;
        if (Object == "BossEnemy(Clone)")
        {
            if (Actionvalue<=60)
            {
                enemyweapon.SetActive(true);
                weapponboxCol.enabled = true;

                GetComponent<Rigidbody>().AddForce(this.transform.forward * Attackspeed, ForceMode.Impulse);
                Recasttime = 3.0f;
                Invoke("RandomRest", Recasttime);
            }
            else if (Actionvalue <= 100)
            {
                Vector3 Attckpoint = playerTr.position;

                float xMaxposition = Attckpoint.x + 15.0f;
                float xMinposition = Attckpoint.x - 15.0f;
                float zMaxposition = Attckpoint.z + 15.0f;
                float zMinposition = Attckpoint.z - 15.0f;

                for(int i = 0; i < 8*GameDifficulty; i++)
                {
                    float x = UnityEngine.Random.Range(xMinposition, xMaxposition);
                    float y = -15.0f;
                    float z = UnityEngine.Random.Range(zMinposition, zMaxposition);

                    Attckpoint = new Vector3(x, y, z);

                    GameObject newBullet = Instantiate(bullet, Attckpoint, Quaternion.identity);
                }

                Recasttime = 5.0f;
                Invoke("RandomRest", Recasttime);
            }
        }
        else if (Object == "Enemy10(Clone)")
        {
            Vector3 bulletPosition = firingPoint.transform.position;

            GameObject newBullet = Instantiate(bullet, bulletPosition, this.gameObject.transform.rotation);

            Vector3 direction = newBullet.transform.forward;

            newBullet.GetComponent<Rigidbody>().AddForce(direction * Objectspeed, ForceMode.Impulse);

            newBullet.name = bullet.name;

            Destroy(newBullet, 0.8f);
            Recasttime = 1.0f;
            Invoke("RandomRest", Recasttime);
        }
        else if (Object == "Enemy1(Clone)")
        {
            Vector3 bulletPosition = firingPoint.transform.position;

            GameObject newBullet = Instantiate(bullet, bulletPosition, this.gameObject.transform.rotation);

            Vector3 direction = newBullet.transform.forward;

            newBullet.GetComponent<Rigidbody>().AddForce(direction * Objectspeed, ForceMode.Impulse);

            newBullet.name = bullet.name;

            Destroy(newBullet, 0.8f);
            Recasttime = 1.0f;
            Invoke("RandomRest", Recasttime);
        }
        else if (Object == "MediumEnemy(1)(Clone)")
        {
            enemyweapon.SetActive(true);
            weapponboxCol.enabled = true;

            GetComponent<Rigidbody>().AddForce(this.transform.forward * Attackspeed, ForceMode.Impulse);

            Recasttime = 3.0f;
            Invoke("RandomRest", Recasttime);
        }
        else if (Object == "Enemy2(Clone)")
        {
            Attackspeed = 5.0f;
            enemyweapon.SetActive(true);
            weapponboxCol.enabled = true;

            assaultposition=playerTr.position;

            transform.position = Vector3.MoveTowards(transform.position, assaultposition, Attackspeed * Time.deltaTime*Timezero);
            this.transform.Rotate(0.0f * Time.deltaTime*Attackspeed, 180.0f * Time.deltaTime*Attackspeed, 0.0f * Time.deltaTime*Attackspeed);

            Recasttime = 3.0f;
            Invoke("RandomRest", Recasttime);
        }
        else if (Object == "MediumEnemy(2)(Clone)")
        {
            enemyweapon.SetActive(true);
            weapponboxCol.enabled = true;

            GetComponent<Rigidbody>().AddForce(this.transform.forward * Attackspeed, ForceMode.Impulse);

            Recasttime = 3.0f;
            Invoke("RandomRest", Recasttime);
        }
    }

    void RandomRest()
    {
        if (Object == "Enemy2(Clone)")
        {
            Attackspeed = 0.0f;
            transform.position = Vector3.MoveTowards(transform.position, assaultposition, Attackspeed * Time.deltaTime * Timezero);
            this.transform.Rotate(0.0f * Time.deltaTime * Attackspeed, 180.0f * Time.deltaTime * Attackspeed, 0.0f * Time.deltaTime * Attackspeed);

            weapponboxCol.enabled = false;
            enemyweapon.SetActive(false);
        }
        if (identification >= 1)
        {
            enemyweapon.SetActive(true);
            weapponboxCol.enabled = true;
        }

        Actionvalue = UnityEngine.Random.Range(Actionvaluemin, Actionvaluemax);
        GetComponent<Renderer>().material.color = Color.white;
        timetrue = false;
        time = 0;
    }

    public void Bosserase()
    {
        if(Object== "BossEnemy(Clone)")
        {
            this.gameObject.SetActive(false);
        }
    }
    public void Bosssummon()
    {
        if (Object == "BossEnemy(Clone)")
        {
            this.gameObject.SetActive(true);
        }
    }
}
