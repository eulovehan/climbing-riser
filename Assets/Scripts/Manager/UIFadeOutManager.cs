using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeOutManager : MonoBehaviour
{
    public CanvasGroup[] uiCanvasElements; // CanvasGroup을 사용하는 UI 요소 배열
    public SpriteRenderer[] uiSpriteElements; // SpriteRenderer를 사용하는 UI 요소 배열
    public CanvasGroup[] uiCanvasElementsImmediate; // 즉시 알파 값을 0으로 설정할 CanvasGroup 배열
    public SpriteRenderer[] uiSpriteElementsImmediate; // 즉시 알파 값을 0으로 설정할 SpriteRenderer 배열

    public IEnumerator FadeOutAllUI(float duration)
    {
        // 즉시 알파 값을 0으로 설정
        foreach (CanvasGroup uiElement in uiCanvasElementsImmediate)
        {
            uiElement.alpha = 0;
        }

        foreach (SpriteRenderer uiElement in uiSpriteElementsImmediate)
        {
            Color color = uiElement.color;
            color.a = 0;
            uiElement.color = color;
        }

        // 점진적으로 알파 값을 0으로 설정
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float alpha = 1.0f - (elapsedTime / duration);

            // CanvasGroup 요소 페이드 아웃
            foreach (CanvasGroup uiElement in uiCanvasElements)
            {
                uiElement.alpha = alpha;
            }

            // SpriteRenderer 요소 페이드 아웃
            foreach (SpriteRenderer uiElement in uiSpriteElements)
            {
                Color color = uiElement.color;
                color.a = alpha;
                uiElement.color = color;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 알파 값 설정
        foreach (CanvasGroup uiElement in uiCanvasElements)
        {
            uiElement.alpha = 0;
        }

        foreach (SpriteRenderer uiElement in uiSpriteElements)
        {
            Color color = uiElement.color;
            color.a = 0;
            uiElement.color = color;
        }
    }
}
