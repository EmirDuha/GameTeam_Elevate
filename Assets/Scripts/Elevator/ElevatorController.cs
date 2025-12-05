using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DoorController doorController;
    [SerializeField] private DayManager dayManager;
    [SerializeField] private BreakdownManager breakdownManager;

    [Header("Settings")]
    [SerializeField] private float timePerFloor = 1.0f; // Her kat arası bekleme süresi

    private int currentFloor; // Başlangıç katı
    private bool isMoving = false;

    private void Start()
    {
        currentFloor = dayManager.firstFloor; // Başlangıç katını ayarla
    }

    // Şu anki katı döndürür
    public int GetCurrentFloor()
    {
        return currentFloor;
    }

    // UI Butonundan bu fonksiyon çağırılacak
    public void GoToFloor(int targetFloor)
    {
        if (currentFloor == targetFloor)
            return;

        else
        {
            if (breakdownManager.CheckBreakdownBeforeMove())
                return;

            if (isMoving) return;

            StartCoroutine(MoveProcess(targetFloor));
        }
    }


    private IEnumerator MoveProcess(int targetFloor)
    {
        isMoving = true;

        // Kapıyı Otomatik Kapat
        doorController.SetDoorState(DoorState.autoClosed);

        yield return new WaitForSeconds(0.5f);

        // Hareket Süresi
        int floorDifference = Mathf.Abs(targetFloor - currentFloor);
        yield return new WaitForSeconds(floorDifference * timePerFloor);

        // Hedef Kata Ulaş
        currentFloor = targetFloor;
        Debug.Log("Kata ulaşıldı: " + currentFloor);

        // Kapıyı Otomatik Aç
        doorController.SetDoorState(DoorState.autoOpen);

        // Breakdown Manager'a haber ver
        if (breakdownManager != null)
            breakdownManager.NotifyFloorReached();

        // Hedef Kontrolü
        if (currentFloor == dayManager.GetCurrentTargetFloor())
        {
            dayManager.CompleteDay();
            currentFloor = dayManager.firstFloor;
        }

        isMoving = false;
    }

    // Breakdown için anında zıplatma
    public void ForceMoveInstant(int newFloor)
    {
        currentFloor = newFloor;
        Debug.Log("Force Move → Yeni kat: " + newFloor);

        // Anında kapıyı açık bırak
        doorController.SetDoorState(DoorState.autoOpen);
    }

}

