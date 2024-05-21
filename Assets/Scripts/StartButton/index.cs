using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
	void OnGUI()
	{
		if (SceneManager.GetActiveScene().name != "GameStart")
		{
			enabled = false;
			return;
		}
		// 임의의 Box를 사용하여 버튼을 생성합니다.
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Start"))
		{
				GameManager.Instance.StartGame();
		}
	}
}