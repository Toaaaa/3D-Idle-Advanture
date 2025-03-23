using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int maxHp;
    public int curHp;
    public int curatk;

    public string playerState;// 플레이어의 enum 상태를 문자열로 저장. (enum.tryparse 사용)
}
