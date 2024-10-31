using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelectCharacter
{
    public class GameStartButton : MonoBehaviour
    {
        private SceneTransition sceneTransition;

        private void Start()
        {
            sceneTransition = FindObjectOfType<SceneTransition>();
        }

        public void OnGameStart()
        {
            //@MyGameManagerData‚É•Û‘¶‚³‚ê‚Ä‚¢‚éŸ‚ÌƒV[ƒ“‚ÉˆÚ“®‚·‚é
            sceneTransition.GameStart();
        }
        public void BackScenes()
        {
            SceneManager.LoadScene("QuestSelect");
        }
    }
}