/*
 * File :   DistributeUIController.cs
 * Desc :   배급 여부 선택씬
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;


public class DistributeUIController : MonoBehaviour
{
    public Toggle playerWaterToggle;
    public Toggle playerFoodToggle;
    public Toggle guardWaterToggle;
    public Toggle guardFoodToggle;
    public Toggle doctorWaterToggle;
    public Toggle doctorFoodToggle;
    public Toggle engineerWaterToggle;
    public Toggle engineerFoodToggle;

    public Toggle playerMedToggle;
    public Toggle guardMedToggle;
    public Toggle engineerMedToggle;
    public Toggle doctorMedToggle;
    public Image medImage;
    public TMP_Text medNumber;

    public Image guardDeath;
    public Image engineerDeath;
    public Image doctorDeath;

    public Button NextDayButton;

    public GameObject distributeUI;
    public GameObject makeMedicineUI;
    public GameObject storyEventUI;

    public TMP_Text waterNumber;
    public TMP_Text foodNumber;

    private float currentWater;
    private float currentFood;
    private int currentMedKit;
    public Button MakeMedicineButton;

    private Distribute distributor = new Distribute();
    private NextDay nextDay = new NextDay();

    void Start()
    {
        currentWater = GameManager.Instance.itemData.waterN;
        currentFood = GameManager.Instance.itemData.foodN;
        currentMedKit = GameManager.Instance.itemData.medKitN;

        UpdateDeath();
        AddToggleListeners();
        UpdateNumberText();
        EnforceToggleLimits();
        UpdateMedicalUI();

        NextDayButton.onClick.AddListener(() =>
        {
            GameManager.Instance.nextDayLog = "";

            distributor.DistributeWater(GameManager.Instance.player, playerWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.player, playerFoodToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.doctor, doctorWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.doctor, doctorFoodToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.engineer, engineerWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.engineer, engineerFoodToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.guard, guardWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.guard, guardFoodToggle.isOn);

            distributor.DistributeMedKit(GameManager.Instance.player, playerMedToggle.isOn);
            distributor.DistributeMedKit(GameManager.Instance.guard, guardMedToggle.isOn);
            distributor.DistributeMedKit(GameManager.Instance.engineer, engineerMedToggle.isOn);
            distributor.DistributeMedKit(GameManager.Instance.doctor, doctorMedToggle.isOn);

            nextDay.RunAllNextDay();
            if (!GameManager.Instance.player.isAlive)
            {
                return;
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

            var item = GameManager.Instance.itemData;
            Debug.Log(
                $"[ItemData]\n" +
                $"Food: {item.foodN}\n" +
                $"Water: {item.waterN}\n" +
                $"Dive Kit: {item.diveKitN}\n" +
                $"Game Kit: {item.gameKitN}\n" +
                $"Med Kit: {item.medKitN}\n" +
                $"Book: {item.bookN}\n" +
                $"Map: {item.mapN}\n" +
                $"Research: {item.researchN}\n" +
                $"Battery: {item.batteryN}\n"
            );
            UpdateDeath();
            SceneManager.LoadScene("Main2DScene");
        });
        MakeMedicineButton.onClick.AddListener(() =>
        {
            SwitchToMakeMedicine();
        });
    }
    void OnEnable()
    {
        currentWater = GameManager.Instance.itemData.waterN;
        currentFood = GameManager.Instance.itemData.foodN;
        currentMedKit = GameManager.Instance.itemData.medKitN;

        UpdateDeath();
        UpdateNumberText();
        EnforceToggleLimits();
    }
    void AddToggleListeners()
    {
        playerWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));
        guardWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));
        doctorWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));
        engineerWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));

        playerFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));
        guardFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));
        doctorFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));
        engineerFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));

        playerMedToggle.onValueChanged.AddListener(val => OnMedToggleChanged(val));
        guardMedToggle.onValueChanged.AddListener(val => OnMedToggleChanged(val));
        doctorMedToggle.onValueChanged.AddListener(val => OnMedToggleChanged(val));
        engineerMedToggle.onValueChanged.AddListener(val => OnMedToggleChanged(val));
    }
    void OnToggleChanged(bool isOn, bool isWater)
    {
        float change = isOn ? -0.25f : 0.25f;

        if (isWater)
            currentWater += change;
        else
            currentFood += change;

        UpdateNumberText();
        EnforceToggleLimits();
    }
    void OnMedToggleChanged(bool isOn)
    {
        int change = isOn ? -1 : 1;

        currentMedKit += change;

        UpdateNumberText();
        EnforceToggleLimits();
    }
    void UpdateNumberText()
    {
        waterNumber.text = $"{currentWater:F2}";
        foodNumber.text = $"{currentFood:F2}";
        medNumber.text = $"x{currentMedKit}";
        UpdateMakeMedicineButton();
    }
    void EnforceToggleLimits()
    {
        Toggle[] waterToggles = { playerWaterToggle, guardWaterToggle, doctorWaterToggle, engineerWaterToggle };
        Toggle[] foodToggles = { playerFoodToggle, guardFoodToggle, doctorFoodToggle, engineerFoodToggle };
        Toggle[] medKitToggles = { playerMedToggle, guardMedToggle, doctorMedToggle, engineerMedToggle };

        foreach (var toggle in waterToggles)
        {
            if (!toggle.isOn)
                toggle.interactable = currentWater >= 0.25f;
        }

        foreach (var toggle in foodToggles)
        {
            if (!toggle.isOn)
                toggle.interactable = currentFood >= 0.25f;
        }
        foreach (var toggle in medKitToggles)
        {
            if (!toggle.isOn)
                toggle.interactable = currentMedKit >= 1;
        }
    }
    void UpdateMakeMedicineButton()
    {
        if (!GameManager.Instance.player.pharmacist)
        {
            MakeMedicineButton.gameObject.SetActive(false); // 약사 아닐시 숨김
        }
        else
        {
            MakeMedicineButton.gameObject.SetActive(true); // 약사일 시 표기
            MakeMedicineButton.interactable = currentFood >= 0.25f && currentWater >= 0.25f; // 약을 만들 자원이 없으면 비활성화
        }
    }
    void UpdateMedicalUI()
    {
        bool anySick = false;

        var player = GameManager.Instance.player;
        var guard = GameManager.Instance.guard;
        var engineer = GameManager.Instance.engineer;
        var doctor = GameManager.Instance.doctor;
        var item = GameManager.Instance.itemData;

        // 기본적으로 모두 숨김
        playerMedToggle.gameObject.SetActive(false);
        guardMedToggle.gameObject.SetActive(false);
        engineerMedToggle.gameObject.SetActive(false);
        doctorMedToggle.gameObject.SetActive(false);

        // 병에 걸린 캐릭터에 따라 토글 표시
        if (player != null && player.isSick)
        {
            playerMedToggle.gameObject.SetActive(true);
            playerMedToggle.interactable = item.medKitN >= 1;
            anySick = true;
        }

        if (guard != null && guard.isSick)
        {
            guardMedToggle.gameObject.SetActive(true);
            guardMedToggle.interactable = item.medKitN >= 1;
            anySick = true;
        }

        if (engineer != null && engineer.isSick)
        {
            engineerMedToggle.gameObject.SetActive(true);
            engineerMedToggle.interactable = item.medKitN >= 1;
            anySick = true;
        }

        if (doctor != null && doctor.isSick)
        {
            doctorMedToggle.gameObject.SetActive(true);
            doctorMedToggle.interactable = item.medKitN >= 1;
            anySick = true;
        }

        // 이미지 및 숫자 텍스트는 병든 사람 있을 때만 보이게
        medImage.gameObject.SetActive(anySick);
        medNumber.gameObject.SetActive(anySick);

        if (anySick)
            medNumber.text = $"x{item.medKitN}";
    }
    void UpdateDeath()
    {
        var player = GameManager.Instance.player;
        var doctor = GameManager.Instance.doctor;
        var guard = GameManager.Instance.guard;
        var engineer = GameManager.Instance.engineer;

        Toggle[] playerToggles = { playerFoodToggle, playerWaterToggle, playerMedToggle };
        Toggle[] doctorToggles = { doctorFoodToggle, doctorWaterToggle, doctorMedToggle };
        Toggle[] guardToggles = { guardFoodToggle, guardWaterToggle, guardMedToggle };
        Toggle[] engineerToggles = { engineerFoodToggle, engineerWaterToggle, engineerMedToggle };

    if (player == null)
        {
            Debug.Log("나 사망");
            foreach (var t in playerToggles) t.gameObject.SetActive(false);
        }
        

    if (doctor == null)
        {
            doctorDeath.gameObject.SetActive(true);
            AffectionUIController.Instance.CharacterDie("Doctor");
            foreach (var t in doctorToggles) t.gameObject.SetActive(false);
        }
        

    if (guard == null)
        {
            guardDeath.gameObject.SetActive(true);
            AffectionUIController.Instance.CharacterDie("Guard");
            foreach (var t in guardToggles) t.gameObject.SetActive(false);
        }
        

    if (engineer == null)
        {
            engineerDeath.gameObject.SetActive(true);
            AffectionUIController.Instance.CharacterDie("Engineer");
            foreach (var t in engineerToggles) t.gameObject.SetActive(false);
        }
        
    }
    void SwitchToMakeMedicine()
    {
        distributeUI.SetActive(false);
        makeMedicineUI.SetActive(true);
    }
    void SwitchToStoryEvent()
    {
        distributeUI.SetActive(false);
        storyEventUI.SetActive(true);
    }
    
}