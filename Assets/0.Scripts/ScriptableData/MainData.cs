using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainData", menuName = "ScriptableObjects/MainData")]
public class MainData : ScriptableObject
{
    public int gold;
    public int stageLevel;
    // 인벤토리 정보

    public void ResetLevel()
    {
        stageLevel = 1;
    }
}
