using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace SelectCharacter
{

    public class MyGameManager : MonoBehaviour
    {
        //�@���E�Ɉ������MyGameManager
        private static MyGameManager myGameManager;
        //�@�Q�[���S�̂ŊǗ�����f�[�^
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
        //�@MyGameManagerData��Ԃ�
        public MyGameManagerData GetMyGameManagerData()
        {
            return myGameManagerData;
        }
    }
}