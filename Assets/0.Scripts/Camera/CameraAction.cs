using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraAction : MonoBehaviour
{
    Vector3 oriPos;// 초기 위치.

    private void Awake()
    {
        oriPos = transform.position;
    }

    public void ShakeCamera(float duration, float strength, int vibrato)
    {
        transform.DOShakePosition(duration, strength, vibrato)
            .OnComplete(() => transform.position = oriPos);
    }
}
