using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject missionCompleteUI;
    public GameObject instructionUI;

    [SerializeField] HealthManager healthManager;
    [SerializeField] CostumerManager costumerManager;

    public event Action OnGameStarted;
    public event Action OnNextLevel;

    private void Start()
    {
        Time.timeScale = 0f;
        healthManager.OnHealthZero += GameOver;
        MissionManager.Instance.OnDayMissionComplete += MissionComplete;
        
    }
    void Update()
    {
        OnGamestart();
    }
    void OnGamestart()
    {
        if (Time.timeScale == 0 && Input.GetKey(KeyCode.Space))
        {
            OnGameStarted?.Invoke();
            instructionUI.SetActive(false);
            OnContinueTime();
        }
    }

    public void GameOver()
    {
        OnStopTime();
        costumerManager.orderQueue.Clear();
        gameOverUI.SetActive(true);

        //Reset all missions in the active day mission
        if (MissionManager.Instance.activeMission != null)
        {
            foreach (var mission in MissionManager.Instance.activeMission.missions)
            {
                mission.Reset();
            }
        }


    }

    void MissionComplete()
    {
        OnStopTime();
        
        missionCompleteUI.SetActive(true);

        // Add persistence or saving here later
    }

    public void OnNextLevelClicked()
    {
        healthManager.ResetLives();
        MissionManager.Instance.missionComplete = false;
        missionCompleteUI.SetActive(false);

        costumerManager.CheckCurrentDay();   // refresh queue first
        MissionManager.Instance.LoadDayMission(MissionManager.Instance.currentDay);

        OnNextLevel?.Invoke();               // now spawn works
        OnContinueTime();
    }

    public void OnPlayAgainClicked()
    {
        OnContinueTime();
        SceneManager.LoadScene("Game");
    }

    public void OnQuitClicked()
    {
        OnContinueTime();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnStopTime()
    {
        Time.timeScale = 0f;
    }

    private void OnContinueTime()
    {
        Time.timeScale = 1f;
    }
}

