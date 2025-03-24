using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Numerics;

public class PlayerStatUI : MonoBehaviour
{
    DataManager dataManager;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atkText;

    public void Awake()
    {
        dataManager = DataManager.Instance;
        UpdateStatUI();
        GameManager.Instance.uiManager.UpdateUIs += UpdateStatUI;
    }


    public void TryUpgradeHP()
    {
        if(dataManager.playerData.HpUpgradeCount * 10 <= dataManager.mainData.goldValue)
        {
            dataManager.mainData.goldValue -= dataManager.playerData.HpUpgradeCount * 10;
            dataManager.playerData.maxHp += 10;
            GameManager.Instance.player.MaxHp += 10;
            dataManager.playerData.HpUpgradeCount++;
            GameManager.Instance.uiManager.UpdateUIs();//UI 갱신
        }
    }
    public void TryUpgradeATK()
    {
        if(dataManager.playerData.AtkUpgradeCount * 10 <= dataManager.mainData.goldValue)
        {
            dataManager.mainData.goldValue -= dataManager.playerData.AtkUpgradeCount * 10;
            dataManager.playerData.curAtkValue += 10;
            GameManager.Instance.player.Atk += 10;
            dataManager.playerData.AtkUpgradeCount++;
            UpdateStatUI();
            //플레이어에 데이터 적용
            GameManager.Instance.uiManager.UpdateUIs();//UI 갱신
        }
    }

    void UpdateStatUI()
    {
        hpText.text = $"Current : {dataManager.playerData.maxHp}\n Cost : {dataManager.playerData.HpUpgradeCount * 10}";
        atkText.text = $"Current : {dataManager.playerData.curAtkValue}\n Cost : {dataManager.playerData.AtkUpgradeCount * 10}";
    }
    public void CloseUI()
    {
        if (gameObject != null)
            gameObject.SetActive(false);
    }
}
