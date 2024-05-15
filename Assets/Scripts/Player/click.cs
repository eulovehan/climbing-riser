using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class click : MonoBehaviour
{
    // hold set
    public Transform HoldSetPosition;

    // player
    public GameObject Player;

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
            if (!hit.collider.CompareTag("Hold")) {
                return;
            }

            // 클릭한 오브젝트 위치 추적
            Vector3 holdPosition = hit.collider.gameObject.transform.position;

            // z축유지
            holdPosition.z = HoldSetPosition.position.z;
            
            // 클락한 오브젝트 위치로 설정
            HoldSetPosition.position = holdPosition;
            Debug.Log("클릭한 오브젝트의 좌표: " + holdPosition);

            Player.GetComponent<index>().isRise = true;
        }
    }
}
