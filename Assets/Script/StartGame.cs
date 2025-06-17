using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject[] scriptsToEnable; // �۵���ų ��ũ��Ʈ���� ���� ������Ʈ��
    public GameObject player;
    public GameObject thisButton; // ��ư ������Ʈ �ڱ� �ڽ�

    private bool hasStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasStarted && collision.CompareTag("Player"))
        {
            hasStarted = true;

            // �ʿ��� ��ũ��Ʈ �ѱ�
            foreach (GameObject obj in scriptsToEnable)
            {
                MonoBehaviour[] comps = obj.GetComponents<MonoBehaviour>();
                foreach (var comp in comps)
                    comp.enabled = true;
            }

            // �÷��̾� �̵�/���� ��ũ��Ʈ�� �ѱ� (��: User ��ũ��Ʈ)
            if (player != null)
            {
                var userScript = player.GetComponent<User>();
                if (userScript != null)
                    userScript.enabled = true;
            }

            // ��ư ��Ȱ��ȭ
            thisButton.SetActive(false);
        }
    }
}
