using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Numerics;

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
    [SerializeField] private BigInteger atk = 10;// 공격력.
    public BigInteger Atk
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

    [Header("UI / SFX")]
    public Image hPBar;// 체력바.
    public TextMeshProUGUI hPText;// 체력 텍스트.
    public GameObject reviveFX;// 부활 이펙트.
    public TextMeshProUGUI damagePrint;// 데미지 출력.


    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameManager.Instance.sceneController.StartMoving += SetMoveState;
        GameManager.Instance.sceneController.StopMoving += SetCombatState;
        GameManager.Instance.uiManager.UpdateUIs += UpdateHP;
    }

    private void Start()
    {
        maxHp = DataManager.Instance.playerData.maxHp;
        hp = DataManager.Instance.playerData.curHp;
        atk = DataManager.Instance.playerData.curAtkValue;
        UpdateHP();
        StartCoroutine(StateMachine());// 상태 머신 시작.
    }
    private void Update()
    {
        if (hp <= 0)
        {
            isMoving = false;
            ChangeState(PlayerState.Dead);
        }
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
        // UI상의 체력바 갱신
        hPBar.fillAmount = hp / maxHp;
        hPText.text = $"{(int)hp} / {(int)maxHp}";
        // 데이터 상의 체력 갱신
        DataManager.Instance.playerData.curHp = (int)hp;
        DataManager.Instance.playerData.maxHp = (int)maxHp;
    }
    private void RevivePlayer()// 플레이어 부활.
    {
        maxHp = DataManager.Instance.playerData.maxHp;
        hp = maxHp;
        reviveFX.SetActive(false);
        reviveFX.SetActive(true);
        ChangeState(PlayerState.Combat);
    }
    private void PrintDamage()
    {
        damagePrint.text = atk.ToString();
        damagePrint.gameObject.SetActive(true);
        damagePrint.GetComponent<DamageText>().PlayDamageEffect();// 데미지 출력 + 효과 재생.
    }


    public void AttackMonster()// 애니메이션 이벤트로 호출.
    {
        if(targetMonster == null)
            return;
        targetMonster.ReceiveDamage(atk);
        PrintDamage();
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
            anim.Play("Idle",0,0);// Idle 애니메이션 설정.

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
            anim.Play("Move", 0,0);// Run 애니메이션 설정.

        while (playerState == PlayerState.Move)
        {
            isMoving = true;
            yield return null;
        }
    }
    IEnumerator Combat()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        if(!curAnimState.IsName("Combat"))
            anim.Play("Combat",0,0);// Combat 애니메이션 설정.

        while (playerState == PlayerState.Combat)
        {
            isMoving = false;
            if(targetMonster != null)
            {
                if(targetMonster.curHp > 0)// 몬스터가 살아있을때만 공격.
                    ChangeState(PlayerState.Attack);
            }
            else
            {
                GameManager.Instance.sceneController.StartMoving?.Invoke();// 이동 시작.
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
    IEnumerator Dead()
    {
        var curAnimState = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play("Dead",0,0);

        yield return new WaitForSeconds(10f);// 10초 후 부활.
        RevivePlayer();// 부활.
    }
}
