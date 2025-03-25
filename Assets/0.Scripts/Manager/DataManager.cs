using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        GameManager.Instance.sceneController.StartGame += mainData.ResetLevel;// 게임 시작시 스테이지 레벨 초기화.
    }
}
