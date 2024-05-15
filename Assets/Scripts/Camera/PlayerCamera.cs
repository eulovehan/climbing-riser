using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 참조합니다.
    public GameObject background; // 배경 오브젝트의 Transform을 참조합니다.
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움을 조절하는 변수입니다.
    public Vector3 offset; // 카메라와 플레이어 사이의 고정된 거리(오프셋)입니다.
    private float backgroundX; // 배경의 x 위치를 저장하는 변수입니다.

    void Start()
    {
        // 배경 오브젝트의 X 위치를 가져옵니다.
        if (background != null)
            backgroundX = background.transform.position.x;
        else
            Debug.LogError("Background object is not assigned!");
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(backgroundX, player.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
