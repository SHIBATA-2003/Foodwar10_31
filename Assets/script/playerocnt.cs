using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics.Contracts;
using System;

public class playerocnt : MonoBehaviour
{
    public static playerocnt playerocntInstance;

    private int countE;

    private bool timetrue;
    private float time;

    private GameObject boss;
    private GameObject boss1;
    private GameObject boss2;


    //��񂾂�������������
    public static bool turnBool;

    //����͗^�_���[�W
    public static int at;

    float inputHorizontal;
    float inputVertical;
    private bool attack;
    private bool skyattck;

    private float currentTime;

    Rigidbody rb;
    [SerializeField] private int upForce;
    float distance;
    //bool isCalledOnc=false;

    //�X�e�[�^�X�̏㏸��
    private float ReHP;
    private int AtUP;
    private float SpUP;

    //�㏸�������̃p�����[�^�\���p
    private Image HpImage;
    private Image AtImage;
    private Image SpImage;
    private int Hpnamber;
    private int Atnamber;
    private int Spnamber;

    //���点��p
    private bool Hpcount;
    private bool Atcount;
    private bool Spcount;
    private bool RainbowOn;
    private int Rainbownanber;
    private int RainbowAtUp;


    public bool invaded;
    public bool invadedA;
    public bool invadedB;

    private float moveSpeed;
    float movedash;
    private bool dashtrue;

    //Hp�Ǘ�
    float damagect;
    //�ő�HP�ƌ��݂�HP�B
    public float maxHp;
    float currentHp;
    //Slider������
    private Slider slider;
    float Recovery;
    int Count=0;

    //�ꎞ��~.
    public static float Timezero;

    //�F�ύX�B
    Material material = null;

    [Header("�F�ύX�X�p��")]
    public float Chnge_Color_Time = 0.1f;

    [Header("�ύX�̊��炩��")]
    public float Smooth = 0.01f;

