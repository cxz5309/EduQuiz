using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        Destroy(gameObject, 3.0f);  // 총알 3초 후 제거
        Physics.IgnoreLayerCollision(8, 5);
        transform.position += transform.forward * 100.0f * Time.deltaTime;
    }
}
