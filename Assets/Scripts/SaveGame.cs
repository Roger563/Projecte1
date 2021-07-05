using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveGame
{
    public static void _SaveGame(int _levelsCompleted, short[] _levelTrophy, bool[] _levelCollectionable)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameState.magneticRecovery";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(_levelsCompleted, _levelTrophy,_levelCollectionable);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void _SaveHighScore(int[][] _time)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highScores.magneticRecovery";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(_time);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/highScores.magneticRecovery";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    public static void FileExist()
    {
        string path = Application.persistentDataPath + "/gameState.magneticRecovery";
        string path2 = Application.persistentDataPath + "/highScores.magneticRecovery";
        if (!File.Exists(path))
        {
            short[] trophy = new short[16];
            bool[] collectionable = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                trophy[i] = 0;
                collectionable[i] = false;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            GameData data = new GameData(0, trophy,collectionable);
            formatter.Serialize(stream, data);
            stream.Close();
        }
       
        
        if (!File.Exists(path2))
        {
            int[][] arr = new int[16][];
            for (int i = 0; i < 16; i++)
            {
                arr[i] = new int[3];
                for(int j = 0; j < 3; j++)
                {
                    arr[i][j] = 999;
                }
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path2, FileMode.Create);
            GameData data = new GameData(arr);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
}
