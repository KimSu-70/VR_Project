using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayersOnOff : MonoBehaviour
{
    [SerializeField] XRBaseController rightController;
    void Update()
    {
        if (rightController.selectInteractionState.activatedThisFrame)
        {
            GameManager.Instance.GetSlap();
        }

        // ��ư�� ������ ����
        if (rightController.selectInteractionState.deactivatedThisFrame)
        {
            GameManager.Instance.NotSlap();
        }
    }
}
