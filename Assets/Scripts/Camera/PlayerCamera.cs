using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 Transform을 할당합니다.
    public float smoothSpeed = 0.125f; // 카메라가 따라오는 속도입니다.
    public Vector3 offset; // 플레이어와 카메라 간의 거리입니다.

    void FixedUpdate()
    {
        // 목표 위치는 플레이어 위치 + 오프셋입니다.
        Vector3 desiredPosition = playerTransform.position + offset;
        desiredPosition.z = transform.position.z; // 카메라의 Z축 위치를 유지합니다.
        
        // 현재 카메라 위치와 목표 위치 사이를 보간합니다.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // 카메라 위치를 보간된 위치로 설정합니다.
        transform.position = smoothedPosition;
    }
}
