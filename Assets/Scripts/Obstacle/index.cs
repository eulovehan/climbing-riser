using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public float fallSpeed = 15f;

	private void Update()
	{
		if (!GameManager.Instance.IsGameOver)
		{
			transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("TEST!");
	}
}