using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public enum MapType
    {
        Forest,
        Desert
    }
    public MapType selectedMapType = MapType.Forest;// 선택된 맵의 이름 (숲 , 사막)

    public bool isGameStart = false;// 게임 시작 여부.
    public int stageLevel = 1;// 스테이지 레벨.
    public Queue<GameObject> chunkQueue = new Queue<GameObject>();// 이동중인 청크.
    public float chunkMoveSpeed = 5;// 청크 이동 속도.

    public Action StartMoving;// 몬스터를 처치 or 게임 시작시 =>> 플레이어를 움직이게 함 + 맵 청크 이동.
    public Action StopMoving;// 플레이어가 움직이는 도중 일정 거리에서 몬스터와 인카운터 시 =>> 플레이어를 멈춤 + 맵 청크 정지 + 전투 시작.
    public Action StartGame;// 플레이 할 스테이지를 고르면 게임이 시작된다 =>> 스테이지 맵 청크 생성및 풀에 저장 + 게임 시작.

}
