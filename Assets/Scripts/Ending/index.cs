using System.Collections;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject ending1; // 모두 구조 실패, 사망
    public GameObject ending2; // 일부 구조, 사망
    public GameObject ending3; // 모두 구조, 사망
    public GameObject ending4; // 모두 구조 실패, 생존
    public GameObject ending5; // 일부 구조, 생존
    public GameObject ending6; // 모두 구조, 생존

    private float duration = 8f * 1.25f; // 페이드 인 지속 시간
    private GameObject targetEnding;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndDoSomething());
    }

    // 엔딩
    void ending() {
        GameObject[] gameManager = GameObject.FindGameObjectsWithTag("GameManager");

        if (gameManager.Length == 0) {
            return;
        }

        float totalHuman = gameManager[0].GetComponent<GameManager>().totalHuman;
        float filedResuceHuman = gameManager[0].GetComponent<GameManager>().filedResuceHuman;
        bool isLive = gameManager[0].GetComponent<GameManager>().isLive;
        
        if (isLive) {
            if (filedResuceHuman == 0) {
                targetEnding = ending6;
            } else if (totalHuman == filedResuceHuman) {
                targetEnding = ending4;
            } else {
                targetEnding = ending5;
            }
        } else {
            if (filedResuceHuman == 0) {
                targetEnding = ending3;
            } else if (totalHuman == filedResuceHuman) {
                targetEnding = ending1;
            } else {
                targetEnding = ending2;
            }
        }

        targetEnding.SetActive(true);
        SpriteRenderer spriteRenderer = targetEnding.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeInCoroutine(spriteRenderer));
    }

    IEnumerator WaitAndDoSomething()
    {
        // 4초 대기
        yield return new WaitForSeconds(4f);

        // 원하는 작업 수행
        ending();
    }
    
    private IEnumerator FadeInCoroutine(SpriteRenderer spriteRenderer)
    {
        Color color = spriteRenderer.color;
        float startAlpha = 0.0f;
        float endAlpha = 0.7f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // 마지막으로 알파값을 설정
        spriteRenderer.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
