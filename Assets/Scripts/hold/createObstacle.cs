using UnityEngine;

public class CreateObstacle : MonoBehaviour
{
    public GameObject obstaclePrefab;
		private GameObject player;
		public float obstacleOffsetY = 30;
		private bool hasBeenClicked = false;
		private float distanceDelta = 4;

		private void Start()
    {
			player = GameObject.Find("Player");
    }

    private void OnMouseDown()
    {
			// 2D 거리 계산 (x, y 좌표만 사용)
			float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
			Debug.Log("Distance: " + distance);
			if (distance > distanceDelta)
			{
				return;
			}

			if (!hasBeenClicked)
        {
					Vector3 obstaclePosition = new Vector3(transform.position.x, transform.position.y + obstacleOffsetY, transform.position.z);
					Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
					hasBeenClicked = true;
        }
    }
}