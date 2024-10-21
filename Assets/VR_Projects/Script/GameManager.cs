using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject player;
    [SerializeField] int playerMaxhp = 6;
    public int playerhp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player.SetActive(false);
        playerhp = playerMaxhp;
    }

    public void GetSlap()
    {
        player.SetActive(true);
    }

    public void NotSlap()
    {
        player.SetActive(false);
    }
}
