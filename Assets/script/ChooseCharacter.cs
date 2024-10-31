using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SelectCharacter
{
    public class ChooseCharacter : MonoBehaviour
    {
        private MyGameManagerData myGameManagerData;
        private GameObject gameStartButton;
        public static int playernamver;

        private void Start()
        {
            //　世界に一つだけのMyGameManagerからMyGameManagerDataを取得する
            myGameManagerData = FindObjectOfType<MyGameManager>().GetMyGameManagerData();
            //　ゲームスタートボタンを取得する
            gameStartButton = transform.parent.Find("ButtonPanel/GameStart").gameObject;
            //　ゲームスタートボタンを無効にする
            gameStartButton.SetActive(false);

            playernamver = 0;

        }
        //　キャラクターを選択した時に実行しキャラクターデータをMyGameManagerDataにセット
        public void OnSelectCharacter(GameObject character)
        {
            //　ボタンの選択状態を解除して選択したボタンのハイライト表示を可能にする為に実行
            EventSystem.current.SetSelectedGameObject(null);
            //　MyGameManagerDataにキャラクターデータをセットする
            myGameManagerData.SetCharacter(character);
            //　ゲームスタートボタンを有効にする
            gameStartButton.SetActive(true);
        }
        //　キャラクターを選択した時に背景をオンにする
        public void SwitchButtonBackground(int buttonNumber)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == buttonNumber - 1)
                {
                    transform.GetChild(i).Find("Background").gameObject.SetActive(true);
                    playernamver = buttonNumber;
                }
                else
                {
                    transform.GetChild(i).Find("Background").gameObject.SetActive(false);
                }
            }
        }
    }
}