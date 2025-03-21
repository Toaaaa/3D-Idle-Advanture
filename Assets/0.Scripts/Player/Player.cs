using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] private float maxHp = 100;// 최대 체력.
    public float MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }
    [SerializeField] private float hp = 100;// 현재 체력.
    public float Hp
    {
        get => hp;
        set => hp = value;
    }
    [SerializeField] private float attack = 10;// 공격력.
    public float Attack
    {
        get => attack;
        set => attack = value;
    }

    [Header("Player State")]
    [SerializeField] PlayerState playerState = PlayerState.Idle;
    public bool isMoving = false;
    enum PlayerState
    {
        Idle,
        Move,
        Combat,
        Dead
    }
    // 기타 컴포넌트
    Animator anim;

    [Header("Combat")]
    public Monster targetMonster;// 타겟 몬스터.

    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameManager.Instance.sceneController.StartMoving += SetMoveState;
        GameManager.Instance.sceneController.StopMoving += SetCombatState;
    }

    private void Start()
    {
        StartCoroutine(StateMachine());// 상태 머신 시작.
    }
    private void Update()
    {
        //테스트 코드
        if(Input.GetKeyDown(KeyCode.F1))
            ChangeState(PlayerState.Idle);
        if(Input.GetKeyDown(KeyCode.F2))
            ChangeState(PlayerState.Move);
    }


    
    private void ChangeState(PlayerState newState)// 플레이어 상태 변경.
    {
        playerState = newState;
    }
    private void SetMoveState()
    {
        ChangeState(PlayerState.Move);
    }
    private void SetCombatState()
    {
        ChangeState(PlayerState.Combat);
    }

    IEnumerator StateMachine()
    {
        while (true)
            yield return StartCoroutine(playerState.ToString());
    }
    IEnumerator Idle()
    {
        while (playerState == PlayerState.Idle)
        {
            anim.SetBool("IsMove", false);
            isMoving = false;
            yield return null;
        }
    }
    IEnumerator Move()
    {
        while (playerState == PlayerState.Move)
        {
            anim.SetBool("IsMove", true);
            isMoving = true;
            yield return null;
        }
    }
}
