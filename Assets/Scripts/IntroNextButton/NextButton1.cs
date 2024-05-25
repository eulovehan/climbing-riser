using UnityEngine;

public class NextButton1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Update()
    {
        // 마우스 hover 색상 변경
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
            StartIntro2();
        }
    }

    void StartIntro2()
    {
        GameManager.Instance.StartIntro2();
    }
}
