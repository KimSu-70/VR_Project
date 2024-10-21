using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hit"))
        {
            GameManager.Instance.playerhp -= 1;
        }
    }
}
