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

    int millis;
    int seconds;
    int minutes;

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

    public int bronzeM;
    public int bronzeS;

    int scoreState;

    public Canvas canvas;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            millis = timer.GetComponent<Timer>().milliseconds;
            seconds = timer.GetComponent<Timer>().seconds;
            minutes = timer.GetComponent<Timer>().minutes;
            timer.enabled = false;
            UI.transform.GetChild(UI.transform.childCount - 4).gameObject.GetComponent<TMP_Text>().text = ("BRONZE: "+bronzeM.ToString("00") + ":"  + bronzeS.ToString("00") + ":00");
            UI.transform.GetChild(UI.transform.childCount - 5).gameObject.GetComponent<TMP_Text>().text = ("SILVER: "+silverM.ToString("00") + ":" + silverS.ToString("00") + ":00");
            UI.transform.GetChild(UI.transform.childCount - 6).gameObject.GetComponent<TMP_Text>().text = ("GOLD:   " + goldM.ToString("00") + ":" + goldS.ToString("00") + ":00");
            UI.transform.GetChild(UI.transform.childCount - 1).gameObject.GetComponent<TMP_Text>().text = (minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + millis.ToString("00"));
            canvas.GetComponent<InGameMenu>().enabled = false;
            UI.SetActive(true);
            Time.timeScale = 0;

            if( (minutes<=platinoM) && (seconds<platinoS))
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite=platinoTrophy;
            }
            else if ( (minutes <= goldM) && (seconds < goldS) )
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite=goldTrophy;
            }
            else if ((minutes <= silverM) && (seconds < silverS))
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite=silverTrophy;
            }
            else  if( (minutes <= bronzeM) && (seconds < bronzeS) )
            {
                UI.transform.GetChild(UI.transform.childCount - 3).gameObject.GetComponent<Image>().sprite=bronzeTrophy;
            }
            else 
            {
                scoreState = 4;
            }
           
            if (collect.GetComponent<SpriteRenderer>().sprite.name == "HealthyFlower")
            {
                UI.transform.GetChild(UI.transform.childCount - 2).gameObject.GetComponent<Image>().sprite=healthyFlower;
            }

        }
    }
}
