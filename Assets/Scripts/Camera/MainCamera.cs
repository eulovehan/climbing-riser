using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 참조합니다.
    public GameObject background; // 배경 오브젝트를 참조합니다.
    public float smoothSpeed = 0.4f; // 카메라 이동의 부드러움을 조절하는 변수입니다.
    public Vector3 offset; // 카메라와 플레이어 사이의 고정된 거리(오프셋)입니다.
    // private float backgroundX; // 배경의 x 위치를 저장하는 변수입니다.
    // private float minY; // 카메라의 최소 Y 위치를 저장하는 변수입니다.
    // private float offsetX = 10; // 카메라 X 위치 보정값입니다.
    // private float offsetY = 20.5f; // 카메라 Y 위치 보정값입니다.

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "ClimbingRiser")
        {
            enabled = false;
            return;
        }
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.x = transform.position.x; // X축은 고정
        smoothedPosition.z = transform.position.z; // Z축은 고정
        transform.position = smoothedPosition;
    }
}