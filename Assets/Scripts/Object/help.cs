using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help : MonoBehaviour
{
    public GameObject helpObject; // 못구함
    public GameObject saveObject; // 구함

    public float fadeSpeed = 0.5f; // 사라지는 속도

    void Update()
    {
        // 구출됐을 경우 서서히 사라짐
        if (saveObject.activeSelf) {
            Color currentColor = saveObject.GetComponent<Renderer>().material.color;

            // 새로운 알파 값 계산 (서서히 감소)
            float newAlpha = Mathf.MoveTowards(currentColor.a, 0f, fadeSpeed * Time.deltaTime);

            // 새로운 색상 생성 (새로운 알파 값 적용)
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            // Material에 새로운 색상 적용
            saveObject.GetComponent<Renderer>().material.color = newColor;

            // 알파 값이 0에 도달하면 오브젝트 삭제
            if (newAlpha == 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 특정 객체인지 확인
        if (collision.gameObject.tag == "Player")
        {
            // 충돌한 객체가 특정 객체일 때 할 일
            Save();
        }
    }

    void Save() {
        helpObject.SetActive(false);
        saveObject.SetActive(true);
    }
}
