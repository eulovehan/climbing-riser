using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NextButton4 : MonoBehaviour
{
    public AudioSource sirenAudioSource;
    public AudioSource footstepAudioSource;
    public UIFadeOutManager fadeOutManager;
    public float fadeDuration = 6.0f;  // 페이드 아웃 지속 시간을 6초로 설정

    private Collider2D collider;
    private bool currentlyHovering = false;

    void Start()
    {
        // 오디오 소스 설정
        sirenAudioSource = gameObject.AddComponent<AudioSource>();
        footstepAudioSource = gameObject.AddComponent<AudioSource>();

        // 오디오 클립 로드 및 할당
        AudioClip sirenClip = Resources.Load<AudioClip>("Audio/siren2");
        AudioClip footstepClip = Resources.Load<AudioClip>("Audio/running");

        sirenAudioSource.clip = sirenClip;
        sirenAudioSource.playOnAwake = false;  // 처음에 재생하지 않도록 설정
        sirenAudioSource.volume = 1.0f;  // 사이렌 소리 볼륨 설정

        footstepAudioSource.clip = footstepClip;
        footstepAudioSource.playOnAwake = false;  // 처음에 재생하지 않도록 설정
        footstepAudioSource.volume = 1.5f;  // 발자국 소리 볼륨 설정
        footstepAudioSource.loop = true;

        // UI 요소 세팅
        fadeOutManager = FindObjectOfType<UIFadeOutManager>();

        // Collider 설정
        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("Collider2D component is missing on NextButton4.");
        }
    }

    void Update()
    {
        HandleMouseHover();
    }

    void HandleMouseHover()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (collider == null) return;

        // 마우스가 콜라이더 안에 있는지 확인
        currentlyHovering = collider.OverlapPoint(mousePosition);

        // 클릭 처리
        if (currentlyHovering && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HandleButtonClick());
        }
    }

    private IEnumerator HandleButtonClick()
    {
        // 사이렌 소리 재생
        if (sirenAudioSource != null && sirenAudioSource.clip != null)
        {
            sirenAudioSource.Play();
        }

        // 걷는 소리 재생
        if (footstepAudioSource != null && footstepAudioSource.clip != null)
        {
            footstepAudioSource.Play();
        }

        // 페이드 아웃 시작
        if (fadeOutManager != null)
        {
            yield return StartCoroutine(fadeOutManager.FadeOutAllUI(fadeDuration));
        }

        // 씬 전환
        GameManager.Instance.StartGame();
    }
}
