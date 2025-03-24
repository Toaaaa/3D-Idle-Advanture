using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using static Player;

public class Monster : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Combat,
        Attack,
        Dead,
    }

    [Header("Monster Status")]
    [SerializeField] MonsterState monsterState = MonsterState.Idle;
    Animator anim;


    [Header("Monster Data")]
    [SerializeField] MonsterData monsterData;
    // monsterData 와 스테이지 진행도를 토대로 보정된 최종 수치.
    public BigInteger curHp;
    int curAttack;
    float curAttackSpeed;
    BigInteger curGold;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameManager.Instance.sceneController.StopMoving += SetCombatState;
    }
    private void Start()
    {
        StartCoroutine(StateMachine());
    }

    private void OnEnable()
    {
        if (monsterData == null)
        {
            Debug.LogError("MonsterData is null");
            Destroy(this.gameObject);
            return;
        }
        monsterState = MonsterState.Idle;
        StartCoroutine(StateMachine());
        curHp = monsterData.maxHp * BigInteger.Pow(2, GameManager.Instance.sceneController.stageLevel);
        curHp /= 2;
        curAttack = monsterData.attack * GameManager.Instance.sceneController.stageLevel;
        curAttackSpeed = monsterData.attackSpeed;
        curGold = monsterData.gold * BigInteger.Pow(10, GameManager.Instance.sceneController.stageLevel);
    }// 활성화시 데이터 로드.

    public void ChangeState(MonsterState newState)// 플레이어 상태 변경.
    {
        monsterState = newState;
    }
    void SetCombatState()
    {
        if(GameManager.Instance.player.targetMonster == this)
            ChangeState(MonsterState.Combat);
    }
    void AddReward()
    {
        DataManager.Instance.mainData.gold += curGold;
    }
    void AttackPlayer()
    {
        GameManager.Instance.player.ReceiveDamage(curAttack);
        Camera.main.GetComponent<CameraAction>().ShakeCamera(0.5f, 0.5f, 10);
    }
    public void ReceiveDamage(BigInteger dmg)
    {
        curHp -= dmg;

        if (curHp <= 0)// 사망 판정 체크.
        {
            Debug.Log("Monster Dead");
            ChangeState(MonsterState.Dead);
            Debug.Log("Monster Dead");
            GameManager.Instance.player.targetMonster = null;// 타겟 몬스터 초기화.
            GameManager.Instance.sceneController.stageLevel++;
            AddReward();// 보상 추가.
        }
    }

    IEnumerator StateMachine()
    {
        while (true)
            yield return StartCoroutine(monsterState.ToString());
    }
    IEnumerator Idle()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if (!curAnimState.IsName("Idle"))
            anim.Play("Idle", 0, 0);// Idle 애니메이션 설정.

        while (monsterState == MonsterState.Idle)
        {
            yield return null;
        }
    }
    IEnumerator Combat()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if (!curAnimState.IsName("Combat"))
            anim.Play("Combat", 0, 0);// Combat 애니메이션 설정.

        while (monsterState == MonsterState.Combat)
        {
            if(curHp > 0)
            {
                ChangeState(MonsterState.Attack);
            }
            else
            {
                ChangeState(MonsterState.Dead);
            }
            yield return null;
        }
    }
    IEnumerator Attack()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if(!curAnimState.IsName("Attack"))
            anim.Play("Attack", 0, 0);// Combat 애니메이션 설정.
        
        while (monsterState == MonsterState.Attack)
        {
            yield return new WaitForSeconds(curAttackSpeed);
        }
    }
    IEnumerator Dead()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if (!curAnimState.IsName("Dead"))
        {
            anim.Play("Dead", 0, 0);
            yield return null;// 1프레임 대기.
            curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        }

        yield return new WaitForSeconds(curAnimState.length);// 애니메이션 재생 후 사망 판정.
        GameManager.Instance.player.targetMonster = null;// 타겟 몬스터 초기화.
        GameManager.Instance.sceneController.stageLevel++;
        AddReward();// 보상 추가.
        this.gameObject.SetActive(false);
        GameManager.Instance.sceneController.StartMoving?.Invoke();
    }
}
