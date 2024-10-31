using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;

public class EnemyAttackObject : MonoBehaviour
{
    private playerocnt Set;
    private int gameDifficulty;
    [SerializeField] private float damege;


    private float time;
    private bool timetrue=true;
    private bool Injectiontrue;

    [SerializeField] private float drawRay=30;
    [SerializeField] private GameObject landmark;
    private GameObject newThornpoint;
    Vector3 vector3;

    // Start is called before the first frame update
    void Start()
    {
        //Set= GameObject.FindGameObjectWithTag("Player").GetComponent<playerocnt>();
        gameDifficulty = BackgroundCl.Gamedifficulty;
        if (name == "Bullet")
        {
            if (gameDifficulty == 1)
            {
                damege = 5;
            }
            else if (gameDifficulty == 2)
            {
                damege = 10;
            }
            else if (gameDifficulty == 3)
            {
                damege = 30;
            }
        }
        if (name == "enemyweapon")
        {
            if (gameDifficulty == 1)
            {
                damege = 10;
            }
            else if (gameDifficulty == 2)
            {
                damege = 20;
            }
            else if (gameDifficulty == 3)
            {
                damege = 40;
            }
        }
        if(name== "Collision")
        {
            if (gameDifficulty == 1)
            {
                damege = 20;
            }
            else if (gameDifficulty == 2)
            {
                damege = 40;
            }
            else if (gameDifficulty == 3)
            {
                damege = 80;
            }
        }
        if (name == "Thorn(Clone)")
        {
            landmark=(GameObject)Resources.Load("Thornpoint");
            if (gameDifficulty == 1)
            {
                damege = 30;
            }
            else if (gameDifficulty == 2)
            {
                damege = 50;
            }
            else if (gameDifficulty == 3)
            {
                damege = 90;
            }

            //ray
            Vector3 rayPosition = transform.position;
            Ray ray = new Ray(rayPosition, Vector3.up);
            RaycastHit hit;
            bool isGround = Physics.Raycast(ray,out hit, drawRay);
            UnityEngine.Debug.DrawRay(rayPosition, Vector3.up * drawRay, Color.red);
            if (isGround)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    vector3 = this.transform.position;
                    UnityEngine.Debug.Log(vector3 + "‚Å’n–Ê‚Æ“–‚½‚Á‚½");
                    vector3.y = hit.transform.position.y+0.5f;//0.5‚È‚¢‚Æ’n–Ê‚É–„‚Ü‚Á‚Ä‚µ‚Ü‚¤.
                    newThornpoint = Instantiate(landmark, vector3, Quaternion.identity);
                    Injectiontrue = true;
                }
            }
        }
    }

    void Update()
    {
        if (Injectiontrue == true&&timetrue==true)
        {
            time += Time.deltaTime;
            if (time >= 2.0f)
            {
                Vector3 pos =this.transform.position;
                pos.y = vector3.y;
                this.transform.position=pos;
                Destroy(newThornpoint, 2.0f);
                Destroy(this.gameObject, 2.0f);
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("t"))
        {
            timetrue = !timetrue;
        }
    }

    //“–‚½‚Á‚½‚Æ‚«.
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerocnt.playerocntInstance.Damege(damege);
        }
    }
}
