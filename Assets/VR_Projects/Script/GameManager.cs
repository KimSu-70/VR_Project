using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject player;
    [SerializeField] UiManager ui;
    public int playerMaxhp = 6;
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
    private void Update()
    {
        if(playerhp <= 0)
        {
            playerhp = 0;
            HitManager.Instance.deadCheck = true;
            ui.GameOver();
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
