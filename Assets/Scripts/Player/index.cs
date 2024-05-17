using System;
using Unity.VisualScripting;
using UnityEngine;

public class index : MonoBehaviour
{
    // public objects
    public float moveSpeed = 15f; // 이동 속도

    public GameObject moveObject; // 움직일 때 활성화할 객체
    public GameObject stopObject; // 멈출 때 활성화할 객체
    public GameObject riseObject; // 올라가는 모션
    public Transform leftGrab; // 왼쪽 그랩 위치
    public Transform rightGrab; // 오른쪽 그랩 위치
    public Transform leftHand; // 왼손
    public Transform rightHand; // 오른손

    public AudioSource audioSource; // 오디오 소스
    public AudioClip moveSound; // 움직일 때 재생할 소리
    
    // states
    private Vector3 leftTouchTarget; // 현재 잡고있는 오브젝트 좌표
    private Vector3 rightTouchTarget; // 현재 잡고있는 오브젝트 좌표
    private bool isLeftGrab; // 왼쪽 손 그랩 여부
    private bool isRightGrab; // 오른쪽 손 그랩 여부

    // components
    private Rigidbody2D rb;
    private Vector2 movement;
   

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

        audioSource = GetComponent<AudioSource>();
        moveSound = Resources.Load<AudioClip>("Audio/moveSound");
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
        // 올라가는 상태일 시 움직임 불가
        if (isLeftGrab || isRightGrab) {
            return;
        }
        
        // 플레이어 이동
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // 움직임 상태 설정
    void SetMoveState()
    {
        // 올라가는 상태일 시 올라가는 상태로 고정
        if (isLeftGrab || isRightGrab) {
            SetActionAnimation(ActionState.Rise);
            return;
        }

        // 입력 감지. 음수면 좌측, 양수면 우측
        float x = Input.GetAxis("Horizontal");
        
        // 이동 거리 대입
        movement.x = x;

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

                if (!audioSource.isPlaying || audioSource.clip != moveSound)
                {
                    audioSource.clip = moveSound;
                    audioSource.loop = true; // 반복 재생 설정
                    audioSource.Play();
                }
                break;
            }

            case ActionState.Stop: {
                moveObject.SetActive(false);
                stopObject.SetActive(true);
                riseObject.SetActive(false);
                // 멈출 때 소리 멈춤
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                break;
            }

