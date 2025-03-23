using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private int atk = 10;// 공격력.
    public int Atk
    {
        get => atk;
        set => atk = value;
    }

    [Header("Player State")]
    [SerializeField] PlayerState playerState = PlayerState.Idle;
    public bool isMoving = false;
    public enum PlayerState
    {
        Idle,
        Move,
        Combat,
        Attack,
        Dead
    }
    // 기타 컴포넌트
    Animator anim;

    [Header("Combat")]
    public Monster targetMonster;// 타겟 몬스터.

    [Header("UI")]
    public Image HPBar;// 체력바.
    public TextMeshProUGUI HPText;// 체력 텍스트.


    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameManager.Instance.sceneController.StartMoving += SetMoveState;
        GameManager.Instance.sceneController.StopMoving += SetCombatState;
    }

    private void Start()
    {
        UpdateHP();
        StartCoroutine(StateMachine());// 상태 머신 시작.
    }
    private void Update()
    {
        //테스트 코드
        if(Input.GetKeyDown(KeyCode.F1))
            UpdateHP();
    }


    
    public void ChangeState(PlayerState newState)// 플레이어 상태 변경.
    {
        playerState = newState;
    }
    private void SetMoveState()
    {
        ChangeState(PlayerState.Move);
    }// action 이벤트로 호출.
    private void SetCombatState()
    {
        ChangeState(PlayerState.Combat);
    }// action 이벤트로 호출.
    private void UpdateHP()// 체력 상태 갱신.
    {
        HPBar.fillAmount = hp / maxHp;
        HPText.text = $"{(int)hp} / {(int)maxHp}";
    }

    public void AttackMonster()// 애니메이션 이벤트로 호출.
    {
        if(targetMonster == null)
            return;
        targetMonster.ReceiveDamage(atk);
    }
    public void ReceiveDamage(int damage)// 몬스터로부터 데미지를 받음.
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
            ChangeState(PlayerState.Dead);
        }
        UpdateHP();// 체력 갱신.
    }

    IEnumerator StateMachine()
    {
        while (true)
            yield return StartCoroutine(playerState.ToString());
    }
    IEnumerator Idle()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        
        if(!curAnimState.IsName("Idle"))
            anim.Play("Idle",0,0);// Idle 애니메이션 상태 유지.

        while (playerState == PlayerState.Idle)
        {
            isMoving = false;
            yield return null;
        }
    }
    IEnumerator Move()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);

        if(!curAnimState.IsName("Move"))
            anim.Play("Move", 0,0);// Run 애니메이션 상태 유지.

        while (playerState == PlayerState.Move)
        {
            isMoving = true;
            yield return null;
        }
    }
    IEnumerator Combat()
    {
        while (playerState == PlayerState.Combat)
        {
            isMoving = false;
            if(targetMonster != null)
            {
                ChangeState(PlayerState.Attack);
            }
            yield return null;
        }
    }
    IEnumerator Attack()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play("Attack",0,0);

        yield return new WaitForSeconds(2f);// 공격 속도.
    }
}
