using UnityEngine.SceneManagement;
using UnityEngine;

public class modori: MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true; //OS�J�[�\���\��
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("QuestSelect");
    }

}
