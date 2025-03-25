using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    public int SlotIndex { get; private set; } = -1;
    public int Amount => _inventorySlot.amount;
    public Image ItemSprite;
    public TextMeshProUGUI AmountText;
    public InventorySlot _inventorySlot;// 슬롯 데이터.

    private void Awake()
    {
        GameManager.Instance.uiManager.UpdateUIs += SetItemSprite;
    }
    private void Start()
    {
        UpdateAmount();// action에서 관리 X
        SetItemSprite();
    }
    public void SetSlotIndex(int index)
    {
        SlotIndex = index;
    }
    public void UpdateAmount()// 아이템 갯수 갱신.
    {
        if (GameManager.Instance.uiManager.inventoryUI._inventory.slots.Count == SlotIndex)
            _inventorySlot = new InventorySlot(null, 0);
        if (_inventorySlot.itemData != null)
            AmountText.text = _inventorySlot.amount.ToString();
        else
            AmountText.text = "";
    }// action 에서 실행.
    public void SetItemSprite()
    {
        if(_inventorySlot.itemData != null)// 아이템 존재시, 스프라이트 교체.
        {
            ItemSprite.sprite = _inventorySlot.itemData.itemImage;
        }
        else//아이템 없으면 스프라이트 제거.
        {
            ItemSprite.sprite = null;
        }
        
    }

    public void OnClick()// 슬롯의 아이템 사용시.
    {
        if(_inventorySlot.itemData !=null)// 슬롯에 아이템이 있을 경우.
        {
            if(_inventorySlot.itemData.itemType == ItemType.Potion)// 포션일 경우.
            {
                // 포션의 경우 아무런 상호작용 없음.
            }
            else// 무기 인 경우
            {
                //현재 착용중인 무기를 해제하고 장착.
                if(GameManager.Instance.uiManager.playerEquipment.weaponData != null)
                {
                    GameManager.Instance.uiManager.inventoryUI._inventory.AddItem(GameManager.Instance.uiManager.playerEquipment.weaponData, 1);// 인벤토리에 해제하는 아이템 추가.
                    GameManager.Instance.player.Atk -= GameManager.Instance.uiManager.playerEquipment.weaponData.itemValue;// 공격력 감소.
                    GameManager.Instance.uiManager.playerEquipment.weaponData = _inventorySlot.itemData;// 장비 장착 (하면서 기존의 데이터는 없어짐).
                    GameManager.Instance.player.Atk += GameManager.Instance.uiManager.playerEquipment.weaponData.itemValue;// 공격력 증가.
                    GameManager.Instance.uiManager.inventoryUI._inventory.RemoveItem(_inventorySlot.itemData, 1);// 인벤토리에서 장착하는 아이템 제거.
                    GameManager.Instance.uiManager.inventoryUI._inventory.curEquiped = _inventorySlot.itemData;// scriptable object에 현재 장착중인 아이템 저장.
                }
                else// 무기를 장착.
                {
                    GameManager.Instance.uiManager.playerEquipment.weaponData = _inventorySlot.itemData;// 장비 장착.
                    GameManager.Instance.player.Atk += GameManager.Instance.uiManager.playerEquipment.weaponData.itemValue;// 공격력 증가.
                    GameManager.Instance.uiManager.inventoryUI._inventory.RemoveItem(_inventorySlot.itemData, 1);// 인벤토리에서 장착하는 아이템 제거.
                    GameManager.Instance.uiManager.inventoryUI._inventory.curEquiped = _inventorySlot.itemData;// scriptable object에 현재 장착중인 아이템 저장.
                }
            }
            GameManager.Instance.uiManager.UpdateUIs?.Invoke();// UI 갱신.
        }
    }

    public void SetItemData(InventorySlot slotdata)
    {
        _inventorySlot = slotdata;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
