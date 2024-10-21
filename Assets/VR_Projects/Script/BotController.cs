using UnityEngine;
using static HitManager;

public class BotController : MonoBehaviour
{
    public enum State { Idle, Hit, Shit, Attack, Dead, Size }
    [SerializeField] State curState = State.Idle;
    private BaseState[] states = new BaseState[(int)State.Size];

    //[SerializeField] GameObject player;
    [SerializeField] GameObject botGame;
    [SerializeField] Animator animator;
    [SerializeField] int maxHp = 10;
    [SerializeField] int hp;

    //[SerializeField] PlayerController playerhit;

    private void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Hit] = new HitState(this);
        states[(int)State.Shit] = new ShitState(this);
        states[(int)State.Attack] = new AttackState(this);
        states[(int)State.Dead] = new DeadState(this);
    }

    private void Start()
    {
        botGame = this.gameObject;
        animator = GetComponent<Animator>();

        //player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null)
        //{
        //    playerhit = player.GetComponent<PlayerController>();
        //}
        hp = maxHp;
        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        states[(int)curState].Update();
    }

    public void ChangeState(State nextState)
    {
        if (curState != nextState)
        {
            states[(int)curState].Exit();
            curState = nextState;
            states[(int)curState].Enter();
        }
    }

    private class BotState : BaseState
    {
        public BotController bot;

        public BotState(BotController bot)
        {
            this.bot = bot;
        }
    }

    private class IdleState : BotState
    {
        public IdleState(BotController bot) : base(bot)
        {
        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            if(HitManager.Instance.currentTurn == Turn.Bot && HitManager.Instance.attackCheck)
            {
                bot.ChangeState(State.Attack);
            }
        }
    }

    private class HitState : BotState
    {
        public HitState(BotController bot) : base(bot)
        {
        }

        public override void Enter()
        {
            bot.hp -= 1;
            if (bot.hp > 0)
            {
                bot.animator.SetBool("Hit", true);
                HitManager.Instance.playerAttackCheck = false;
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
            }
            else
            {
                bot.ChangeState(State.Dead);
            }
        }

        public override void Update()
        {
            //// 애니메이션이 끝났는지 확인
            //// GetCurrentAnimatorStateInfo(0) 현재 재생 중인 애니메이션 상태 정보 가져오는 메서드
            //// IsName 현재 재생 중인 애니메이션이 이름이 맞는지 확인
            //// normalizedTime은 애니메이션이 재생되는 비율을 나타내며, 0.0은 시작, 1.0은 끝을 의미
            if (bot.animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
                bot.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                bot.animator.SetBool("Hit", false);
                bot.ChangeState(State.Idle); // 애니메이션이 끝나면 Idle 상태로 전환
            }
        }
    }

    private class ShitState : BotState
    {
        public ShitState(BotController bot) : base(bot)
        {
        }

        public override void Enter()
        {
            bot.hp -= 2;
            if (bot.hp > 0)
            {
                bot.animator.SetBool("Shit", true);
                HitManager.Instance.playerAttackCheck = false;
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Shit);
            }
            else
            {
                bot.ChangeState(State.Dead);
            }
        }

        public override void Update()
        {
            if (bot.animator.GetCurrentAnimatorStateInfo(0).IsName("Shit") &&
                bot.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                bot.animator.SetBool("Shit", false);
                bot.ChangeState(State.Idle); // 애니메이션이 끝나면 Idle 상태로 전환
            }
        }
    }

    private class AttackState : BotState
    {
        public AttackState(BotController bot) : base(bot)
        {
        }

        public override void Enter()
        {
            bot.animator.applyRootMotion = true;
            bot.animator.SetBool("Attack", true);
            HitManager.Instance.attackCheck = false;
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
        }

        public override void Update()
        {
            if (bot.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                bot.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                bot.animator.applyRootMotion = false;
                bot.animator.SetBool("Attack", false);
                bot.ChangeState(State.Idle); // 애니메이션이 끝나면 Idle 상태로 전환
            }
        }
    }

    private class DeadState : BotState
    {
        public DeadState(BotController bot) : base(bot)
        {
        }
        public override void Enter()
        {
            bot.animator.SetBool("Dead", true);
        }

        public override void Exit()
        {

        }
    }

    public void BotHit(int damage)
    {
        if (HitManager.Instance.playerAttackCheck == true)
        {
            if (damage == 2)
            {
                ChangeState(State.Shit);
            }
            else if (damage == 1)
            {
                ChangeState(State.Hit);
            }
        }
    }
}
