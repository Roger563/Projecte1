using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectionable : MonoBehaviour
{
    bool carryFlower;
    private GameObject flower;
    private Vector3 flowerPos;
    public Sprite healthyFlower;
    
    void Start()
    {
        carryFlower = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FirstCollect")
        {
            carryFlower = true;
            //collision.gameObject.SetActive(false);
            collision.enabled = false;
            flower = collision.gameObject;
            //flowerPos = flower.GetComponent<Transform>().position;
            flowerPos = flower.GetComponentInParent<Transform>().position;
            flower.GetComponent<Animator>().SetBool("Levitate", false);
            flower.transform.SetParent(gameObject.transform);
            flower.GetComponent<Transform>().position = gameObject.transform.position;
            flower.GetComponent<Transform>().position += new Vector3(0.0625f + 0.375f, 0.75f, 0); //0.125 -> one pixel
        }

        if(collision.tag == "SecondCollect" && carryFlower)
        {
            carryFlower = false;
            collision.enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = healthyFlower;
            flower.SetActive(false);
            //collision.gameObject.GetComponentInChildren<GameObject>().SetActive(true);
            collision.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void RespawnFlower()
    {
        if (carryFlower)
        {
            flower.SetActive(true);
            flower.GetComponent<Collider2D>().enabled = true;
            flower.transform.SetParent(null);
            flower.GetComponent<Transform>().position = flowerPos;
            flower.GetComponent<Animator>().SetBool("Levitate", true);
            carryFlower = false; 
        }
    }
}
