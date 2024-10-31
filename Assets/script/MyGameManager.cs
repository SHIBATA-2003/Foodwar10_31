using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace SelectCharacter
{

    public class MyGameManager : MonoBehaviour
    {
        //　世界に一つだけのMyGameManager
        private static MyGameManager myGameManager;
        //　ゲーム全体で管理するデータ
        [SerializeField]
        private MyGameManagerData myGameManagerData = null;

        private void Awake()
        {
            if (myGameManager == null)
            {
                myGameManager = this;
                DontDestroyOnLoad(this);
            }
        }
        //　MyGameManagerDataを返す
        public MyGameManagerData GetMyGameManagerData()
        {
            return myGameManagerData;
        }
    }
}