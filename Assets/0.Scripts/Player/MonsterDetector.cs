using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetector : MonoBehaviour
{
    // 사정 거리 내에 몬스터가 들어왔을 경우 (레이 캐스팅으로 감지)
    [SerializeField] Player player;

    public void OnDetected()
    {
        //player.targetMonster =  타켓 배정
        GameManager.Instance.sceneController.StopMoving?.Invoke();
    }
}
