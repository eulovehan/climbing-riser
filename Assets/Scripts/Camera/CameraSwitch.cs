using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라를 할당합니다.
    public Camera subCamera; // 서브 카메라를 할당합니다.
    public GameObject Player; // 플레이어 객체

    void Start()
    {
        // 초기 설정: 메인 카메라를 활성화하고 서브 카메라는 비활성화합니다.
        mainCamera.enabled = true;
        subCamera.enabled = false;
    }

    void Update()
    {
        // 'V' 키를 누르면 카메라를 전환합니다.
        if (Input.GetKeyDown(KeyCode.V))
        {
            swapCamera();
        }

        /** 플레이어가 움직이면 카메라 전환 */
        if (subCamera.enabled == false) {
            bool isMove = Player.GetComponent<index>().moveObject.activeSelf || Player.GetComponent<index>().riseObject.activeSelf;
            if (isMove) {
                swapCamera();
            }
        }
    }
    
    public void swapCamera()
    {
        // 카메라 활성화 상태를 전환합니다.
        mainCamera.enabled = !mainCamera.enabled;
        subCamera.enabled = !subCamera.enabled;
    }
}
