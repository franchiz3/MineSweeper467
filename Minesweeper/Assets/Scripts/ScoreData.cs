using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreData
{
    public float highScore;
    public float lastScore;
    private string filePath;

    public ScoreData()
    {
        filePath = Application.dataPath + "/StreamingAssets\\Saves\\data.json";
        Initialize();
    }
    public void setHighScore(float score)
    {
        highScore = score;
        lastScore = score;
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonData);
    }

    public void setLastScore(float score)
    {
        lastScore = score;
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonData);
    }

    private void Initialize()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ScoreData temp = JsonUtility.FromJson<ScoreData>(jsonData);
            highScore = temp.highScore;
            lastScore = temp.lastScore;
        }
    }
}
