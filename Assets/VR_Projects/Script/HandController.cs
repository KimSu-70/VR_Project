using UnityEngine;
using static HitManager;

public class HandController : MonoBehaviour
{
    public Transform hand; // VR ��Ʈ�ѷ��� Transform
    private Vector3 previousPosition;
    private float speed; // ���� �ӵ�
    public float threshold = 6f; // ���ط� ������ ���� �ӵ� �Ӱ谪

    [SerializeField] BotController bot;

    void Start()
    {
        previousPosition = hand.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        CalculateSpeed();
    }

    void CalculateSpeed()
    {
        // ���� ��ġ�� ���� ��ġ�� �Ÿ� ���
        Vector3 currentPosition = hand.position;
        float distance = Vector3.Distance(currentPosition, previousPosition);

        // ������ ���� �ð����� ������ �ӵ� ���
        speed = distance / Time.deltaTime;

        // ���� ��ġ ������Ʈ
        previousPosition = currentPosition;
    }

    // Ÿ���� �߻����� �� ȣ��Ǵ� �޼���
    public float CalculateDamage()
    {
        float baseDamage;
        if (speed > threshold)
        {
            baseDamage = 2;
        }
        else
        {
            baseDamage = 1;
        }
        return baseDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        int damage = (int)CalculateDamage();
        if (other.CompareTag("Bot"))
        {
            if (HitManager.Instance.currentTurn == Turn.Player)
            {
                bot.BotHit(damage);
            }
        }
    }
}
