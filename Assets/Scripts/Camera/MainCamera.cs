using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform을 참조합니다.
    public GameObject background; // 배경 오브젝트를 참조합니다.
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움을 조절하는 변수입니다.
    public Vector3 offset; // 카메라와 플레이어 사이의 고정된 거리(오프셋)입니다.
    private float backgroundX; // 배경의 x 위치를 저장하는 변수입니다.
    private float minY; // 카메라의 최소 Y 위치를 저장하는 변수입니다.
    private float offsetX = 10; // 카메라 X 위치 보정값입니다.
    private float offsetY = 20.5f; // 카메라 Y 위치 보정값입니다.

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "ClimbingRiser")
        {
            enabled = false;
            return;
        }
    }
    void LateUpdate()
    {
        Renderer backgroundRenderer = background.GetComponent<Renderer>();
        if (backgroundRenderer != null)
        {
            backgroundX = background.transform.position.x + backgroundRenderer.bounds.size.x / 2 + offsetX;
            minY = background.transform.position.y - backgroundRenderer.bounds.size.y / 2 + Camera.main.orthographicSize - offsetY;

            float targetY = player.position.y + offset.y;
            float cameraMinY = Mathf.Max(targetY, minY);

            Vector3 desiredPosition = new Vector3(backgroundX, cameraMinY, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            Debug.LogError("No Renderer attached to the Background object.");
        }
    }
}