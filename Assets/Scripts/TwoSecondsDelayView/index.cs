using System.Collections;
using UnityEngine;

public class TwoSecondsDelayView : MonoBehaviour
{
    public GameObject[] uiElements;

    private void Start()
    {
        // 모든 UI 요소를 비활성화
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }
        
        // 코루틴 시작
        StartCoroutine(ShowUIAfterDelay());
    }

    private IEnumerator ShowUIAfterDelay()
    {
        // 2초 대기
        yield return new WaitForSeconds(2f);

        // UI 요소들을 활성화
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(true);
            Debug.Log(uiElement.name + " is now active.");
        }
    }
}
