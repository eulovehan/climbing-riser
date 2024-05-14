using Unity.VisualScripting;
using UnityEngine;

public class riggingText : MonoBehaviour
{
    // public statics
    // 조작할 bone의 Transform
    public Transform bone;
    
    // 왼손 / 오른손 구분
    public bool isLeftHand = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // 마우스 클릭 입력 감지
        if (Input.GetMouseButtonDown(0))
        {
            HandRotation();
        }
    }

    // 팔 회전 함수
    void HandRotation() {
        // 마우스 위치 가져오기
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D 공간에서는 z 축을 사용하지 않음

        // bone1과 마우스 위치 간의 방향 계산
        Vector3 direction = mousePosition - bone.position;

        // 예상 회전 거리
        Vector3 expectedRotation = direction.normalized;
        
        /** 오른손일 경우 x축이 마이너스로 떨어지면 회전 최대치 이므로 거부 */
        if (isLeftHand && LeftHand(expectedRotation.x)) {
            bone.right = direction.normalized;   
        }

        else if (!isLeftHand && RightHand(expectedRotation.x)) {
            bone.right = direction.normalized;   
        }
    }

    // left hand check
    bool LeftHand(float x) {
        return x < 0 ? true : false;
    }

    // right hand check
    bool RightHand(float x) {
        return x > 0 ? true : false;
    }
}
