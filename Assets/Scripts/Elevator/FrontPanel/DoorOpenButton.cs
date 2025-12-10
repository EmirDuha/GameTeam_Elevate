using UnityEngine;

public class DoorOpenButton : MonoBehaviour
{
    [SerializeField] private DoorController doorController;

    public void Press()
    {
        doorController.SetDoorState(DoorState.manualOpen);
    }
}
