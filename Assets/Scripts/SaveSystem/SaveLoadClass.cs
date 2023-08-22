using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveLoadClass
{
    public int highScore;
    public int lastGameScore;
    public int highCombo;
    public int lastComboScore;

    public SaveLoadClass(SphereStats sphereData)
    {
        highScore = sphereData.highScore;
        lastGameScore = sphereData.endScore;
        highCombo = sphereData.highCombo;
        lastComboScore = sphereData.endCombo;
    }
}
