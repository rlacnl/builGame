using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject[] scriptsToEnable; 
    public GameObject player;
    public GameObject thisButton; 

    private bool hasStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasStarted && collision.CompareTag("Player"))
        {
            hasStarted = true;

            foreach (GameObject obj in scriptsToEnable)
            {
                MonoBehaviour[] comps = obj.GetComponents<MonoBehaviour>();
                foreach (var comp in comps)
                    comp.enabled = true;
            }

            if (player != null)
            {
                var userScript = player.GetComponent<User>();
                if (userScript != null)
                    userScript.enabled = true;
            }

            thisButton.SetActive(false);
        }
    }
}
