using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/MonsterData", order = 1)]
public class MonsterData : ScriptableObject
{
    // hp,attack,gold 등의 최종 수치는 스테이지 진행도에 따라 보정.
    public string monsterName;
    public int maxHp;
    public int attack;
    public float attackSpeed;
    public int gold;
}
