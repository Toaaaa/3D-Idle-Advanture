using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableData/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public int itemValue;
    public int itemPrice;
    public int maxStack = 1;//아이템 최대 스택 수 (weapon ==1, potion == 9999)
    public Sprite itemImage;

}

public enum ItemType
{
    Weapon,
    Potion,
}
