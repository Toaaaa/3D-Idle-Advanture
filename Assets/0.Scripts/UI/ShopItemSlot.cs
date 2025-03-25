using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static MapDatas;

public class ShopItemSlot : MonoBehaviour
{
    public ItemData itemData;
    public TextMeshProUGUI buttonDesc;

    private void Start()
    {
        TextForButton();
    }

    public void OnClickTryBuy()
    {
        if(DataManager.Instance.mainData.goldValue >= itemData.itemPrice)
        {
            DataManager.Instance.mainData.goldValue -= itemData.itemPrice;
            DataManager.Instance.inventoryData.AddItem(itemData, 1);
            if (itemData.itemType == ItemType.Potion)
            {
                DataManager.Instance.mainData.potionCount = DataManager.Instance.inventoryData.GetPotionCount();
            }
            GameManager.Instance.uiManager.UpdateUIs?.Invoke();// UI 갱신 (골드량)
        }
    }
    public void TextForButton()
    {
        if(itemData != null)
            buttonDesc.text = itemData.itemName + "\n" + "Cost : " + itemData.itemPrice;
    }
}
