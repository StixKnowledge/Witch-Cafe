using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI missionTextUI;

    public CostumerManager CostumerManager;

    private void Update()
    {
        UpdateBalance(CostumerManager.shopBalance);
        UpdateMissionUI();
        dayText.text = $"Day {MissionManager.Instance.currentDay + 1}";
    }

    private void UpdateMissionUI()
    {
        if (missionTextUI == null || MissionManager.Instance.activeMission == null) return;

        missionTextUI.text = "";
        foreach (var mission in MissionManager.Instance.activeMission.missions)
        {
            missionTextUI.text += $"{mission.description}: {mission.currentValue}/{mission.targetValue}\n";
        }
    }

    public void UpdateBalance(float newBalance)
    {
        balanceText.text = "Sales: " + newBalance.ToString();
    }

}

