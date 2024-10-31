using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelectCharacter
{
    [CreateAssetMenu(fileName = "MyGameManagerData", menuName = "MyGameManagerData")]
    public class MyGameManagerData : ScriptableObject
    {
        //�@���̃V�[����
        [SerializeField]
        private string nextSceneName;
        //�@�g�p����L�����N�^�[�v���n�u
        [SerializeField]
        private GameObject character;
        //�@�f�[�^�̏�����
        private void OnEnable()
        {
            //�@���Z�b�g
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