using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DoorController doorController;
    [SerializeField] private DayManager dayManager;

    [Header("Settings")]
    [SerializeField] private float timePerFloor = 1.0f; // Her kat arası bekleme süresi
    
    private int currentFloor = 1; // Başlangıç katı
    private bool isMoving = false;

    // UI Butonundan bu fonksiyon çağırılacak
    public void GoToFloor(int targetFloor)
    {
        
        if (isMoving || currentFloor == targetFloor) return;

        
        StartCoroutine(MoveProcess(targetFloor));
    }

    private IEnumerator MoveProcess(int targetFloor)
    {
        isMoving = true;
        Debug.Log("Asansör hareket ediyor...");

        //  Kapıyı Otomatik Kitle
        doorController.SetDoorState(DoorState.autoClosed);

        yield return new WaitForSeconds(0.5f);

        //  Hareket Süresini Hesapla (Kat farkı * süre)
        int floorDifference = Mathf.Abs(targetFloor - currentFloor);
        yield return new WaitForSeconds(floorDifference * timePerFloor);

        //  Hedef Kata Ulaş
        currentFloor = targetFloor;
        Debug.Log("Kata ulaşıldı: " + currentFloor);

        //  Kapıyı Otomatik Aç
        doorController.SetDoorState(DoorState.autoOpen);

        // Hedef Kontrolü
        CheckTargetFloor();

        isMoving = false;
    }

    private void CheckTargetFloor()
    {
        if (currentFloor == dayManager.GetCurrentTargetFloor())
        {
            dayManager.CompleteDay(); 
        }
    }
}
