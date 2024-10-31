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
            //　ゲームスタートボタンを無効にする
            gameButton.SetActive(false);
        }

        public void GoToOtherScene(string stage)
        {
            //　次のシーンデータをMyGameManagerに保存
            myGameManagerData.SetNextSceneName(stage);
            //　ゲームスタートボタンを有効にする
            gameButton.SetActive(true);

        }
        public void CharacterSelect()
        {
            //　キャラクター選択シーンへ
            SceneManager.LoadScene("CharacterSelect");
        }
        public void GameStart()
        {
            //　MyGameManagerDataに保存されている次のシーンに移動する
            SceneManager.LoadScene(myGameManagerData.GetNextSceneName());
        }
    }
}
