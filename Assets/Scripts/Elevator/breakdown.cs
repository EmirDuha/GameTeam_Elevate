using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BreakdownManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ElevatorController elevator;
    [SerializeField] private DoorController doorController;
    [SerializeField] private StressSystem stressSystem;
    [SerializeField] private List<Button> floorButtons;

    [Header("Risk Settings")]
    [SerializeField] private float quickMoveLimit = 5f; // Katlar arası max güvenli süre
    public int skippedFloorCount = 0;
    [SerializeField] private int quickMove_riskIncrease = 50; // Her hızlı çıkışta artan risk (%)
    [SerializeField] private int skippedFloor_riskIncrease = 20; // Atlanan her kat için artan risk (%)
    [SerializeField] private float stressIncrease = 2f;
    [SerializeField] private float stressDecrease = 2f;
    private int riskLevel = 0;
    private float timeSinceLastMove = 99f;
    private bool inBreakdown = false;

    public bool IsInBreakdown => inBreakdown;

    private void Update()
    {
        if (!inBreakdown)
            timeSinceLastMove += Time.deltaTime;
    }

    public void RiskCalculations()
    {
        if (inBreakdown) return;

        if (timeSinceLastMove < quickMoveLimit)
        {
            riskLevel += quickMove_riskIncrease;
            riskLevel = Mathf.Clamp(riskLevel, 0, 100);
            Debug.Log("⚠ Risk Arttı → %" + riskLevel);
        }

        timeSinceLastMove = 0f;

        if (skippedFloorCount > 0)
        {
            int addedRisk = skippedFloorCount * skippedFloor_riskIncrease;
            riskLevel += addedRisk;
            riskLevel = Mathf.Clamp(riskLevel, 0, 100);
            Debug.Log($"⚠ Atlanan {skippedFloorCount} kat için risk arttı → %" + riskLevel);
            skippedFloorCount = 0;
        }
    }

    public bool CheckBreakdownBeforeMove()
    {
        if (inBreakdown) return true;

        int roll = Random.Range(0, 100);
        Debug.Log($"🎲 Arıza Zar Atışı: {roll} (Risk: %{riskLevel})");

        if (roll < riskLevel)
        {
            StartCoroutine(BreakdownSequence());
            return true; 
        }

        return false; 
    }

    private IEnumerator BreakdownSequence()
    {
        inBreakdown = true;
        stressSystem.isUnderStress = true;
        stressSystem.stressIncreaseRate += stressIncrease;

        Debug.Log("🔥 ASANSÖR ARIZAYA GİRDİ!");

        foreach (var b in floorButtons)
            b.interactable = false;

        doorController.SetDoorState(DoorState.autoOpen);

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

        stressSystem.isUnderStress = false;
        stressSystem.stressIncreaseRate -= stressDecrease;

        foreach (var b in floorButtons)
            b.interactable = true;

        Debug.Log("🔧 Arıza Resetlendi");
    }
}
