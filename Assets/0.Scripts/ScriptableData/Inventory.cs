using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public ItemData curEquiped;// 현재 장착중인 아이템.
    public List<InventorySlot> slots = new List<InventorySlot>();// 인벤토리 슬롯.

    public void AddItem(ItemData item, int quantity)
    {
        var slot = slots.Find(s => s.itemData == item);
        if (slot != null)// 이미 동일한 아이템이 존재하면 수량 추가.
        {
            slot.amount += quantity;
        }
        else// 동일한 아이템이 없을 시 새로운 슬롯 추가.
        {
            slots.Add(new InventorySlot(item, quantity));
        }
    }

    public void RemoveItem(ItemData item, int quantity)
    {
        var slot = slots.Find(s => s.itemData == item);
        if (slot != null)
        {
            slot.amount -= quantity;// 수량 감소.
            if (slot.amount <= 0)// 만약 0개가 되면 슬롯 제거.
            {
                slots.Remove(slot);
            }
        }
    }

    public int GetPotionCount()
    {
        var slot = slots.Find(s => s.itemData.itemType == ItemType.Potion);
        if (slot != null)
        {
            return slot.amount;
        }
        return 0;
    }

    public void UsePotion(Player p)// 포션 사용시 호출. (GetPotionCount를 선행한뒤 실행.)
    {
        var slot = slots.Find(s => s.itemData.itemType == ItemType.Potion);
        p.Hp += slot.itemData.itemValue;
        slot.amount--;
        GameManager.Instance.uiManager.UpdateUIs?.Invoke();
    }

    /*
    public void SaveInventory()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);
    }

    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
    *///추후 게임 매니저에서 Action을 통해 게임 전역의 모든 저장/로드를 한번에 관리.
}
[Serializable]
public class InventorySlot
{
    public ItemData itemData;
    public int amount;

    public InventorySlot(ItemData _itemData, int _amount)
    {
        itemData = _itemData;
        amount = _amount;
    }
    public InventorySlot()
    {
        itemData = null;
        amount = 0;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
    public void SubAmount(int value)
    {
        amount -= value;
    }
    public ItemData GetItem()
    {
        return itemData;
    }
    public void EquipItem()
    {
        //장비 장착.
    }
    public void UseItem()
    {
        //아이템 사용.
    }
}
