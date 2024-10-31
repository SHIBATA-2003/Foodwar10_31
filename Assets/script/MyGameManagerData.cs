using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelectCharacter
{
    [CreateAssetMenu(fileName = "MyGameManagerData", menuName = "MyGameManagerData")]
    public class MyGameManagerData : ScriptableObject
    {
        //　次のシーン名
        [SerializeField]
        private string nextSceneName;
        //　使用するキャラクタープレハブ
        [SerializeField]
        private GameObject character;
        //　データの初期化
        private void OnEnable()
        {
            //　リセット
            if (SceneManager.GetActiveScene().name == "QuestSelect")
            {
                nextSceneName = "";
                character = null;
            }
        }

        public void SetNextSceneName(string nextSceneName)
        {
            this.nextSceneName = nextSceneName;
        }

        public string GetNextSceneName()
        {
            return nextSceneName;
        }

        public void SetCharacter(GameObject character)
        {
            this.character = character;
        }

        public GameObject GetCharacter()
        {
            return character;
        }
    }
}