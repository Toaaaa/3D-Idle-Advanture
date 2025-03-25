using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapChunk : MonoBehaviour
{
    SceneController sceneController;
    Player player;
    [SerializeField] Monster monsterInChunk;// 해당 청크에 포함된 몬스터.
    [SerializeField] int ChunkID;
    float moveSpeed = 5;
    Coroutine move;

    private void Awake()
    {
        sceneController = GameManager.Instance.sceneController;
        player = GameManager.Instance.player;
        moveSpeed = sceneController.chunkMoveSpeed;
    }

    private void FixedUpdate()
    {
        if(sceneController.isGameStart && player.isMoving)
        {
            if(move == null)
                move = StartCoroutine(MoveChunkToZ());
        }
        else
        {
            if(move != null)
            {
                StopCoroutine(move);
                move = null;
            }
        }
    }
    void OnEnable()
    {
        sceneController.chunkQueue.Enqueue(this.gameObject);
        if(this != sceneController.chunkQueue.Peek())// 청크 큐의 첫번째 청크가 아니라면 뒤틀림 방지 추가 코드.
            this.transform.position = sceneController.chunkQueue.Peek().transform.position + new Vector3(0, 0, 40);

        if(sceneController.isGameStart && player.isMoving)
            move = StartCoroutine(MoveChunkToZ());

        if(monsterInChunk != null)
            monsterInChunk.gameObject.SetActive(true);

    }
    void OnDisable()
    {
        sceneController.chunkQueue.Dequeue();
    }

    IEnumerator MoveChunkToZ()
    {
        while (sceneController.isGameStart && player.isMoving)
        {
            if (transform.position.z < - 40)
            {
                this.gameObject.SetActive(false);// 청크가 일정 위치 뒤로 이동하면 비활성화.
            }
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);// moveSpeed 만큼 청크를 뒤로 이동.
            yield return null;
        }
    }
}
