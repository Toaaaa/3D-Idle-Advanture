using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetector : MonoBehaviour
{
    // 사정 거리 내에 몬스터가 들어왔을 경우 (레이 캐스팅으로 감지)
    [SerializeField] Player player;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 4, LayerMask.GetMask("Monster")) && player.targetMonster == null)
        {
            OnDetected(hit.collider.GetComponent<Monster>());
        }
    }

    public void OnDetected(Monster monster)
    {
        player.targetMonster = monster;
        GameManager.Instance.sceneController.StopMoving?.Invoke();
    }
}
