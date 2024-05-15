using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float riseSpeed = 1f;
    public GameObject smokeObject; // smoke 객체
		public GameObject player; // 플레이어 객체

    float centerY; // smoke의 중앙 y 위치
    float objectHeight; // smoke의 높이
    float topY; // smoke의 상단 y 위치
		int offsetY = 10; // smoke 높이 보정값
		float playerTopOffsetY = 3; // 플레이어 상단 보정값
		float playerTopY;
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        // 초기 중앙 y 위치 및 높이 설정
        UpdatePositionAndHeight();
    }

    void Update()
    {
        // smoke 점점 위로 이동(riseSpeed만큼)
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);

				// 현재 위치 및 높이 갱신
        UpdatePositionAndHeight();

				// 화면 밖으로 벗어나면 smoke 제거(추후 GAME OVER 처리 추가)
        if (topY - offsetY > Camera.main.ViewportToWorldPoint(Vector3.one).y)
        {
            Destroy(smokeObject);
        }

				playerTopY = player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 2 + playerTopOffsetY;

				// 플레이어 상단 영역보다 높이가 높아지면 smoke 제거(추후 GAME OVER 처리 추가)
				if (topY > playerTopY)
				{
						Destroy(smokeObject);
				}
    }

    void UpdatePositionAndHeight()
    {
        centerY = transform.position.y;
        objectHeight = renderer.bounds.size.y;
        topY = centerY + objectHeight / 2;
    }
}