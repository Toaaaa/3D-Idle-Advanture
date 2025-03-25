using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour, IPointerClickHandler
{
    //플레이어의 장비 장착 UI
    public ItemData weaponData;// 현재 장착중인 장비의 데이터
    [SerializeField] Image weaponSprite;// 무기 스프라이트 렌더러

    private void Awake()
    {
        if(GameManager.Instance.uiManager.inventoryUI._inventory.curEquiped != null)
            weaponData = GameManager.Instance.uiManager.inventoryUI._inventory.curEquiped;
        GameManager.Instance.uiManager.UpdateUIs += SpriteUpdate;
    }
    private void Start()
    {
        SpriteUpdate();
    }
    void SpriteUpdate()
    {
        if(weaponData != null)
            weaponSprite.sprite = weaponData.itemImage;
        else 
            weaponSprite.sprite = null;
    }

    public void OnClickUnequip()
    {
        if(weaponData != null)// 만약 장착중인 무기가 있으면, 해제.
        {
            GameManager.Instance.uiManager.inventoryUI._inventory.AddItem(weaponData, 1);// 인벤토리에 해제하는 아이템 추가.
            GameManager.Instance.player.Atk -= weaponData.itemValue;// 공격력 감소.
            weaponData = null;// 장비 해제.
            GameManager.Instance.uiManager.inventoryUI._inventory.curEquiped = null;// scriptable object에 현재 장착중인 아이템 갱신.
            GameManager.Instance.uiManager.UpdateUIs?.Invoke();
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
