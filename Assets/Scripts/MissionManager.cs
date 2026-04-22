using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour //, IDataPersistence
{
    public static MissionManager Instance { get; private set; }

    [Header("Day Missions")]
    public List<DayMission> dayMissions = new List<DayMission>();
    public int currentDay = 0;

    //[Header("Mission UI")]
    //public TextMeshProUGUI missionTextUI; //dapat wala to dito
     
    public event Action OnDayMissionComplete;

    public DayMission activeMission;
    public bool missionComplete = false;
    public CostumerManager costumerManager;     //dapat wala to dito

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDayMission(currentDay);
    }


    public void LoadDayMission(int day)
    {
        activeMission = dayMissions.Find(m => m.dayNumber == day);

        if (activeMission != null)
        {
            foreach (var mission in activeMission.missions)
            {
                mission.OnMissionComplete += HandleMissionComplete;
            }
        }
        else
        {
            Debug.LogWarning($"No mission found for Day {day}");
        }
    }

    public void RegisterCustomerServed(float orderTotal)
    {
        if (activeMission == null) return;

        foreach (var mission in activeMission.missions)
        {
            if (mission.type == MissionType.ServeCustomers)
                mission.AddProgress(1);

            if (mission.type == MissionType.ReachSales)
                mission.AddProgress((int)orderTotal);
        }
        CheckDayMissionCompletion();
    }


    private void HandleMissionComplete(Mission mission)
    {
        Debug.Log($"?? Mission Complete: {mission.description}");
    }

    private void CheckDayMissionCompletion()
    {
        if (activeMission == null) return;

        bool allComplete = true;
        foreach (var mission in activeMission.missions)
        {
            if (!mission.IsComplete)
            {
                allComplete = false;
                break;
            }
        }

        if (allComplete)
        {
            Debug.Log($"? Day {activeMission.dayNumber} Missions Complete!");
            currentDay++;
            missionComplete = true;
            OnDayMissionComplete?.Invoke();
        }
    }
}

