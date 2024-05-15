using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorePlayer : MonoBehaviour
{
    public Collider2D colliderToIgnore;

    void Start()
    {
        // 이 오브젝트와 colliderToIgnore 간의 충돌 무시
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), colliderToIgnore, true);
    }
}
