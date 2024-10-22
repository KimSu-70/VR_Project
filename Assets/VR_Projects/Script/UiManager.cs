using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider time;
    [SerializeField] Slider playerHp;
    [SerializeField] Slider botHp;
    [SerializeField] BotController bot;
    [SerializeField] XRBaseController rightController;

    [SerializeField] GameObject gameOver;

    private void Start()
    {
        gameOver.SetActive(false);
    }

    private void Update()
    {
        Times();
        PlayerHp();
        BotHp();
    }

    public void Times()
    {
        time.maxValue = HitManager.Instance.turnDuration;
        time.minValue = 0;
        time.value = HitManager.Instance.curTurnTime;
    }

    public void PlayerHp()
    {
        playerHp.maxValue = GameManager.Instance.playerMaxhp;
        playerHp.minValue = 0;
        playerHp.value = GameManager.Instance.playerhp;
    }

    public void BotHp()
    {
        botHp.maxValue = bot.maxHp;
        botHp.minValue = 0;
        botHp.value = bot.hp;
    }

    public void GameOver()
    {
        HitManager.Instance.deadCheck = true;
        gameOver.SetActive(true);
        if (HitManager.Instance.deadCheck && rightController.activateInteractionState.active)
        {
            SceneManager.LoadScene("Slap");
        }
    }
}
