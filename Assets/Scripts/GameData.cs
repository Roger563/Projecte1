using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelsCompleted; //if level is unlocked or not
    public short[] levelTrophy; //trophy
    public bool[] levelCollectionable; //if level collectionable has been aquired
    public int[][] time = new int[16][];

    public GameData(int _levelsCompleted,short[] _levelTrophy,bool[] _levelCollectionable)
    {
        levelsCompleted = _levelsCompleted;
        levelTrophy = _levelTrophy;
        levelCollectionable = _levelCollectionable;
    }

    public GameData(int[][] _time)
    {
        time = _time;
    }
}
