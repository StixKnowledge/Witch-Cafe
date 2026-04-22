using System;
using System.Collections.Generic;

[System.Serializable]
public class DayMission
{
    public int dayNumber;
    public List<Mission> missions = new List<Mission>();
}