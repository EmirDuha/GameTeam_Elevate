using UnityEngine;

public enum EmergencyExitState
{
    safe,
    threatLevel1,
    threatLevel2,
    threatLevel3,
    jumpscare
}

public class EmergencyExitStates : MonoBehaviour
{
    [SerializeField] private EmergencyExitState currentState;
    [SerializeField] private GameObject emergencyExitPanelSafe;
    [SerializeField] private GameObject emergencyExitPanel_1;
    [SerializeField] private GameObject emergencyExitPanel_2;
    [SerializeField] private GameObject emergencyExitPanel_3;
    [SerializeField] private StressSystem stressSystem;

    private void Start()
    {
        currentState = EmergencyExitState.safe;
        UpdateEmergencyExitPanel();
    }

    public void SetEmergencyExitState(EmergencyExitState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        UpdateEmergencyExitPanel();
    }

    private void UpdateEmergencyExitPanel()
    {
        switch (currentState)
        {
            case EmergencyExitState.safe:
                emergencyExitPanelSafe.SetActive(true);
                foreach (var panel in new GameObject[]
                {
                emergencyExitPanel_1,
                emergencyExitPanel_2,
                emergencyExitPanel_3 })
                    panel.SetActive(false);
                break;

            case EmergencyExitState.threatLevel1:
                emergencyExitPanel_1.SetActive(true);
                foreach (var panel in new GameObject[]
                {
                emergencyExitPanelSafe,
                emergencyExitPanel_2,
                emergencyExitPanel_3 })
                    panel.SetActive(false);

                StressActivation();
                break;

            case EmergencyExitState.threatLevel2:
                emergencyExitPanel_2.SetActive(true);
                foreach (var panel in new GameObject[]
                {
                emergencyExitPanelSafe,
                emergencyExitPanel_1,
                emergencyExitPanel_3 })
                    panel.SetActive(false);

                StressActivation();
                break;

            case EmergencyExitState.threatLevel3:
                emergencyExitPanel_3.SetActive(true);
                foreach (var panel in new GameObject[]
                {
                emergencyExitPanelSafe,
                emergencyExitPanel_1,
                emergencyExitPanel_2 })
                    panel.SetActive(false);

                StressActivation();
                break;

            case EmergencyExitState.jumpscare:
                emergencyExitPanelSafe.SetActive(false);
                break;

            default:
                emergencyExitPanelSafe.SetActive(true);
                break;
        }
    }

    private void StressActivation()
    {
        stressSystem.isThreatActive = true;
    }
}
