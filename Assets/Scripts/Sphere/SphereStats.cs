using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereStats : MonoBehaviour
{
    public int highScore;
    public int highCombo;
    public int endScore;
    public int endCombo;

    private void CheckHighScore(int scoreOne, int scoreTwo)
    {
        if(scoreOne > scoreTwo)
        {
            Debug.Log("New record!");
            scoreOne = scoreTwo;
        }

    }


    public void LoadData()
    {
        SaveLoadClass loadData = SaveLoadManager.LoadData();

        highScore = loadData.highScore;
        highCombo = loadData.highCombo;
        endScore = loadData.lastGameScore;
        endCombo = loadData.lastComboScore;

    }

    public void SaveData(int currentScore, int currentCombo, int maxCombo)
    {
        endScore = currentScore;
        endCombo = currentCombo;

        if(highScore < endScore)
        {
            highScore = endScore;
        }

        if(highCombo < maxCombo)
        {
            highCombo = maxCombo;
        }

        SaveLoadManager.SaveData(this);
    }


}
