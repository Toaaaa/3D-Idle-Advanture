using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    SceneController sceneController;

    [SerializeField] MapDatas mapDatas;// 맵 데이터를 가지고 있는 스크립터블 오브젝트
    [SerializeField] Transform chunkCheckPoint;// 청크 생성 위치를 확인할 위치, 만약 해당 위치에 생성된 청크가 없다면 새로이 청크 배치.
    int layerMask; // 청크 레이어 마스크.

    List<GameObject> chunkList = new List<GameObject>();// 생성된 청크들을 담을 리스트 (오브젝트 풀링)

    private void Awake()
    {
        sceneController = GameManager.Instance.sceneController;
        layerMask = LayerMask.GetMask("Chunk");// 청크 레이어 마스크 == 6번 레이어.
        sceneController.StartGame += SetChunkList;
    }

    private void Update()
    {
        Ray ray = new Ray(chunkCheckPoint.position, chunkCheckPoint.up);
        RaycastHit hit;

        if(!Physics.Raycast(ray, out hit, 20, layerMask) && sceneController.isGameStart)// 청크 생성 위치에 청크가 없다면
        {
            ChunkPlace();
        }
    }

    private void SetChunkList()// 선택한 맵의 모든 청크를 미리 오브젝트 풀에 저장 + 시작 청크 배치.
    {
        foreach(var mapdatas in mapDatas.mapDataList)
        {
            if(mapdatas.mapName == sceneController.selectedMapType.ToString())
            {
                foreach(var chunk in mapdatas.mapChunkList)
                {
                    GameObject newChunk = Instantiate(chunk, new Vector3 (0,0,0), Quaternion.identity);// 청크 생성.
                    chunkList.Add(newChunk);// 생성된 청크를 리스트에 추가.
                    newChunk.SetActive(false);// 생성된 청크를 비활성화.
                }
            }
        }
        InitiateChunk();
    }
    private void InitiateChunk()// 시작시 즉시 배치될 청크 배치.
    {
        if(chunkList.Count < 2)
        {
            Debug.LogError("Chunk data missing");
            return;
        }

        Debug.Log("Initiate Chunk");
        chunkList[0].SetActive(true);
        chunkList[0].transform.position = new Vector3(0, 0, 0);
        chunkList[1].SetActive(true);
        chunkList[1].transform.position = new Vector3(0, 0, 40);// ChunkPlace와 동일하게 추후 Bounds를 활용해 배치 위치 조정.
    }
    private void ChunkPlace()// 청크 배치.
    {
        if(mapDatas.mapDataList.Count == 0)
        {
            Debug.LogError("Map data missing");
            return;
        }

        GameObject chunk = chunkList.Find(x => x.activeSelf == false);// 비활성화된 청크를 찾음.
        if(chunk == null)
        {
            // 오브젝트 풀에 청크가 없다면 새로 생성.
            foreach(var mapdatas in mapDatas.mapDataList)
            {
                if(mapdatas.mapName == sceneController.selectedMapType.ToString())
                {
                    foreach(var chunkData in mapdatas.mapChunkList)
                    {
                        chunk = Instantiate(chunkData, chunkCheckPoint.position, Quaternion.identity);// 청크 생성.
                        chunkList.Add(chunk);// 생성된 청크를 리스트에 추가.
                        chunk.SetActive(false);// 생성된 청크를 비활성화.
                    }
                }
            }
        }
        else
        {
            Debug.Log("Chunk Place");
            int randomIndex = Random.Range(0, chunkList.Count);
            if (chunkList[randomIndex].activeSelf)// 이미 활성화된 청크라면
            {
                ChunkPlace();// 재귀 호출.
                return;
            }
            chunkList[randomIndex].transform.position = new Vector3(chunkCheckPoint.position.x,0, chunkCheckPoint.position.z) + new Vector3(0,0,10);// 개선한다면 chunk 데이터의 bounds를 이용하여 청크의 크기를 고려하여 청크 배치의 위치를 조정 가능할듯.
            chunkList[randomIndex].SetActive(true);// 청크 활성화.
        }
    }
}
