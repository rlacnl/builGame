using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Span : MonoBehaviour
{
    public float rotationSpeed = 5000f; // ȸ�� �ӵ� (��/��)

    // Update is called once per frame
    void Update()
    {
        // Y���� �������� ȸ��
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}