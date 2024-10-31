using UnityEngine.SceneManagement;
using UnityEngine;

public class modori: MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true; //OSカーソル表示
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("QuestSelect");
    }

}
