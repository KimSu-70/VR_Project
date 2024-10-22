using System.Collections;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public static HitManager Instance { get; private set; }

    public enum Turn { Player, Bot }
    public Turn currentTurn;
    public float turnDuration = 7f; // 턴 지속 시간
    public float curTurnTime;       // 현재 턴 시간

    public bool attackCheck = false;
    public bool playerAttackCheck = false;
    public bool deadCheck = false;

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
        currentTurn = Turn.Player; // 게임 시작 시 플레이어 턴
        playerAttackCheck = true;
        StartCoroutine(TurnCycle());
        curTurnTime = turnDuration;
    }

    private void Update()
    {
        if (curTurnTime > 0)
        {
            curTurnTime -= 1 * Time.deltaTime;
            Debug.Log((int)curTurnTime);
        }
        else
        {
            curTurnTime = turnDuration;
            Debug.Log($"{(int)curTurnTime} 초기화");
        }
    }

    private IEnumerator TurnCycle()
    {
        while (true)
        {
            if (deadCheck == false)
            {
                // 턴에 따라 행동 처리
                if (currentTurn == Turn.Player)
                {
                    Debug.Log("Player's 턴");
                    playerAttackCheck = true;
                    yield return new WaitForSeconds(turnDuration); // 턴 지속 시간 대기
                    currentTurn = Turn.Bot; // 턴 전환
                }
                else
                {
                    // 몬스터 턴의 행동 처리
                    Debug.Log("Bot's 턴");
                    attackCheck = true;
                    yield return new WaitForSeconds(turnDuration); // 턴 지속 시간 대기
                    currentTurn = Turn.Player; // 턴 전환
                }
            }
            else if (deadCheck)
            {
                yield break; // 코루틴 종료
            }
        }
    }
}
