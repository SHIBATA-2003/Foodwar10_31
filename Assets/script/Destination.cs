using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public static Destination InstanceDe;

    void Start()
    {
        InstanceDe = this;

        int GameDifficulty = BackgroundCl.Gamedifficulty;
        Vector3 Stagescale = this.transform.lossyScale;
        Stagescale.x *= (float)GameDifficulty;
        Stagescale.z *= (float)GameDifficulty;

        this.transform.localScale = Stagescale;
    }

    //“–‚½‚Á‚½‚Æ‚«.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManeger.Instance.QuestClear();
            Destroy(gameObject);
        }
    }

    public void DestroyDestination()
    {
        Destroy(gameObject);
    }
}
