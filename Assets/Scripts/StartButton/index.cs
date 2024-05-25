using UnityEngine;

public class StartButton : MonoBehaviour
{
    public Sprite startButtonSprite;
    public Sprite startButtonHoverSprite;

    private SpriteRenderer spriteRenderer;
    private bool isHovering = false;

    void Start()
    {
        // SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found!");
            return;
        }

        // 초기 이미지 설정
        spriteRenderer.sprite = startButtonSprite;
    }

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

        if (currentlyHovering && !isHovering)
        {
            // hover 이미지 적용
            spriteRenderer.sprite = startButtonHoverSprite;
            isHovering = true;
        }
        else if (!currentlyHovering && isHovering)
        {
            // 원래 이미지로 되돌림
            spriteRenderer.sprite = startButtonSprite;
            isHovering = false;
        }

        // 클릭 처리
        if (currentlyHovering && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        Debug.Log("StartGame");
        GameManager.Instance.StartIntro1();
    }
}
