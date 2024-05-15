using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 참조합니다.
    public GameObject background; // 배경 오브젝트를 참조합니다.
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움을 조절하는 변수입니다.
    public Vector3 offset; // 카메라와 플레이어 사이의 고정된 거리(오프셋)입니다.
    private float backgroundX; // 배경의 x 위치를 저장하는 변수입니다.
    private float minY; // 카메라의 최소 Y 위치를 저장하는 변수입니다.

    void LateUpdate()
    {
        Renderer backgroundRenderer = background.GetComponent<Renderer>();
        backgroundX = background.transform.position.x - backgroundRenderer.bounds.size.x / 2;
        minY = background.transform.position.y - backgroundRenderer.bounds.size.y / 2 + Camera.main.orthographicSize;

        float targetY = player.position.y + offset.y;
        float cameraMinY = Mathf.Max(targetY, minY); // 캐릭터 위치와 배경 하단 중 높은 위치 선택

        Vector3 desiredPosition = new Vector3(backgroundX, cameraMinY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
