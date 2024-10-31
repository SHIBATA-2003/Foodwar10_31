using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace SelectCharacter
{
    public class SceneTransition : MonoBehaviour
    {
        private MyGameManagerData myGameManagerData;
        public GameObject gameButton;

        private void Start()
        {
            myGameManagerData = FindObjectOfType<MyGameManager>().GetMyGameManagerData();
            //�@�Q�[���X�^�[�g�{�^���𖳌��ɂ���
            gameButton.SetActive(false);
        }

        public void GoToOtherScene(string stage)
        {
            //�@���̃V�[���f�[�^��MyGameManager�ɕۑ�
            myGameManagerData.SetNextSceneName(stage);
            //�@�Q�[���X�^�[�g�{�^����L���ɂ���
            gameButton.SetActive(true);

        }
        public void CharacterSelect()
        {
            //�@�L�����N�^�[�I���V�[����
            SceneManager.LoadScene("CharacterSelect");
        }
        public void GameStart()
        {
            //�@MyGameManagerData�ɕۑ�����Ă��鎟�̃V�[���Ɉړ�����
            SceneManager.LoadScene(myGameManagerData.GetNextSceneName());
        }
    }
}
