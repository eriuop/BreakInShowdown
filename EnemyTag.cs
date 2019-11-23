using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{
    public AudioClip dieCryClip;


    public void dieCryHasSound()
    {
        MatchInfo matchInfo = GameObject.FindObjectOfType<MatchInfo>();
        matchInfo.AudioClipPlayer(dieCryClip);
    }

}
