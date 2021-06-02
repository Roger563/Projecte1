using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class NextLevel : MonoBehaviour
{
    public GameObject UI;
    public TMP_Text timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           
            Time.timeScale = 0;
            UI.transform.GetChild(UI.transform.childCount-1).gameObject.GetComponent<TMP_Text>().text = timer.text;
            UI.SetActive(true);
            // UI.GetComponentInChildren<TMP_Text>().text=timer.text;
           
        }
    }
}
