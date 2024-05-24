using UnityEngine;

public class NextButton2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Update()
    {
        HandleMouseHover();
    }

    void HandleMouseHover()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = GetComponent<Collider2D>();

        if (collider == null) return;

        // 마우스가 콜라이더 안에 있는지 확인
        bool currentlyHovering = collider.OverlapPoint(mousePosition);

        // 클릭 처리
        if (currentlyHovering && Input.GetMouseButtonDown(0))
        {
            StartIntro3();
        }
    }

    void StartIntro3()
    {
        GameManager.Instance.StartIntro3();
    }
}
