using UnityEngine;
using static HitManager;

public class HandController : MonoBehaviour
{
    public Transform hand; // VR 컨트롤러의 Transform
    private Vector3 previousPosition;
    private float speed; // 손의 속도
    public float threshold = 6f; // 피해량 증가를 위한 속도 임계값

    [SerializeField] BotController bot;

    void Start()
    {
        previousPosition = hand.position; // 초기 위치 설정
    }

    void Update()
    {
        CalculateSpeed();
    }

    void CalculateSpeed()
    {
        // 현재 위치와 이전 위치의 거리 계산
        Vector3 currentPosition = hand.position;
        float distance = Vector3.Distance(currentPosition, previousPosition);

        // 프레임 간의 시간으로 나누어 속도 계산
        speed = distance / Time.deltaTime;

        // 이전 위치 업데이트
        previousPosition = currentPosition;
    }

    // 타격이 발생했을 때 호출되는 메서드
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
