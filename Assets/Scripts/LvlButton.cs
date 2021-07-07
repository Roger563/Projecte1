using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LvlButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLvl()
    {
       
        if (gameObject.transform.GetChild(transform.childCount - 3).gameObject.activeSelf)
        {
            int index = int.Parse(gameObject.transform.GetChild(transform.childCount - 3).gameObject.GetComponent<TMP_Text>().text);
            SceneManager.LoadScene(index + 1);
        }
       
    }
}
