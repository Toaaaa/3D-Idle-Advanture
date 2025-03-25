using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Numerics;

public class UIManager : MonoBehaviour
{
    [SerializeField] AudioSource uiSoundSource;
    [SerializeField] ParticleSystem particle;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI potionCountText;
    [SerializeField] GameObject playerStatsPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject shopPanel;

    public Action UpdateUIs;

    private void Awake()
    {
        UpdateUIs += UpdateGoldDisplay;
        UpdateUIs += UpdatePotionCount;
        UpdateUIs?.Invoke();
    }

    public void ButtonPush(RectTransform buttonrect)
    {
        if(uiSoundSource == null)
        {
            Debug.LogError("uiSoundSource is null");
            return;
        }

        // 버튼 에서 소리 재생.
        uiSoundSource.PlayOneShot(uiSoundSource.clip);

        // 버튼의 ui 상의 좌표 > 스크린 좌표 >> 월드 좌표로 변환.
        UnityEngine.Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, buttonrect.position);
        UnityEngine.Vector3 worldPos = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane + 5f));

        // 파티클 생성
        if(particle != null)
        {
            ParticleSystem effect = Instantiate(particle, worldPos, UnityEngine.Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }
    }

    public void UpdateGoldDisplay()
    {
        goldText.text = "Gold : " + FormatBigInteger(DataManager.Instance.mainData.goldValue);
    }
    public void UpdatePotionCount()
    {
        potionCountText.text = $"{DataManager.Instance.mainData.potionCount.ToString()}";
    }
    public void ChooseForestMap(GameObject thisobj)
    {
        GameManager.Instance.sceneController.selectedMapType = SceneController.MapType.Forest;
        DataManager.Instance.mainData.stageLevel = 1;
        DataManager.Instance.mainData.currentMapName = "Forest";
        GameManager.Instance.sceneController.isGameStart = true;
        thisobj.SetActive(false);
        GameManager.Instance.sceneController.StartGame?.Invoke();// 게임 시작.
        GameManager.Instance.sceneController.StartMoving?.Invoke();// 플레이어 이동 시작.
    }
    public void ChooseDesertMap(GameObject thisobj)
    {
        GameManager.Instance.sceneController.selectedMapType = SceneController.MapType.Desert;
        DataManager.Instance.mainData.stageLevel = 1;
        DataManager.Instance.mainData.currentMapName = "Desert";
        GameManager.Instance.sceneController.isGameStart = true;
        thisobj.SetActive(false);
        GameManager.Instance.sceneController.StartGame?.Invoke();// 게임 시작.
        GameManager.Instance.sceneController.StartMoving?.Invoke();// 플레이어 이동 시작.
    }
    public void OpenPlayerStats()
    {
        if(!inventoryPanel.activeSelf && !shopPanel.activeSelf)
            playerStatsPanel.SetActive(true);
    }
    public void OpenInventoryPanel()
    {
        if(!playerStatsPanel.activeSelf && !shopPanel.activeSelf)
            inventoryPanel.SetActive(true);
    }
    public void OpenShopPanel()
    {
        if(!playerStatsPanel.activeSelf && !inventoryPanel.activeSelf)
            shopPanel.SetActive(true);
    }
    public void ExitGameButton()
    {
        // 데이터 저장.

        // 게임 종료.
    }

    // BigInteger 변환
    private string FormatBigInteger(BigInteger value)
    {
        if (value < 1000)
        {
            return value.ToString();
        }

        int suffixIndex = 0;
        BigInteger divisor = new BigInteger(10000);

        while (value >= divisor && suffixIndex < suffixes.Length - 1)
        {
            value /= divisor;
            suffixIndex++;
        }

        string formattedValue = value.ToString("F2");
        return $"{formattedValue}{suffixes[suffixIndex]}";
    }
    private string[] suffixes =
    {
        "","a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"
    };
}
