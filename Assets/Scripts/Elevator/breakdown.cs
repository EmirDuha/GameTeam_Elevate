using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BreakdownManager : MonoBehaviour
{
    [Header("References")]
    public ElevatorController elevator;
    public DoorController doorController;
    public List<Button> floorButtons;

    [Header("Risk Settings")]
    public float quickMoveThreshold = 1.5f;
    public int maxRisk = 3;

    private int riskLevel = 0;
    private float timeSinceLastMove = 99f;
    private bool inBreakdown = false;

    public bool IsInBreakdown => inBreakdown;

    private void Update()
    {
        if (!inBreakdown)
            timeSinceLastMove += Time.deltaTime;
    }

    // Asansör her kata ulaştığında çağırılır
    public void NotifyFloorReached()
    {
        if (inBreakdown) return;

        // Hızlı kullanıldı mı?
        if (timeSinceLastMove < quickMoveThreshold)
        {
            riskLevel++;
            TryBreakdown();
        }

        timeSinceLastMove = 0f;
    }

    private void TryBreakdown()
    {
        if (riskLevel < maxRisk) return;

        StartCoroutine(BreakdownSequence());
    }

    private IEnumerator BreakdownSequence()
    {
        inBreakdown = true;

        Debug.Log("🔥 ASANSÖR ARIZAYA GİRDİ!");

        // Butonları kilitle
        foreach (var b in floorButtons)
            b.interactable = false;

        // Kapıyı açık bırak
        doorController.SetDoorState(DoorState.autoOpen);

        // Rastgele aşağı düşürme
        int current = elevator.GetCurrentFloor();
        int drop = Random.Range(1, 4);
        int newFloor = Mathf.Max(1, current - drop);

        elevator.ForceMoveInstant(newFloor);

        yield break;
    }

    public void ResetBreakdown()
    {
        inBreakdown = false;
        riskLevel = 0;

        foreach (var b in floorButtons)
            b.interactable = true;

        Debug.Log("Breakdown resetlendi.");
    }
}
