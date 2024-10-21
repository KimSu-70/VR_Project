using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private bool isg = false;

    private void Start()
    {
        AudioManager.Instance.PlayBgm(true);
        isg = true;
    }

    public void PlaysBgm()
    {
        if (isg == false)
        {
            AudioManager.Instance.PlayBgm(true);
            isg = true;
        }
        else
        {
            AudioManager.Instance.PlayBgm(false);
            isg = false;
        }
    }
}
