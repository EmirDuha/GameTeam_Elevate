using UnityEngine;

public enum DoorState
{
    autoOpen,
    autoClosed,
    manualOpen,
    manualClosed
}

public class DoorController : MonoBehaviour
{
    [SerializeField] private DoorState currentState;
    [SerializeField] private GameObject doorPanel;

    public bool IsDoorOpen => 
    currentState == DoorState.autoOpen || currentState == DoorState.manualOpen;

    private void Start()
    {
        currentState = DoorState.autoOpen;
    }

    public void SetDoorState(DoorState newState)
    {

        if (currentState == newState)
            return;
            
        currentState = newState;
        HandleDoorState();
    }

    private void HandleDoorState()
    {

        switch(IsDoorOpen)
        {
            case true:
                doorPanel.SetActive(true);
                break;
            case false:
                doorPanel.SetActive(false);
                break;
        }

        switch (currentState)
        {
            case DoorState.autoOpen:
                DoorAutoOpen();
                break;
            case DoorState.autoClosed:
                DoorAutoClosed();
                break;
            case DoorState.manualOpen:
                DoorManualOpen();
                break;
            case DoorState.manualClosed:
                DoorManualClosed();
                break;
        }
    }

    private void DoorAutoOpen()
    {
        Debug.Log("Door is open");

    }

    private void DoorAutoClosed()
    {
        Debug.Log("Door is closed");
    }

    private void DoorManualOpen()
    {
        Debug.Log("Door is manually open");
    }

    private void DoorManualClosed()
    {
        Debug.Log("Door is manually closed");
    }
}
