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
    // monsterData 와 스테이지 진행도를 토대로 보정된 최종 수치.
    int curtHp;
    int curAttack;
    float curAttackSpeed;
    int curGold;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (monsterData == null)
        {
            Debug.LogError("MonsterData is null");
            Destroy(this.gameObject);
            return;
        }
        curtHp = monsterData.maxHp * GameManager.Instance.sceneController.stageLevel;
        curAttack = monsterData.attack * GameManager.Instance.sceneController.stageLevel;
        curAttackSpeed = monsterData.attackSpeed * GameManager.Instance.sceneController.stageLevel;
        curGold = monsterData.gold * GameManager.Instance.sceneController.stageLevel;
    }// 활성화시 데이터 로드.

    private void Update()
    {
        if(curtHp <= 0)
        {
            // 추후 state 를 dead 로 변경하여 진행하기.
            GameManager.Instance.sceneController.stageLevel++;
            AddReward();// 보상 추가.
            GameManager.Instance.sceneController.StartMoving?.Invoke();
        }

        // 테스트 코드
        if(Input.GetKeyDown(KeyCode.A))
        {
            curtHp -= 10;
        }
    }

    void AddReward()
    {
        DataManager.Instance.mainData.gold += curGold;
    }
}
