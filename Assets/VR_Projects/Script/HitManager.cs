using System.Collections;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public static HitManager Instance { get; private set; }

    public enum Turn { Player, Bot }
    public Turn currentTurn;
    public float turnDuration = 7f; // �� ���� �ð�
    public float curTurnTime;       // ���� �� �ð�

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
        currentTurn = Turn.Player; // ���� ���� �� �÷��̾� ��
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
            Debug.Log($"{(int)curTurnTime} �ʱ�ȭ");
        }
    }

    private IEnumerator TurnCycle()
    {
        while (true)
        {
            if (deadCheck == false)
            {
                // �Ͽ� ���� �ൿ ó��
                if (currentTurn == Turn.Player)
                {
                    Debug.Log("Player's ��");
                    playerAttackCheck = true;
                    yield return new WaitForSeconds(turnDuration); // �� ���� �ð� ���
                    currentTurn = Turn.Bot; // �� ��ȯ
                }
                else
                {
                    // ���� ���� �ൿ ó��
                    Debug.Log("Bot's ��");
                    attackCheck = true;
                    yield return new WaitForSeconds(turnDuration); // �� ���� �ð� ���
                    currentTurn = Turn.Player; // �� ��ȯ
                }
            }
            else if (deadCheck)
            {
                yield break; // �ڷ�ƾ ����
            }
        }
    }
}
