using UnityEngine;

public class index : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도

    public GameObject moveObject; // 움직일 때 활성화할 객체
    public GameObject stopObject; // 멈출 때 활성화할 객체
    public GameObject riseObject; // 올라가는 모션

    private Rigidbody2D rb;
    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 초기 상태 설정: 플레이어가 멈춘 상태로 시작한다고 가정
        SetMoveState(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 입력 감지. 음수면 좌측, 양수면 우측
        float x = Input.GetAxis("Horizontal");
        
        // 이동 거리 대입
        movement.x = x;

        // 상태 변화 감지에 따른 애니메이션 갱신
        if (movement.sqrMagnitude > 0) {
            SetMoveState(true);
        }

        else {
            SetMoveState(false);
        }

        // 방향전환
        if (x != 0) {
            TurnAnimation(x < 0);
        }
    }

    void FixedUpdate()
    {
        // 플레이어 이동
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // 움직임에 따른 애니메이션 변화
    void SetMoveState(bool isMoving)
    {
        if (isMoving) {
            moveObject.SetActive(true);
            stopObject.SetActive(false);
            riseObject.SetActive(false);
        }

        else {
            moveObject.SetActive(false);
            stopObject.SetActive(true);
            riseObject.SetActive(false);
        }
    }

    // 방향변화
    void TurnAnimation(bool isRight)
    {
        if (isRight) {
            moveObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            stopObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else {
            moveObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            stopObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
