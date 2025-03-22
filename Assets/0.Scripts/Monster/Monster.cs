using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    enum MonsterState
    {
        Idle,
        Attack,
    }

    [Header("Monster Status")]
    [SerializeField] MonsterState monsterState = MonsterState.Idle;
    Animator anim;


    [Header("Monster Data")]
    [SerializeField] MonsterData monsterData;
    int curtHp;
    int curAttack;
    float curAttackSpeed;
    int curGold;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if(monsterData == null)
        {
            Debug.LogError("MonsterData is null");
            Destroy(this.gameObject);
            return;
        }
        curtHp = monsterData.maxHp * GameManager.Instance.sceneController.stageLevel;
        curAttack = monsterData.attack * GameManager.Instance.sceneController.stageLevel;
        curAttackSpeed = monsterData.attackSpeed * GameManager.Instance.sceneController.stageLevel;
        curGold = monsterData.gold * GameManager.Instance.sceneController.stageLevel;
    }// 생성시 데이터 로드.

    private void Update()
    {
        if(curtHp <= 0)
        {
            GameManager.Instance.sceneController.stageLevel++;
            AddReward();// 보상 추가.
            GameManager.Instance.sceneController.StartMoving?.Invoke();
        }
    }

    void AddReward()
    {
        // 경험치, 골드 보상 획득.
    }
}
