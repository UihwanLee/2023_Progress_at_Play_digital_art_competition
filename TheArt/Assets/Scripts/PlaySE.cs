using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySE : MonoBehaviour
{
    // SE ����

    public void PlayClickSound()
    {
        if(AudioManager.instance.isAudioPlay)
        {
            AudioManager.instance.PlaySE("Click button");
        }
    }
}
