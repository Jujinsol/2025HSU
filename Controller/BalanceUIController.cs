/*
 * File :   BuffAndNerf.cs
 * Desc :   버프와 너프 선택 및 남은 포인트 표시
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BalanceUIController : MonoBehaviour
{
    public Toggle buff1, buff2, buff3, buff4, buff5, buff6, buff7, buff8, buff9, buff10;
    public Toggle nerf1, nerf2, nerf3, nerf4, nerf5, nerf6, nerf7, nerf8, nerf9, nerf10;
    public TMP_Text pointText;
    public Button fixButton;
    public int point = 10;

    private BuffAndNerf buffAndNerf = new BuffAndNerf();

    void Start()
    {
        AddToggleListener(buff1, 1, isBuff: true);
        AddToggleListener(buff2, 2, isBuff: true);
        AddToggleListener(buff3, 3, isBuff: true);
        AddToggleListener(buff4, 4, isBuff: true);
        AddToggleListener(buff5, 5, isBuff: true);
        AddToggleListener(buff6, 6, isBuff: true);
        AddToggleListener(buff7, 7, isBuff: true);
        AddToggleListener(buff8, 8, isBuff: true);
        AddToggleListener(buff9, 9, isBuff: true);
        AddToggleListener(buff10, 10, isBuff: true);

        AddToggleListener(nerf1, 1, isBuff: false);
        AddToggleListener(nerf2, 2, isBuff: false);
        AddToggleListener(nerf3, 3, isBuff: false);
        AddToggleListener(nerf4, 4, isBuff: false);
        AddToggleListener(nerf5, 5, isBuff: false);
        AddToggleListener(nerf6, 6, isBuff: false);
        AddToggleListener(nerf7, 7, isBuff: false);
        AddToggleListener(nerf8, 8, isBuff: false);
        AddToggleListener(nerf9, 9, isBuff: false);
        AddToggleListener(nerf10, 10, isBuff: false);

        UpdatePointText();        
        fixButton.onClick.AddListener(() =>
        {
            if (point >= 0)
            {
                SceneManager.LoadScene("Main2DScene");
            }
        });
    }

    void AddToggleListener(Toggle toggle, int value, bool isBuff)
    {
        toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isBuff)
            {
                int b = buffAndNerf.BuffPoint(value);
                point += isOn ? -b : b;
            }
            else
            {
                int n = buffAndNerf.NerfPoint(value);
                point += isOn ? n : -n;
            }

            UpdatePointText();
        });

        fixButton.onClick.AddListener(() =>
        {
            if (point < 0)
            {
                Debug.Log("포인트가 부족합니다.");
            }
            else if (isBuff)
            {
                if (toggle.isOn)
                    buffAndNerf.FixBuff(value);
            }
            else
            {
                if (toggle.isOn)
                    buffAndNerf.FixNerf(value);
            }
            foreach (CharacterData cd in GameManager.Instance.characterList)
            {
                if (cd == null)
                    continue;

                Debug.Log(
                    $"Job: {cd.job}\n" +
                    $"Food: {cd.food}\n" +
                    $"Water: {cd.water}\n" +
                    $"Electricity: {cd.electricity}\n" +
                    $"Mental: {cd.mental}\n" +
                    $"Affection: {cd.affection}\n" +
                    $"Is Sick: {cd.isSick}\n" +
                    $"Is Alive: {cd.isAlive}\n" +
                    $"Death Reason: {cd.deathReason}\n" +
                    // 증가·감소 단위
                    $"Food Gain Unit: {cd.foodGainUnit}\n" +
                    $"Food Loss Unit: {cd.foodLossUnit}\n" +
                    $"Water Gain Unit: {cd.waterGainUnit}\n" +
                    $"Water Loss Unit: {cd.waterLossUnit}\n" +
                    $"Electricity Gain Unit: {cd.electricityGainUnit}\n" +
                    $"Electricity Loss Unit: {cd.electricityLossUnit}\n" +
                    $"Mental Gain Unit: {cd.mentalGainUnit}\n" +
                    $"Mental Loss Unit: {cd.mentalLossUnit}\n" +
                    $"Affection Gain Unit: {cd.affectionGainUnit}\n" +
                    $"Affection Loss Unit: {cd.affectionLossUnit}\n" +
                    // 확률 관련
                    $"Facility Fail Prob: {cd.facilityFailProb}\n" +
                    $"Random Acting Prob: {cd.randomActingProb}\n" +
                    $"Repair Prob: {cd.repairProb}\n" +
                    $"Death Prob: {cd.deathProb}\n" +
                    // 그 외
                    $"Mission Days: {cd.missionDays}\n" +
                    $"Survival Pro: {cd.survivalPro}\n" +
                    $"Good Leader: {cd.goodLeader}"
                );
            }
        });
    }
    
    void UpdatePointText()
    {
        pointText.text = $"Point: {point}";
    }
}

