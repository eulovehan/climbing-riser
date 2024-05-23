using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreGround : MonoBehaviour
{
    public Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 올라가는 중이면 충돌 무시
        bool isLeftGrab = playerCollider.gameObject.GetComponent<index>().isLeftGrab;
        bool isRightGrab = playerCollider.gameObject.GetComponent<index>().isRightGrab;
    
        if (isLeftGrab || isRightGrab)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider, true);
        }

        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider, false);
        }
    }
}