    [Header("�F��")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;// 0 ~ 1

    [Header("�ʓx")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;// 0 ~ 1

    [Header("���x")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;// 0 ~ 1

    [Header("�F�� MAX")]
    [Range(0, 1)] public float HSV_Hue_max = 1.0f;// 0 ~ 1

    [Header("�F�� MIN")]
    [Range(0, 1)] public float HSV_Hue_min = 0.0f;// 0 ~ 1

    //����.
    [SerializeField] private GameObject Weapon;
    private Transform weapontrans;
    [SerializeField] private GameObject storageWeapon;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider playerslider;

    //�s���A�p�����[�^�ݒ�p.
    private int characternamver;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;

    AudioSource audioSource;

    void Start()
    {
        PlayerSelection();
        playerocntInstance = this;

        Timezero = 1;

        currentTime=0;
        //  anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        distance = 0.4f;

        Hpcount = false;
        Atcount = false;
        Spcount = false;
        RainbowOn = false;
        Rainbownanber = 0;
        RainbowAtUp = 500;


        invaded = false;
        invadedA=false;
        invadedB=false;

        slider = GameObject.Find("Canvas/PlayerHP").GetComponent<Slider>();
        //Slider�𖞃^���ɂ���B
        slider.value = 1;

        Recovery = 40;

        //���݂�HP���ő�HP�ɁB

        maxHp = currentHp;

        Hpnamber = 0;
        Atnamber = 0;
        Spnamber = 0;

        movedash = 1;
        dashtrue = false;

        turnBool = false;
        timetrue = false;
        time = 0;

        material = GetComponent<Renderer>().material;
        HSV_Hue = HSV_Hue_min;

        countE = 0;

        GameObject canvasObject=GameObject.FindWithTag("canvas");
        canvas =canvasObject.GetComponent<Canvas>();

        playerslider = GameObject.Find("PlayerCanvas/Slider").GetComponent<Slider>();
        playerslider.value = 0;
        canvas.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Timezero == 1)
        {
            if (Input.GetMouseButton(0))// && attack == false)
            {
                Attack();
            }
            if (Input.GetMouseButtonUp(0))
            {
                Weapon.SetActive(false);
                storageWeapon.SetActive(true);
                attack = false;
            }

            if (Input.GetKey("e"))
            {
                if (turnBool == false&&attack==false&&timetrue==false)
                {
                    canvas.enabled = true;
                    countE++;
                    playerslider.transform.forward = Camera.main.transform.forward;
                    playerslider.value=(float)countE/100.0f;
                    //UnityEngine.Debug.Log(countE);
                }
                if (countE == 1)
                {
                    audioSource.PlayOneShot(sound1);
                }
                else if(countE ==40)
                {
                    audioSource.PlayOneShot(sound2);
                }
                else if (countE ==80)
                {
                    audioSource.PlayOneShot(sound3);
                }

                if (turnBool == false &&countE==100)
                {
                    turnBool = true;
                    timetrue = true;
                    countE = 0;
                    playerslider.value = 0;
                    canvas.enabled = false;

                    ScoreManeger.Instance.Cooking();

                    //�����邩�󂯎��Ƃ���
                    int Consumption = ScoreManeger.consumption;

                    //�����łȂ�̃��[�h�ŉ���������󂯎�� 
                    bool comsumAll = ScoreManeger.ComsumAll;
                    //UnityEngine.Debug.Log(comsumAll);
                    bool comsumRe = ScoreManeger.ComsumRe;
                    //UnityEngine.Debug.Log(comsumRe);
                    bool comsumAt = ScoreManeger.ComsumAt;
                    //UnityEngine.Debug.Log(comsumAt);
                    bool comsumSp = ScoreManeger.ComsumSp;
                    //UnityEngine.Debug.Log(comsumSp);
                    if (comsumAll == true || comsumRe == true)
                    {
                        Count++;
                        ReHP = ReHP * Consumption;
                        maxHp += ReHP;
                        //
                        for(int i=0;i<Consumption;i++)
                        {
                            Vector3 localScale = slider.transform.localScale;
                            localScale.x += 0.2f;
                            slider.transform.localScale = localScale;
                            //
                            Vector3 position = slider.transform.position;
                            position.x += 24;
                            slider.transform.position = position;
                        }

                        if (Hpcount == false)
                        {
                            //�������F��ς���
                            for (int i = 0; i < Consumption; ++i)
                            {
                                Hpnamber++;
                                if (Hpnamber < 10)
                                {
                                    HpImage = GameObject.Find("HpImage" + Hpnamber).GetComponent<Image>();
                                    HpImage.color = new Color32(0, 255, 0, 255);
                                }
                                else if (Hpnamber == 10)
                                {
                                    //���点��pHP��true.
                                    HpImage = GameObject.Find("HpImage" + Hpnamber).GetComponent<Image>();
                                    HpImage.color = new Color32(0, 255, 0, 255);
                                    Hpcount = true;
                                }
                            }
                        }
                    }
                    if (comsumAll == true || comsumAt == true)
                    {
                        Count++;
                        at = at + AtUP * Consumption;
                        Weapon.transform.localScale+=new Vector3(0.01f * Consumption,0.01f*Consumption,0.01f*Consumption);

                        if (Atcount == false)
                        {
                            //
                            for (int j = 0; j < Consumption; ++j)
                            {
                                Atnamber++;
                                if (Atnamber < 10)
                                {
                                    AtImage = GameObject.Find("AtImage" + Atnamber).GetComponent<Image>();
                                    AtImage.color = new Color32(255, 0, 0, 255);
                                }
                                else if (Atnamber == 10)
                                {
                                    AtImage = GameObject.Find("AtImage" + Atnamber).GetComponent<Image>();
                                    AtImage.color = new Color32(255, 0, 0, 255);
                                    Atcount = true;
                                }
                            }
                        }
                    }
                    if (comsumAll == true || comsumSp == true)
                    {
                        Count++;
                        moveSpeed = moveSpeed + SpUP * (float)Consumption;

                        if (Spcount == false)
                        {
                            //
                            for (int n = 0; n < Consumption; ++n)
                            {
                                Spnamber++;
                                if (Spnamber < 10)
                                {
                                    SpImage = GameObject.Find("SpImage" + Spnamber).GetComponent<Image>();
                                    SpImage.color = new Color32(0, 255, 255, 255);
                                }
                                else if (Spnamber == 10)
                                {
                                    SpImage = GameObject.Find("SpImage" + Spnamber).GetComponent<Image>();
                                    SpImage.color = new Color32(0, 255, 255, 255);
                                    Spcount = true;
                                }
                            }
                        }
                    }

                    if (Count == 0)
                    {
                        turnBool = false;
                        timetrue = false;
                        time = 0;
                        UnityEngine.Debug.Log("�������s");
                        return;
                    }
                    else if (Count >= 1)
                    {
                        Recovery = Recovery * Consumption * Count;
                        currentHp += Recovery;
                        if (maxHp < currentHp)
                        {
                            currentHp = maxHp;
                        }
                        Count = 0;
                        slider.value = currentHp / maxHp;
                        Invoke("Restrat", 6.0f);
                    }
                }
            }
            if (Input.GetKeyUp("e"))
            {
                if (turnBool == false)
                {
                    countE = 0;
                    playerslider.value = 0;
                    canvas.enabled = false;
                }
            }

            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");

            Vector3 rayPosition = transform.position + new Vector3(0.0f, -0.4f, 0.0f);
            Ray ray = new Ray(rayPosition, Vector3.down);
            bool isGround = Physics.Raycast(ray, distance);
            UnityEngine.Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
            if (Input.GetKeyDown(KeyCode.Space) && isGround )
            {
                //UnityEngine.Debug.Log("�W�����v");
                rb.AddForce(new Vector3(0, upForce, 0));
                skyattck = true;
            }
            else if(isGround&&skyattck==true)
            {
                skyattck = false;
                UnityEngine.Debug.Log("�n�ʂɂ��Ă���");
            }

        }

        if (timetrue==true&&Timezero==1)
        {
            time += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (Timezero == 1)
        {
            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

            // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
            rb.velocity = moveForward * moveSpeed * movedash + new Vector3(0, rb.velocity.y, 0);

            // �L�����N�^�[�̌�����i�s������
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
                //�_�b�V��
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    material.color = new Color32(0, 0, 255, 255);
                    dashtrue = true;
                    movedash = 1.9f;
                }
                else if (dashtrue == true)
                {
                    material.color = new Color32(183, 183, 183, 255);
                    dashtrue = false;
                    movedash = 1.0f;
                }
            }
        }
        if (Input.GetKeyDown("t"))
        {
            if (Timezero == 1)
            {
                Timezero = 0;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                if (timetrue == true)
                {
                    audioSource.Pause();
                    UnityEngine.Debug.Log("invoke������");
                    CancelInvoke();
                }
            }
            else
            {
                Timezero = 1;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                if (timetrue == true)
                {
                    audioSource.UnPause();
                    Invoke("Restrat", 6.0f-time);
                }
            }
        }

        //���点��
        if (Hpcount == true && Atcount == true && Spcount == true && RainbowOn == false)
        {
            StartCoroutine("Change_Color");
            RainbowOn = true;
            Weapon.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
            at += RainbowAtUp;
        }

    }

    private void Restrat()
    {
        //UnityEngine.Debug.Log("�����ĊJ");
        audioSource.Stop();
        turnBool = false;
        timetrue =false;
        time = 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "BossArea")
        {
          //  UnityEngine.Debug.Log("Boss�͈͓̔�����");
            invaded = true;

        }
        if (other.gameObject.name == "CautionArea(A)")
        {
            // UnityEngine.Debug.Log("A�͈͓̔�����");
            invadedA = true;
        }
        if(other.gameObject.name == "CautionArea(B)")
        {
            // UnityEngine.Debug.Log("B�͈͓̔�����");
            invadedB = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "BossArea")
        {
            invaded = false;
        }
        if (other.gameObject.name == "CautionArea(A)")
        {
            invadedA = false;
        }
        if (other.gameObject.name == "CautionArea(B)")
        {
            invadedB = false;
        }
    }

