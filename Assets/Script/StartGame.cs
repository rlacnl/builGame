using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject[] scriptsToEnable; // 작동시킬 스크립트들이 붙은 오브젝트들
    public GameObject player;
    public GameObject thisButton; // 버튼 오브젝트 자기 자신

    private bool hasStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasStarted && collision.CompareTag("Player"))
        {
            hasStarted = true;

            // 필요한 스크립트 켜기
            foreach (GameObject obj in scriptsToEnable)
            {
                MonoBehaviour[] comps = obj.GetComponents<MonoBehaviour>();
                foreach (var comp in comps)
                    comp.enabled = true;
            }

            // 플레이어 이동/점프 스크립트도 켜기 (예: User 스크립트)
            if (player != null)
            {
                var userScript = player.GetComponent<User>();
                if (userScript != null)
                    userScript.enabled = true;
            }

            // 버튼 비활성화
            thisButton.SetActive(false);
        }
    }
}
