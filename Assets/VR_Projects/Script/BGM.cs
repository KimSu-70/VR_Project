using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBgm(true);
    }

    public void PBgm()
    {
        AudioManager.Instance.PlayBgm(true);
    }

    public void SBgm()
    {
        AudioManager.Instance.PlayBgm(false);
    }
}
