using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpBodyCtr : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.localRotation *= Quaternion.Euler(0, 2, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.localRotation *= Quaternion.Euler(0, -2, 0);
        }
    }
}