            case ActionState.Rise: {
                moveObject.SetActive(false);
                stopObject.SetActive(false);
                riseObject.SetActive(true);
                // 올라갈 때 소리 멈춤
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                break;
            }
        }
    }

    // 좌우 방향변화
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

    // 올라가기
    public Transform RiseAction(Vector3 targetPosition) {
        // 왼쪽 / 오른쪽 손 그랩 여부
        bool isLeft = targetPosition.x < transform.position.x;

        // 처음잡을 경우 가능거리 3.5, 두번째로 잡을 경우 가능거리 5
        float range = (!isLeftGrab && !isRightGrab) ? 3.5f : 5f;

        // 타겟과의 거리가 사정거리내가 아니면 거부
        if (Vector3.Distance(transform.position, targetPosition) > range) {
            return null;
        }

        // 왼손 요구
        if (isLeft) {
            // 같은 타겟을 클릭할 경우 잡기 해제
            if (leftTouchTarget == targetPosition) {
                isLeftGrab = false;
                leftTouchTarget = Vector3.zero;

                // 손 위치 초기화
                leftHand.right = new Vector3(-0.32f, -0.95f, 0);

                return null;
            }

            // 왼쪽 그랩 설정 + 현재 잡고있는 요소에 추가
            else {
                isLeftGrab = true;
                leftTouchTarget = targetPosition;

                return leftGrab;
            }
        }
        
        // 오른손 요구
        else {
            // 같은 타겟을 클릭할 경우 잡기 해제
            if (rightTouchTarget == targetPosition) {
                isRightGrab = false;
                rightTouchTarget = Vector3.zero;
                
                // 손 위치 초기화
                rightHand.right = new Vector3(0.32f, -0.95f, 0);

                return null;
            }

            // 오른쪽 그랩 설정 + 현재 잡고있는 요소에 추가
            else {
                isRightGrab = true;
                rightTouchTarget = targetPosition;

                return rightGrab;
            }
        }
    }

    // 올라가는 상태 설정
    void SetRise() {
        // 양손 다 그랩된 상태가 아니면 관련 설정
        if (!isLeftGrab && !isRightGrab) {
            rb.gravityScale = 90;
            return;
        }

        // 왼손 그랩 상태일 때
        if (isLeftGrab) {
            LeftMove();
        }

        // 오른손 그랩 상태일 때
        if (isRightGrab) {
            RightMove();
        }

        RiseMove();
    }

    // 왼손 이동
    void LeftMove() {
        // 그립 위치
        Vector3 holdPosition = leftGrab.position;
        holdPosition.z = 0f;

        // 왼손과 그립 위치 간의 방향 계산
        Vector3 direction = holdPosition - leftHand.position;

        // 예상 회전 위치
        Vector3 expectedRotation = direction.normalized;
        expectedRotation.z = 0;

        leftHand.right = expectedRotation;
        rb.gravityScale = 0f;
    }

    // 오른손 이동
    void RightMove() {
        // 그립 위치
        Vector3 holdPosition = rightGrab.position;
        holdPosition.z = 0f;

        // 오른손과 그립 위치 간의 방향 계산
        Vector3 direction = holdPosition - rightHand.position;

        // 예상 회전 위치
        Vector3 expectedRotation = direction.normalized;
        expectedRotation.z = 0;

        rightHand.right = expectedRotation;
        rb.gravityScale = 0f;
    }

    // 올라가기 (플레이어)
    void RiseMove() {
        // 양손 모두 잡았을 경우
        if (isLeftGrab && isRightGrab) {
            // 왼손과 오른손의 중앙 위치 계산
            Vector3 target = (leftGrab.position + rightGrab.position) / 2;

            // 위치로 이동
            float upSpeed = 150f;

            // 현재 위치에서 대상 오브젝트로 이동
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target, upSpeed * Time.deltaTime);

            // Rigidbody를 사용하여 부드러운 이동 적용
            rb.MovePosition(newPosition);
        }

        // 왼손만 잡았을 경우
        else if (isLeftGrab) {
            // 대상 오브젝트와의 거리 계산
            Transform target = leftGrab;
            float distance = Vector3.Distance(transform.position, target.position);

            // 대상 오브젝트와 일정 거리 이내에 있을 때까지 이동
            if (distance > 1.5f) {
                float upSpeed = 15f;

                // 대상 오브젝트 방향으로 이동
                transform.position = Vector3.MoveTowards(transform.position, target.position, upSpeed * Time.deltaTime);
            }
            
            // 이동 완료 후 약한 임의의 중력 적용
            else {
                // 0.5씩 transform 하강
                // 오브젝트의 현재 위치를 가져옴
                Vector3 currentPosition = transform.position;

                // y 값을 -1로 설정하여 오브젝트를 아래로 이동시킴
                currentPosition.y -= 0.5f * Time.deltaTime;

                // 오브젝트의 위치를 업데이트
                transform.position = currentPosition;
            }
        }

        // 오른손만 잡았을 경우
        else if (isRightGrab) {
            // 대상 오브젝트와의 거리 계산
            Transform target = rightGrab;
            float distance = Vector3.Distance(transform.position, target.position);

            // 대상 오브젝트와 일정 거리 이내에 있을 때까지 이동
            if (distance > 1.5f) {
                float upSpeed = 15;

                // 대상 오브젝트 방향으로 이동
                transform.position = Vector3.MoveTowards(transform.position, target.position, upSpeed * Time.deltaTime);
            }
            
            // 이동 완료 후 약한 임의의 중력 적용
            else {
                // 0.5씩 transform 하강
                // 오브젝트의 현재 위치를 가져옴
                Vector3 currentPosition = transform.position;

                // y 값을 -1로 설정하여 오브젝트를 아래로 이동시킴
                currentPosition.y -= 0.5f * Time.deltaTime;

                // 오브젝트의 위치를 업데이트
                transform.position = currentPosition;
            }
        }
    }
}
