using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignore : MonoBehaviour
{
    public Collider colliderToIgnore;

    void Start()
    {
        // 이 오브젝트와 colliderToIgnore 간의 충돌 무시
        Physics.IgnoreCollision(GetComponent<Collider>(), colliderToIgnore, true);
    }
}
