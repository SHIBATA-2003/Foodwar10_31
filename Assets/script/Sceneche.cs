using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Sceneche : MonoBehaviour
{
    private Text scoreLabel;
    private int Score;

    private Text timeText;
    private float Time;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameClear")
        {
            scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
            timeText = GameObject.Find("Gametime").GetComponent<Text>();
            Score = ScoreManeger.score;
            Time = ScoreManeger.counttimestatic;

            scoreLabel.text = Score + "KILL";
            timeText.text = (int)Time + "�b";
        }

        Cursor.visible = true; //OS�J�[�\���\��
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("QuestSelect");
    }
}

