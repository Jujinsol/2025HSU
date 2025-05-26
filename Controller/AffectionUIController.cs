/*
 * File :   AffectionUIController.cs
 * Desc :   호감도 이벤트 UI 제어
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class AffectionUIController : MonoBehaviour
{
    public Button goodButton;        // 좋은 선택
    public Button badButton;         // 나쁜 선택

    public Button doctorButton;      // 의사 선택
    public Button engineerButton;    // 정비공 선택
    public Button guardButton;       // 경비 선택

    public TMP_Text npcTalk;         // 대사 출력 텍스트

    public GameObject affectionObject; // 호감도 UI 전체 오브젝트

    private StatChanger statChanger = new StatChanger();
    private string selectedJob = null;

    public static AffectionUIController Instance;

    void Start()
    {
        Instance = this;

        // 처음엔 UI 숨김
        affectionObject.SetActive(false);

        if (!Convert.ToBoolean(Storage.inst.white))
            doctorButton.gameObject.SetActive(false);
        else
        {
            if (GameManager.Instance.doctor == null)
                doctorButton.gameObject.SetActive(false);
        }
        if (!Convert.ToBoolean(Storage.inst.yellow))
            engineerButton.gameObject.SetActive(false);
        else
        {
            if (GameManager.Instance.engineer == null)
                engineerButton.gameObject.SetActive(false);
        }
        if (!Convert.ToBoolean(Storage.inst.blue))
            guardButton.gameObject.SetActive(false);
        else
        {
            if (GameManager.Instance.guard == null)
                guardButton.gameObject.SetActive(false);
        }

        //if (!GameManager.Instance.doctor.isAlive) doctorButton.gameObject.SetActive(false);
        //if (!GameManager.Instance.guard.isAlive) guardButton.gameObject.SetActive(false);
        //if (!GameManager.Instance.engineer.isAlive) engineerButton.gameObject.SetActive(false);

        // 직업 버튼 클릭 시 호출
        doctorButton.onClick.AddListener(() => ShowAffectionUI("Doctor"));
        engineerButton.onClick.AddListener(() => ShowAffectionUI("Engineer"));
        guardButton.onClick.AddListener(() => ShowAffectionUI("Guard"));

        // 선택 버튼, 질문에 따라 위치 바꾸기?
        goodButton.onClick.AddListener(() => ApplyAffectionChange(true));
        badButton.onClick.AddListener(() => ApplyAffectionChange(false));

        
    }

    void ShowAffectionUI(string job)
    {
        Debug.Log("버튼이 눌렸습니다!");
        selectedJob = job;

        npcTalk.text = job switch
        {
            "Doctor" => "의사 호감도",
            "Engineer" => "정비공 호감도",
            "Guard" => "경비원 호감도",
            _ => ""
        };

        affectionObject.SetActive(true);
    }

    void ApplyAffectionChange(bool isGood)
    {
        CharacterData target = selectedJob switch
        {
            "Doctor" => GameManager.Instance.doctor,
            "Engineer" => GameManager.Instance.engineer,
            "Guard" => GameManager.Instance.guard,
            _ => null
        };

        if (target == null)
        {
            Debug.LogWarning("해당 캐릭터 없음");
            affectionObject.SetActive(false);
            return;
        }

        if (isGood)
            statChanger.GainAffection(target, 4);
        else
            statChanger.LoseAffection(target, 2);

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
        doctorButton.interactable = false;
        engineerButton.interactable = false;
        guardButton.interactable = false;

        affectionObject.SetActive(false);
    }

    public void CharacterDie(string job)
    {
        switch (job)
        {
            case "Engineer":
                Debug.Log("정비공 사망");
                break;
            case "Doctor":
                Debug.Log("의사 사망");
                break;
            case "Guard":
                Debug.Log("경비원 사망");
                break;
        }
    }
}