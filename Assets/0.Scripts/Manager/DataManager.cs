using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Numerics;

public class DataManager : MonoBehaviour
{
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("DataManager");
                    instance = container.AddComponent<DataManager>();
                }
            }
            return instance;
        }
    }

    // 골드, 스테이지 레벨, 플레이어 정보 연동.
    public MainData mainData;
    public PlayerData playerData;
    public Inventory inventoryData;

    // 데이터 저장
    private string savePath => Application.persistentDataPath;

    public void SaveAllData()
    {
        SaveData(mainData, "MainData.json");
        SaveData(playerData, "PlayerData.json");
        SaveData(inventoryData, "InventoryData.json");
        Debug.Log("모든 데이터가 저장되었습니다.");
    }

    public void LoadAllData()
    {
        LoadData(mainData, "MainData.json");
        LoadData(playerData, "PlayerData.json");
        LoadData(inventoryData, "InventoryData.json");
        Debug.Log("모든 데이터가 로드되었습니다.");
    }

    public void DebugData()
    {
        Debug.Log($"[MainData] Gold: {mainData.goldValue}, Map: {mainData.currentMapName}");
        Debug.Log($"[PlayerData] HP: {playerData.curHp}/{playerData.maxHp}, ATK: {playerData.curAtkValue}");
        Debug.Log($"[Inventory] Items: {inventoryData.slots.Count}");
    }
    private void SaveData<T>(T data, string fileName) where T : ScriptableObject
    {
        try
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(Path.Combine(savePath, fileName), json);
            Debug.Log($"{fileName} 저장 완료");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"데이터 저장 중 오류 발생: {fileName}, {ex.Message}");
        }
    }

    private void LoadData<T>(T data, string fileName) where T : ScriptableObject
    {
        string filePath = Path.Combine(savePath, fileName);
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"{fileName} 파일이 존재하지 않습니다. 기본 데이터 사용.");
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            JsonConvert.PopulateObject(json, data);
            ValidateData(data); // 데이터 검증
            Debug.Log($"{fileName} 로드 완료");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"데이터 로드 중 오류 발생: {fileName}, {ex.Message}");
        }
    }

    private void ValidateData<T>(T data)
    {
        switch (data)
        {
            case MainData mainData:
                if (!BigInteger.TryParse(mainData.gold, out _))
                {
                    Debug.LogError("Gold 데이터가 손상되었습니다. 기본값으로 초기화합니다.");
                    mainData.goldValue = BigInteger.Zero;
                }
                break;

            case PlayerData playerData:
                if (!BigInteger.TryParse(playerData.curatk, out _))
                {
                    Debug.LogError("공격력 데이터가 손상되었습니다. 기본값으로 초기화합니다.");
                    playerData.curAtkValue = BigInteger.One; // 기본값 1
                }
                break;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
