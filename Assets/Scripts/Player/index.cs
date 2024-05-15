using UnityEngine;

public class index : MonoBehaviour
{
    // public objects
    public float moveSpeed = 5f; // 이동 속도

    public GameObject moveObject; // 움직일 때 활성화할 객체
    public GameObject stopObject; // 멈출 때 활성화할 객체
    public GameObject riseObject; // 올라가는 모션
    public Transform leftGrab; // 왼쪽 그랩 위치
    // public Transform rightGrab; // 오른쪽 그랩 위치
    public Transform leftHand; // 왼손
    public Transform rightHand; // 오른손
    
    // states
    // 올라가는 상태
    public bool isRise = false;

    // components
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isRising = false;

    // inner type
    // 움직임 상태
    private enum ActionState {
        Move,
        Stop,
        Rise
    }

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 움직임 전환
        SetMoveState();

        // 올라가는 상태 설정
        SetRise();
    }

    void FixedUpdate()
    {
        // 플레이어 이동
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // 움직임 상태 설정
    void SetMoveState()
    {
        // 입력 감지. 음수면 좌측, 양수면 우측
        float x = Input.GetAxis("Horizontal");
        
        // 이동 거리 대입
        movement.x = x;

        // 올라가는 상태일 시 올라가는 상태로 고정
        if (isRise) {
            SetActionAnimation(ActionState.Rise);
            return;
        }

        // 상태 변화 감지에 따른 애니메이션 갱신
        if (movement.sqrMagnitude > 0) {
            SetActionAnimation(ActionState.Move);
        }

        else {
            SetActionAnimation(ActionState.Stop);
        }

        // 움직임이 있을 경우 알맞는 방향으로 방향전환
        if (x != 0) {
            TurnAnimation(x < 0);
        }
    }

    // 움직임에 따른 애니메이션 변화
    void SetActionAnimation(ActionState actionState)
    {
        switch (actionState) {
            case ActionState.Move: {
                moveObject.SetActive(true);
                stopObject.SetActive(false);
                riseObject.SetActive(false);
                break;
            }

            case ActionState.Stop: {
                moveObject.SetActive(false);
                stopObject.SetActive(true);
                riseObject.SetActive(false);
                break;
            }

            case ActionState.Rise: {
                moveObject.SetActive(false);
                stopObject.SetActive(false);
                riseObject.SetActive(true);
                break;
            }
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

    // 올라가는 상태 설정
    void SetRise() {
        // 올라가지 않는 상태면 중력 재설정
        if (!isRise) {
            rb.gravityScale = 20f;
            return;
        }

        // 그립 위치
        Vector3 holdPosition = leftGrab.position;
        holdPosition.z = 0f;
    
        Debug.Log("올라가는 중 " + holdPosition);

        // 왼손과 그립 위치 간의 방향 계산
        Vector3 direction = holdPosition - leftHand.position;

        // 예상 회전 위치
        Vector3 expectedRotation = direction.normalized;

        leftHand.right = expectedRotation;
        rb.gravityScale = 0f;

        RiseMove(leftGrab);
    }

    // 올라가기 (플레이어)
    void RiseMove(Transform targetObject) {
        if (isRising) {
            return;
        }

        Debug.Log("올라가는 중");

        // 대상 오브젝트와의 거리 계산
        float distance = Vector3.Distance(transform.position, targetObject.position);

        // 대상 오브젝트와 일정 거리 이내에 있을 때까지 이동
        if (distance > 2.05f)
        {
            // 대상 오브젝트 방향으로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetObject.position, moveSpeed * Time.deltaTime);
        }
        
        else
        {
            // 이동 완료 후 이동 여부 플래그를 false로 설정
            // isMoving = false;
            // isRising = true;
        }
    }
}
