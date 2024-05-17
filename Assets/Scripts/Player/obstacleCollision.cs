using UnityEngine;

public class obstacleCollision : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("OnCollisionEnter!");
		Debug.Log(collision.gameObject.CompareTag("Obstacle"));
		if (collision.gameObject.CompareTag("Obstacle"))
		{
			Debug.Log("GAME OVER!?");
			GameManager.Instance.GameOver();
		}
	}
}
