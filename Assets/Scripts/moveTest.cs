using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // 이동 속도를 조절하는 변수
    private float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 위치를 받아옴
        Vector3 mousePosition = Input.mousePosition;
        
        // 카메라에서 월드 좌표로 변환
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.y - Camera.main.transform.position.z));
        // z 축 값은 원래 오브젝트의 z 값으로 설정
        worldPosition.z = transform.position.z;

        // 오브젝트를 새로운 위치로 이동
        transform.position = Vector3.Lerp(transform.position, worldPosition, moveSpeed * Time.deltaTime);
    }
}
