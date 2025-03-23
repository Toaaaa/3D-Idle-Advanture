using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public RectTransform rect;

    void Start()
    {
        PlayDamageEffect();
    }


    public void PlayDamageEffect()
    {
        Vector2 startPos = rect.anchoredPosition; // UI의 로컬 위치 사용
        float floatUpDistance = 50f;
        float floatDownDistance = 30f;
        float duration = 0.8f;

        // 1️. 위로 이동 (anchoredPosition 사용)
        rect.DOAnchorPosY(startPos.y + floatUpDistance, duration * 0.4f)
            .SetEase(Ease.OutQuad);

        // 2️. 좌우로 랜덤 흔들림 추가
        rect.DOAnchorPosX(startPos.x + Random.Range(-20f, 20f), duration * 0.4f)
            .SetEase(Ease.OutQuad);

        // 3️. 아래로 떨어지기
        rect.DOAnchorPosY(startPos.y - floatDownDistance, duration * 0.6f)
            .SetEase(Ease.InQuad)
            .SetDelay(duration * 0.4f);

        // 4. 투명도 서서히 감소 후 제거
        textMesh.DOFade(0, duration * 0.5f)
            .SetEase(Ease.InQuad)
            .SetDelay(duration * 0.5f)
            .OnComplete(() =>
            {
                textMesh.alpha = 1;// 투명도 초기화.
                gameObject.SetActive(false);
            });
    }
}
