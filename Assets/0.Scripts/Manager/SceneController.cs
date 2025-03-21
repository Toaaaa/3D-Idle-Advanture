using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public Action StartMoving;// 몬스터를 처치 or 게임 시작시 =>> 플레이어를 움직이게 함.
    public Action StopMoving;// 플레이어가 움직이는 도중 일정 거리에서 몬스터와 인카운터 시 =>> 플레이어를 멈춤 + 전투 시작.
}
