using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameData data;

    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    public Sprite plat;
    public Sprite flowerPlat;

    public GameObject Collectionable;

    void Awake()
    {
        //load status, collectionables and trophies
        //load game
        //data = SaveGame.LoadGame();
    }

    void Start()
    {
        //load game
        data = SaveGame.LoadState();

        //const int background = 0;
        const int lockPad = 1;
        const int border = 2;
        const int number = 3;
        const int vines = 4;
        //const int selected = 5;

        for (int i = 0; i < data.levelsCompleted; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            obj.transform.GetChild(lockPad).gameObject.GetComponent<Image>().enabled = false;
            obj.transform.GetChild(number).gameObject.SetActive(true);
            obj.transform.GetChild(number).gameObject.GetComponent<TMP_Text>().text = (i + 1).ToString();

            switch (data.levelTrophy[i])
            {
                case 0:
                    obj.transform.GetChild(border).gameObject.GetComponent<Image>().sprite = bronze;
                    break;
                case 1:
                    obj.transform.GetChild(border).gameObject.GetComponent<Image>().sprite = silver;
                    break;
                case 2:
                    obj.transform.GetChild(border).gameObject.GetComponent<Image>().sprite = gold;
                    break;
                case 3:
                    obj.transform.GetChild(border).gameObject.GetComponent<Image>().sprite = plat;
                    break;
            }

            if (data.levelCollectionable[i]) { obj.transform.GetChild(vines).gameObject.SetActive(true); }
        }

        if (data.levelsCompleted != 16)
        {
            GameObject obj = transform.GetChild(data.levelsCompleted).gameObject;
            obj.transform.GetChild(lockPad).gameObject.GetComponent<Image>().enabled = false;
            obj.transform.GetChild(number).gameObject.SetActive(true);
            obj.transform.GetChild(number).gameObject.GetComponent<TMP_Text>().text = (data.levelsCompleted + 1).ToString();
        }

        int n = 0;
        for (int i = 0; i < 16; i++)
        {
            if (data.levelCollectionable[i])
                n++;
        }
        Collectionable.GetComponent<TMP_Text>().text = n.ToString("00") + "/16";

        if(n == 16)
        {
            Collectionable.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = flowerPlat;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
