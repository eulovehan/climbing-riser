using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool isGamePlaying { get; private set; }

    private AudioSource audioSource;
    public AudioClip backgroundSound; // 배경 음악 오디오 클립
    public AudioClip introBackgroundSound; // 인트로 배경 음악 오디오 클립

    public float totalHuman = 0;
    public float filedResuceHuman = 0; // 구조하지 못한 사람 수
    public bool isLive = true; // 생존 여부

    void Start() {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Human");
        totalHuman = objectsWithTag.Length;
    }

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
            introBackgroundSound = Resources.Load<AudioClip>("Audio/intro_bgm");

            // 현재 씬의 배경 음악 재생
            PlayBackgroundSound(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때마다 배경 음악 재생
        PlayBackgroundSound(scene.name);
    }


    // 옥상에 올라감

    // 가스에 빠짐
    public void GameOver()
    {
        GameOutVolume();
        
        // 구조 못한 사람 수 파악
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Human");
        filedResuceHuman = objectsWithTag.Length;
        isLive = false;
        
        IsGameOver = true;
        Debug.Log("GAME OVER!");
        SceneManager.LoadScene("Ending", LoadSceneMode.Single);
    }

    // 게임 클리어
    public void GameClear()
    {
        GameOutVolume();
        
        // 구조 못한 사람 수 파악
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Human");
        filedResuceHuman = objectsWithTag.Length;
        isLive = true;
        
        IsGameOver = true;
        Debug.Log("GAME CLEAR!");
        SceneManager.LoadScene("Ending", LoadSceneMode.Single);
    }

    public void RestartGame()
    {
        IsGameOver = false;
        SceneManager.LoadScene("ClimbingRiser", LoadSceneMode.Single); // 게임 시작 씬 이름으로 변경
        // DestroyUI("RestartButton");
    }

    public void StartGame()
    {
        IsGameOver = false;
        SceneManager.LoadScene("ClimbingRiser", LoadSceneMode.Single);
        // DestroyUI("StartButton");
    }

    public void StartIntro1()
    {
        SceneManager.LoadScene("Intro1", LoadSceneMode.Single);
    }
    public void StartIntro2()
    {
        SceneManager.LoadScene("Intro2", LoadSceneMode.Single);
    }
    public void StartIntro3()
    {
        SceneManager.LoadScene("Intro3", LoadSceneMode.Single);
    }
    public void StartIntro4()
    {
        SceneManager.LoadScene("Intro4", LoadSceneMode.Single);
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

    private void PlayBackgroundSound(string sceneName)
    {
        if (sceneName.StartsWith("Intro"))
        {
            if (audioSource.clip != introBackgroundSound || !audioSource.isPlaying)
            {
                audioSource.clip = introBackgroundSound;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != backgroundSound || !audioSource.isPlaying)
            {
                audioSource.clip = backgroundSound;
                audioSource.Play();
            }
        }
    }

    
    // 코루틴을 시작하는 메서드
    public void GameOutVolume()
    {
        StartCoroutine(FadeOut());
    }
    
    // 오디오 볼륨을 서서히 줄이는 코루틴
    private IEnumerator FadeOut()
    {
        float fadeDuration = 5.0f;
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    public void setIsGamePlaying(bool isGamePlaying)
    {
        this.isGamePlaying = isGamePlaying;
    }
}