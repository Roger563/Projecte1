using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLevel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("1"))
            SceneManager.LoadScene(1);

        if (Input.GetKeyDown("2"))
            SceneManager.LoadScene(2);

        if (Input.GetKeyDown("3"))
            SceneManager.LoadScene(3);

        if (Input.GetKeyDown("4"))
            SceneManager.LoadScene(4);

        if (Input.GetKeyDown("5"))
            SceneManager.LoadScene(5);

        if (Input.GetKeyDown("6"))
            SceneManager.LoadScene(6);

        if (Input.GetKeyDown("7"))
            SceneManager.LoadScene(7);

        if (Input.GetKeyDown("8"))
            SceneManager.LoadScene(8);

        if (Input.GetKeyDown("9"))
            SceneManager.LoadScene(9);
    }
}
