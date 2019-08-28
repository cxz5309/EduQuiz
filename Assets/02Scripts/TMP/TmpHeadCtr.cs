using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpHeadCtr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.localRotation *= Quaternion.Euler(2, 0, 0);//부호 주의
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.localRotation *= Quaternion.Euler(-2, 0, 0);//부호 주의
        }
    }
}
