using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class index : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 좌우 방향키 입력 감지
        float horizontalInput = Input.GetAxis("Horizontal");

        // 이동 벡터 계산
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, 0f);

        // 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
