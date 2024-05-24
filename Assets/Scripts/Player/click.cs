using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class click : MonoBehaviour
{
    // player
    public GameObject Player;
    private AudioSource audioSource;
    public AudioClip holdSound; // 홀드 소리 오디오 클립

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "ClimbingRiser")
        {
            enabled = false;
            return;
        }
        audioSource = Player.GetComponent<AudioSource>();
        holdSound = Resources.Load<AudioClip>("Audio/holdSound");

        if (audioSource == null)
        {
            Debug.LogError("Player 오브젝트에 AudioSource 컴포넌트가 없습니다.");
        }

    }

    void Update()
    {
        HoldSet();
    }

    // 홀드 잡기
    void HoldSet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 카메라에서 마우스 위치로 레이를 쏴서 충돌한 오브젝트를 검출
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // 오브젝트를 클릭하지 않았을 때 중지
            if (hit.collider == null) {
                return;
            }

            // 홀드가 아니면 중지
            if (!(hit.collider.CompareTag("Hold") || hit.collider.CompareTag("WindowHold"))) {
                return;
            }

            // 클릭한 오브젝트 위치 추적
            Vector3 holdPosition = hit.collider.gameObject.transform.position;

            // 플레이어의 홀드 정보
            Transform HoldSetPosition = Player.GetComponent<index>().RiseAction(holdPosition);

            // 홀드 조건에 만족하지 않으면 그랩불가 (거리, 홀드해제)
            if (!HoldSetPosition) {
                return;
            }

            // z축유지
            holdPosition.z = HoldSetPosition.position.z;

            // 클릭한 오브젝트 위치로 설정
            HoldSetPosition.position = holdPosition;

            // 홀드 잡는 소리 재생
            if (audioSource != null && holdSound != null)
            {
                audioSource.PlayOneShot(holdSound);
            }
        }
    }
}