    IEnumerator Change_Color()
    {
        if (Timezero == 1)
        {
            HSV_Hue += Smooth;

            if (HSV_Hue >= HSV_Hue_max)
            {
                HSV_Hue = HSV_Hue_min;
            }

            material.color = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness);
        }
        yield return new WaitForSeconds(Chnge_Color_Time);

        Rainbownanber++;
        if (Rainbownanber >= 200)
        {
            HSV_Hue = 0;
            at -= RainbowAtUp;
            Weapon.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
            material.color= new Color32(183, 183, 183,255);

            for (int i = 0; i < 10; i++)
            {
                if (Hpnamber > 1)
                {
                    HpImage = GameObject.Find("HpImage" + Hpnamber).GetComponent<Image>();
                    HpImage.color = new Color32(0, 0, 0, 42);
                }
                else if (Hpnamber == 1)
                {
                    //���点��pHP��true.
                    HpImage = GameObject.Find("HpImage" + Hpnamber).GetComponent<Image>();
                    HpImage.color = new Color32(0, 0, 0, 42);
                    Hpcount = false;
                }
                Hpnamber--;
            }
            for (int j = 0; j < 10; j++)
            {
                if (Atnamber > 1)
                {
                    AtImage = GameObject.Find("AtImage" + Atnamber).GetComponent<Image>();
                    AtImage.color = new Color32(0, 0, 0, 42);
                }
                else if (Atnamber == 1)
                {
                    AtImage = GameObject.Find("AtImage" + Atnamber).GetComponent<Image>();
                    AtImage.color = new Color32(0, 0, 0, 42);
                    Atcount = false;
                }
                Atnamber--;
            }
            for (int n = 0; n < 10; n++)
            {
                if (Spnamber > 1)
                {
                    SpImage = GameObject.Find("SpImage" + Spnamber).GetComponent<Image>();
                    SpImage.color = new Color32(0, 0, 0, 42);
                }
                else if (Spnamber == 1)
                {
                    SpImage = GameObject.Find("SpImage" + Spnamber).GetComponent<Image>();
                    SpImage.color = new Color32(0, 0, 0, 42);
                    Spcount = false;
                }
                Spnamber--;
            }
            RainbowOn = false;
            Rainbownanber = 0;

            yield break;
        }
        else
        {
            StartCoroutine("Change_Color");
        }
    }

    //����L�����̏��擾.
    void PlayerSelection()
    {
        characternamver = SelectCharacter.ChooseCharacter.playernamver;
        //UnityEngine.Debug.Log(characternamver);

        Weapon = GameObject.Find("weapon");
        weapontrans = Weapon.transform;
        Weapon.SetActive(false);
        storageWeapon = GameObject.Find("storageWeapon");
        storageWeapon.SetActive(true);

        if (characternamver == 1)
        {
            Vector3 localAngle = weapontrans.localEulerAngles;
            localAngle.y = -130.0f;
            weapontrans.localEulerAngles = localAngle;
            upForce = 50000;
            //��
            currentHp = 200;
            at = 100;
            moveSpeed = 10f;
            ReHP = 40.0f;
            AtUP=30;
            SpUP=0.3f;
}
        else if(characternamver == 2)
        {
            Vector3 localAngle = weapontrans.localEulerAngles;
            localAngle.y = -130.0f;
            weapontrans.localEulerAngles = localAngle;
            upForce = 50000;
            //��
            currentHp = 200;
            at= 100;
            moveSpeed = 10f;
            ReHP = 40.0f;
            AtUP = 30;
            SpUP = 0.3f;
        }
    }

    void Attack()
    {
        Weapon.SetActive(true);
        storageWeapon.SetActive(false);
        attack = true;
        currentTime += Time.deltaTime;

        if(characternamver==1)
        {
            Vector3 localAngle = weapontrans.localEulerAngles;
            localAngle.y +=30.0f ;
            weapontrans.localEulerAngles = localAngle;
            UnityEngine.Debug.Log(weapontrans.localEulerAngles.y);
            if (localAngle.y==80)
            {
                localAngle.y = -130.0f;
                weapontrans.localEulerAngles = localAngle;
                currentTime = 0f;
            }
        }
        else if(characternamver==2)
        {
            Vector3 localAngle = weapontrans.localEulerAngles;
            localAngle.y += 30.0f;
            weapontrans.localEulerAngles = localAngle;
            if (localAngle.y==80)
            {
                localAngle.y = -130.0f;
                weapontrans.localEulerAngles = localAngle;
                currentTime = 0f;
            }
        }
    }

    public void Damege(float damege)
    {
        if (Timezero == 1)
        {
            damagect = damege;
            //���݂�HP����_���[�W������
            currentHp = currentHp - damagect;
            damagect = 0;
            //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f�B

            slider.value = currentHp / maxHp;
            // Debug.Log("slider.value : " + slider.value);

            if (currentHp <= 0)
            {
                //Debug.Log("�Q�[���I�[�o�[");
                slider = GameObject.Find("Canvas/PlayerHP").GetComponent<Slider>();
                //Slider�𖞃^���ɂ���B
                slider.value = 1;
                //���݂�HP���ő�HP�Ɠ����ɁB
                currentHp = maxHp;
                SceneManager.LoadScene("GameOver");

            }
        }
    }
}