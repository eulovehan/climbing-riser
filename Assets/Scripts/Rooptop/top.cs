using UnityEngine;

public class top : MonoBehaviour
{
    public GameObject GameManager; // 게임 매니저 오브젝트
    
    // 특정 오브젝트와 충돌했을 때 호출되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 감지
        if (collision.gameObject.CompareTag("Player"))
        {
            // 게임 클리어처리
            GameManager.GetComponent<GameManager>().GameClear();
        }
    }
}
