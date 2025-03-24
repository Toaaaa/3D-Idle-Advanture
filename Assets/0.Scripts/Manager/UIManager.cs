using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField] AudioSource uiSoundSource;
    [SerializeField] ParticleSystem particle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ButtonPush(RectTransform buttonrect)
    {
        if(uiSoundSource == null)
        {
            Debug.LogError("uiSoundSource is null");
            return;
        }

        // 버튼 에서 소리 재생.
        uiSoundSource.PlayOneShot(uiSoundSource.clip);

        // 버튼의 ui 상의 좌표 > 스크린 좌표 >> 월드 좌표로 변환.
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, buttonrect.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane + 5f));

        // 파티클 생성
        if(particle != null)
        {
            ParticleSystem effect = Instantiate(particle, worldPos, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }

    }
}
