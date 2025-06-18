using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Span : MonoBehaviour
{
    public float rotationSpeed = 5000f; // 회전 속도 (도/초)

    // Update is called once per frame
    void Update()
    {
        // Y축을 기준으로 회전
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}