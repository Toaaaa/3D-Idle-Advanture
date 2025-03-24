using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

[CreateAssetMenu(fileName = "MainData", menuName = "ScriptableObjects/MainData")]
public class MainData : ScriptableObject
{
    [SerializeField] string gold;
    public BigInteger goldValue
    {
        get
        {
            if (BigInteger.TryParse(gold, out BigInteger result))
            {
                return result;
            }
            Debug.LogError($"Invalid gold value: {gold}");
            return BigInteger.Zero;
        }
        set
        {
            gold = value.ToString("0");
        }
    }

    public int potionCount;
    public int stageLevel;
    // 인벤토리 정보

    public void ResetLevel()
    {
        stageLevel = 1;
    }
}
