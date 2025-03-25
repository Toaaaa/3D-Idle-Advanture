using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int maxHp =100;
    public int curHp =100;
    public string curatk ="10";
    public BigInteger curAtkValue
    {
        get
        {
            if (BigInteger.TryParse(curatk, out BigInteger result))
            {
                return result;
            }
            Debug.LogError($"Invalid gold value: {curatk}");
            return BigInteger.Zero;
        }
        set
        {
            curatk = value.ToString("0");
        }
    }

    public int HpUpgradeCount;
    public int AtkUpgradeCount;

    public string playerState;// 플레이어의 enum 상태를 문자열로 저장. (enum.tryparse 사용)
}
