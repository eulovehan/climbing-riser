using UnityEngine;

public class CreateObstacle : MonoBehaviour
{
    public GameObject obstaclePrefab;
		public float obstacleOffsetY = 30;
		private bool hasBeenClicked = false;

    private void OnMouseDown()
    {
			if (!hasBeenClicked)
        {
					Vector3 obstaclePosition = new Vector3(transform.position.x, transform.position.y + obstacleOffsetY, transform.position.z);
					Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
					hasBeenClicked = true;
        }
    }
}