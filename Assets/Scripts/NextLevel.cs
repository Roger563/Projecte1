using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevel : MonoBehaviour
{
    public GameObject collect;
    public GameObject UI;
    public TMP_Text timer;

    public Color32 plat;
    public Color32 gold;
    public Color32 silver;
    public Color32 bronze;

    int millis;
    int seconds;
    int minutes;
    int[] highScore = new int[3] {999,999,999};

    public Sprite platinoTrophy;
    public Sprite goldTrophy;
    public Sprite silverTrophy;
    public Sprite bronzeTrophy;
    public Sprite healthyFlower;

    public int platinoM;
    public int platinoS;

    public int goldM;
    public int goldS;

    public int silverM;
    public int silverS;

    int scoreState;

    public Canvas canvas;

    const int nScenes = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //load game
           GameData data = SaveGame.LoadHighScore();
           
           highScore[0] = data.time[SceneManager.GetActiveScene().buildIndex - 1][0];
           highScore[1] = data.time[SceneManager.GetActiveScene().buildIndex - 1][1];
           highScore[2] = data.time[SceneManager.GetActiveScene().buildIndex - 1][2];


            millis = timer.GetComponent<Timer>().milliseconds;
            seconds = timer.GetComponent<Timer>().seconds;
            minutes = timer.GetComponent<Timer>().minutes;
            timer.enabled = false;
            UI.transform.GetChild(UI.transform.childCount - 4).gameObject.GetComponent<TMP_Text>().text = ("PLATINUM: "+ platinoM.ToString("00") + ":"  + platinoS.ToString("00") + ":00");
            UI.transform.GetChild(UI.transform.childCount - 5).gameObject.GetComponent<TMP_Text>().text = ("SILVER:   "+silverM.ToString("00") + ":" + silverS.ToString("00") + ":00");
            UI.transform.GetChild(UI.transform.childCount - 6).gameObject.GetComponent<TMP_Text>().text = ("GOLD:     " + goldM.ToString("00") + ":" + goldS.ToString("00") + ":00");
            UI.transform.GetChild(UI.transform.childCount - 1).gameObject.GetComponent<TMP_Text>().text = (minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + millis.ToString("00"));
            canvas.GetComponent<InGameMenu>().enabled = false;
            UI.SetActive(true);
            Time.timeScale = 0;

            short t;
            if ((minutes < platinoM) || ((minutes == platinoM) && (seconds < platinoS)))
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite = platinoTrophy;
                UI.transform.GetChild(UI.transform.childCount - 1).gameObject.GetComponent<TMP_Text>().color = plat;
                t = 3;
            }
            else if ((minutes < goldM) || ((minutes == goldM) && (seconds < goldS)))
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite = goldTrophy;
                UI.transform.GetChild(UI.transform.childCount - 1).gameObject.GetComponent<TMP_Text>().color = gold;
                t = 2;
            }
            else if ((minutes < silverM) || ((minutes == silverM) && (seconds < silverS)))
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite = silverTrophy;
                UI.transform.GetChild(UI.transform.childCount - 1).gameObject.GetComponent<TMP_Text>().color = silver;
                t = 1;
            }
            else
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite = bronzeTrophy;
                UI.transform.GetChild(UI.transform.childCount - 1).gameObject.GetComponent<TMP_Text>().color = bronze;
                t = 0;
            }

            bool collectionable = false;
            if (collect.GetComponent<SpriteRenderer>().sprite.name == "HealthyFlower")
            {
                UI.transform.GetChild(UI.transform.childCount - 2).gameObject.GetComponent<Image>().sprite=healthyFlower;
                collectionable = true;
            }

            if ((minutes < highScore[0]) || ((minutes == highScore[0]) && (seconds < highScore[1])) || ((minutes == highScore[0]) && (seconds == highScore[1])&&(millis < highScore[2])))
            {
                highScore[0] = minutes;
                highScore[1] = seconds;
                highScore[2] = millis;

                //save highscore
                data.time[SceneManager.GetActiveScene().buildIndex - nScenes][0] = highScore[0];
                data.time[SceneManager.GetActiveScene().buildIndex - nScenes][1] = highScore[1];
                data.time[SceneManager.GetActiveScene().buildIndex - nScenes][2] = highScore[2];
                SaveGame._SaveHighScore(data.time);
            }

            UI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = (highScore[0].ToString("00") + ":" + highScore[1].ToString("00") + ":" + highScore[2].ToString("00"));

            data = SaveGame.LoadState();

            if (t > data.levelTrophy[SceneManager.GetActiveScene().buildIndex - nScenes])
            {
                data.levelTrophy[SceneManager.GetActiveScene().buildIndex - nScenes] = t;
            }

            if (collectionable)
                data.levelCollectionable[SceneManager.GetActiveScene().buildIndex - nScenes] = true;

            if (SceneManager.GetActiveScene().buildIndex - nScenes == data.levelsCompleted)
            {
                data.levelsCompleted++;
            }

            SaveGame._SaveGame(data.levelsCompleted, data.levelTrophy, data.levelCollectionable);
        }
    }
}
