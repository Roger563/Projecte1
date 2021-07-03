using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool[] level_status; //if level is unlocked or not
    private bool[] level_trophy; //if level is unlocked or not
    private bool[] level_collectionable; //if level collectionable has been aquired

    void Start()
    {
        //level_status = read level status from file;
        //level_trophy = read level trophy from file;
        //level_collectionable = read collectionable from file;
    }

    void Update()
    {
        
    }
}
