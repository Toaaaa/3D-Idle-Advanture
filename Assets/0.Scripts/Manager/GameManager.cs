using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    DataManager dataManager;
    public UIManager uiManager;
    public SceneController sceneController;
    public Player player;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            dataManager = GetComponent<DataManager>();
            uiManager = GetComponent<UIManager>();
            dataManager.LoadAllData();// 데이터 로드
            dataManager.DebugData();
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }       
    }

}
