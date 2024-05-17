using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 참조합니다.
    public GameObject background; // 배경 오브젝트를 참조합니다.
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움을 조절하는 변수입니다.
    public Vector3 offset; // 카메라와 플레이어 사이의 고정된 거리(오프셋)입니다
    
    public float minX, maxX, minY, maxY; // 카메라 이동 제한을 위한 변수입니다.

    private void Start() {
        Renderer backgroundRenderer = background.GetComponent<Renderer>();
        float leftBound = backgroundRenderer.bounds.min.x;
        float rightBound = backgroundRenderer.bounds.max.x;
        float topBound = backgroundRenderer.bounds.max.y;
        float bottomBound = backgroundRenderer.bounds.min.y;

        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        leftBound += camWidth;
        rightBound -= camWidth;
        topBound -= camHeight;
        bottomBound += camHeight;

        minX = leftBound;
        maxX = rightBound;
        minY = bottomBound;
        maxY = topBound;
    }


    void LateUpdate() {
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.z = transform.position.z;

        // 카메라의 x, y 위치를 제한된 범위 내로 조정
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
