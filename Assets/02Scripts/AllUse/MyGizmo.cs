using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color color = Color.green;   // 색 저장 

    public float radius = 0.4f;     // 반지름 저장 

    void OnDrawGizmos()     // Gizmo를 그리는 함수 
    {
        Gizmos.color = color;
        // Gizmo의 색을 color 변수의 색으로 한다.

        Gizmos.DrawSphere(transform.position, radius);
        // Gizmo를 Sphere 형태로 현재 위치에 반지름 사이즈로 그린다.
    }
}
