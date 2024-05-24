using System.Collections;
using TMPro;
using UnityEngine;

public class TypingText : MonoBehaviour
{
    public TMP_Text text;
    public string dialogue;
    public GameObject nextButton;  // 다음 버튼

    void Start()
    {
        nextButton.SetActive(false);  // 시작할 때 버튼을 숨김
        StartCoroutine(Typing(dialogue));
    }

    IEnumerator Typing(string talk)
    {
        text.text = null;
        for (int i = 0; i < talk.Length; i++)
        {
            text.text += talk[i];
            yield return new WaitForSeconds(0.05f);
        }
        nextButton.SetActive(true);  // 타이핑이 완료되면 버튼을 보이게 함
    }
}
