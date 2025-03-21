using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunk : MonoBehaviour
{
    float moveSpeed = 5;

    private void Awake()
    {
        moveSpeed = GameManager.Instance.sceneController.chunkMoveSpeed;
    }

    private void Update()
    {
        if(GameManager.Instance.sceneController.isGameStart)
            MoveChunkToZ();
    }
    IEnumerator MoveChunkToZ()
    {
        while (true)
        {
            if (transform.position.z < -15)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 10);
            }
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);// moveSpeed 만큼 청크를 뒤로 이동.
            yield return null;
        }
    }
}
