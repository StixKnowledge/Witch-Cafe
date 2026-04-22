using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   // this attribute makes the class serializable by Unity, meaning its fields can be saved and loaded
public class GameData
{
    //this is where we define all the data we want to save
    public int dayCount;


    //the values is defined in this constructor will be the default values
    //the game starts with when there is no data to load
    public GameData()
    {
        dayCount = 0;
    }

}
