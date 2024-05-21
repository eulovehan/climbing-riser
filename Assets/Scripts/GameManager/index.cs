using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; }

    private AudioSource audioSource;
    public AudioClip backgroundSound; // 배경 음악 오디오 클립

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 오디오 소스 설정
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;
            backgroundSound = Resources.Load<AudioClip>("Audio/bgm");

            // 배경 음악 재생
            PlayBackgroundSound();
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
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void RestartGame()
    {
        IsGameOver = false;
        SceneManager.LoadScene("ClimbingRiser", LoadSceneMode.Single); // 게임 시작 씬 이름으로 변경
        DestroyUI("RestartButton");
    }

    public void StartGame()
    {
        IsGameOver = false;
        SceneManager.LoadScene("ClimbingRiser", LoadSceneMode.Single);
        DestroyUI("StartButton");
    }

    private void DestroyUI(string objectName)
    {
        var obj = GameObject.Find(objectName);
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    private void CleanupScene()
    {
        var objects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var obj in objects)
        {
            if (obj != this.gameObject && obj.GetComponent<Camera>() == null)
            {
                Destroy(obj);
            }
        }
    }

    private void PlayBackgroundSound()
    {
        if (backgroundSound != null)
        {
            audioSource.clip = backgroundSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Background music is not set.");
        }
    }
}