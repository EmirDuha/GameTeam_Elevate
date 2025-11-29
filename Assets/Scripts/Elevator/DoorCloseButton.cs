using UnityEngine;

public class DoorCloseButton : MonoBehaviour
{
    [SerializeField] private DoorController doorController;

    public void Press()
    {
        doorController.SetDoorState(DoorState.manualClosed);
    }
}
