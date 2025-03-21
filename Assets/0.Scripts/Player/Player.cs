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
    enum PlayerState
    {
        Idle,
        Move,
        Combat,
        Dead
    }
    // 기타 컴포넌트
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameManager.Instance.sceneController.StartMoving += SetMoveState;
        GameManager.Instance.sceneController.StopMoving += SetCombatState;
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
}
