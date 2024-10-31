using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SelectCharacter
{
    public class cameracont : MonoBehaviour
    {
        public Image setting;
        public Image Pause;
        private GameObject pause;
        private GameObject btnF;
        private GameObject btnW;
        private GameObject btn1;
        private GameObject btnwithdrawal;
        public Text text;
        private int count2 = 0;
        private bool Gamesetting=false;
     
        private float zero = 1;
        CursorLockMode wantedMode = CursorLockMode.Confined;
        private MyGameManagerData myGameManagerData;
        private GameObject targetObj;
        private Vector3 targetPos;
        private bool Ground;

        private GameObject MinimapPlayer;
        private Transform mytransform_mini;
        private GameObject minimap;
        private Camera minimapcamera;

        private bool minimapbool;
        private GameObject circularminimap;

        //camera
        private GameObject minicamera;
        private int cameract;

        private Camera camera_;
        private Vector3 cameraposition;

        void Start()
        {
            btnF = GameObject.Find("SettingImage/ButtonF");
            btnW = GameObject.Find("SettingImage/ButtonW");
            btn1 = GameObject.Find("SettingImage/Button1");
            btnwithdrawal = GameObject.Find("SettingImage/Buttonwithdrawal");
            pause = GameObject.Find("pause/Pause");

            minimap = GameObject.Find("minicamera");
            minimapcamera = minimap.GetComponent<Camera>();

            minimapbool = true;
            circularminimap = GameObject.Find("Circularminimap");

            //
            minicamera = GameObject.Find("Camera");
            camera_ = minicamera.GetComponent<Camera>();
            cameraposition = minicamera.transform.position;
            int diffi = BackgroundCl.Gamedifficulty;
            if (diffi == 2)
            {
                minimapcamera.orthographicSize *= 1.5f;
                camera_.orthographicSize *= 2.0f;
                cameraposition.z = 75.0f;
            }
            else if (diffi == 3)
            {
                minimapcamera.orthographicSize *= 2.5f;
                camera_.orthographicSize *= 3.0f;
                cameraposition.z = 113.0f;
            }
            minicamera.transform.position = cameraposition;
            minicamera.SetActive(false);
            cameract = 0;

            setting.enabled = false;
            Pause.enabled=false;
            pause.SetActive(false);
            btnF.SetActive(false);
            btnW.SetActive(false);
            btn1.SetActive(false);
            btnwithdrawal.SetActive(false);

            Ground = false;
        }

        void Awake()
        {
            myGameManagerData = FindObjectOfType<MyGameManager>().GetMyGameManagerData();
            targetObj = Instantiate(myGameManagerData.GetCharacter(), Vector3.zero, Quaternion.identity);
            targetPos = targetObj.transform.position;

            MinimapPlayer = (GameObject)Resources.Load("MiniPlayer");
            Instantiate(MinimapPlayer,new Vector3(), Quaternion.Euler(0.0f,0.0f,0.0f));

            Cursor.lockState = wantedMode;
            Cursor.visible = false; //OSカーソル非表示
        }

        void FixedUpdate()
        {
            Transform mytransform = this.transform;
            // targetの移動量分、自分（カメラ）も移動する
            transform.position += targetObj.transform.position - targetPos;
            targetPos = targetObj.transform.position;

            if(minimapbool)
            {
                minimap.transform.position = new Vector3(targetObj.transform.position.x, targetObj.transform.position.y + 65, targetObj.transform.position.z);
            }

            //
            if (Input.GetKey("p"))
            {
                Vector3 localAngle=mytransform.eulerAngles;
                Vector3 localPos=mytransform.position;
                localAngle.x = 0.0f;
                localAngle.y= 0.0f;
                localAngle.z= 0.0f;
                localPos.x=targetPos.x;
                localPos.y =targetPos.y+1.0f;
                localPos.z=targetPos.z-10.0f;

                mytransform.eulerAngles = localAngle;
                mytransform.position=localPos;
            }

            //
            if (Input.GetKeyDown("t"))
            {
                if (count2 == 0)
                {
                    Cursor.lockState = wantedMode = CursorLockMode.None; //標準モード
                                                                         // Debug.Log("DEBUG:Cursor is normal");
                    zero = 0;
                    Cursor.visible = true; //OSカーソル表示
                    SettingI();
                }
                else if (count2 == 1)
                {
                    Cursor.lockState = wantedMode = CursorLockMode.Confined; //はみ出さないモード
                                                                             // Debug.Log("DEBUG:Cursor is confined");
                    zero = 1;
                    Cursor.visible = false; //OSカーソル非表示
                    SettingI();
                    if(Gamesetting==true)
                    {
                        btnW.SetActive(false);
                        btnF.SetActive(false);
                        text.text = "設定";
                        Gamesetting = false;
                    }
                }

            }

            //
            if (Input.GetKeyDown("m"))
            {
                if (cameract == 0)
                {
                    cameract = 1;
                    minicamera.SetActive(true);
                    circularminimap.SetActive(false);
                }
                else if (cameract == 1)
                {
                    cameract = 0;
                    minicamera.SetActive(false);
                    circularminimap.SetActive(true);
                }
            }
        }
        void OnGUI()
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 60f*zero);
           if(Ground==false)
           {
                // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
                transform.RotateAround(targetPos, transform.right, -mouseInputY * Time.deltaTime * 60f * zero);
           }
           else if (Ground == true && mouseInputY <= 0.0)
           {
                transform.RotateAround(targetPos, transform.right, -mouseInputY * Time.deltaTime * 60f * zero);
           }
        }
        public void SettingI()
        {
            if (count2 == 0)
            {
                count2 = 1;
                setting.enabled = true;
                Pause.enabled = true;
                pause.SetActive(true);
                btn1.SetActive(true);
                btnwithdrawal.SetActive(true);
                // slider.SetActive(true);
            }
            else if(count2==1) 
            {
                count2 = 0;
                setting.enabled = false;
                Pause.enabled = false;
                pause.SetActive(false);
                btn1.SetActive(false);
                btnwithdrawal.SetActive(false);
                // slider.SetActive(false);
            }

        }
        public void On1()
        {
            if(Gamesetting==false)
            {
                btnF.SetActive(true);
                btnW.SetActive(true);
                Pause.enabled = false;
                pause.SetActive(false);
                text.text = "もどる";
                Gamesetting = true;
            }
            else if(Gamesetting==true)
            {
                btnW.SetActive(false);
                btnF.SetActive(false);
                Pause.enabled = true;
                pause.SetActive(true);
                text.text = "設定";
                Gamesetting=false;
            }
        }
        public void OnClickFullScreenMode()
        {
            // フルスクリーンモードに切り替えます
            Screen.fullScreen = true;
        }

        public void OnClickWindowMode()
        {
            // ウィンドウモードに切り替えます
            Screen.fullScreen = false;
        }

        public void Withdrawal()
        {
            SceneManager.LoadScene("GameOver");
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                Ground=true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                Ground = false;
            }
        }
    }
}
