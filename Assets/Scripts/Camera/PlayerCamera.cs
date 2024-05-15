using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 참조합니다.
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움을 조절하는 변수입니다.
    public Vector3 offset; // 카메라와 플레이어 사이의 고정된 거리(오프셋)입니다.

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset; // 목표 위치 설정
        desiredPosition.z = transform.position.z; // 2D 게임에서는 Z축을 고정합니다.
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // 현재 위치에서 목표 위치로 부드럽게 이동
        transform.position = smoothedPosition; // 카메라 위치 업데이트
    }
}
