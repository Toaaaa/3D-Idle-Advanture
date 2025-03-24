using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

[CreateAssetMenu(fileName = "MainData", menuName = "ScriptableObjects/MainData")]
public class MainData : ScriptableObject
{
    public BigInteger gold;
    public int potionCount;
    public int stageLevel;
    // 인벤토리 정보

    public void ResetLevel()
    {
        stageLevel = 1;
    }
}
