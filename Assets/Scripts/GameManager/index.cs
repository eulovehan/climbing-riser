using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; }

    void Awake()
    {
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
    }

    public void GameOver()
    {
			IsGameOver = true;
			Debug.Log("GAME OVER!");
    }
}
