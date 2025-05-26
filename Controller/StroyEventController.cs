using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StoryEventController : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private Button beforeButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button nextButton2;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;
    [SerializeField] private GameObject distributeUI;
    [SerializeField] private GameObject storyUI;

    [System.Serializable]
    public class StoryEvent
    {
        public int day;
        public List<string> pages;
        public string acceptEvent;
        public string rejectEvent;
        public string acceptText;
        public string rejectText;
    }

    [System.Serializable]
    public class StoryEventList
    {
        public List<StoryEvent> events;
    }

    private List<string> pages;
    private int currentPage;
    private StoryEvent selectedEvent;

    void Start()
    {
        // 예시: currentDay 값에 따라 텍스트 구성
        int currentDay = GameManager.Instance.day;
        currentPage = 0;
        SetupButtonListeners();
        LoadEventForDay(currentDay);

        if (pages == null || pages.Count == 0)
        {
            Debug.Log("이벤트 없음");
            return;
        }

        UpdateUI();


    }

    void LoadEventForDay(int day)
    {
        string log = GameManager.Instance.nextDayLog;
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/StoryEvents");
        if (jsonFile == null)
        {
            Debug.LogError("이벤트 파일을 찾지 못했습니다");
            return;
        }

        StoryEventList data = JsonUtility.FromJson<StoryEventList>(jsonFile.text);

        List<StoryEvent> candidates = data.events.FindAll(e => e.day == day);
        if (candidates.Count == 0)
        {
            if (!string.IsNullOrEmpty(log))
            {
                ShowResult(log);
                selectedEvent = null;
            }
            else
            {
                SwitchToDistribute();
            }
            return;
        }
        // 이벤트 무작위 선택
        selectedEvent = candidates[Random.Range(0, candidates.Count)];
        pages = selectedEvent.pages;
        
        if (!string.IsNullOrEmpty(log))
        {
            pages.Insert(0, $"{log}");
        }
    }


    void SetupButtonListeners()
    {
        beforeButton.onClick.AddListener(() =>
        {
            if (currentPage > 0)
                currentPage--;
            UpdateUI();
        });

        nextButton.onClick.AddListener(() =>
        {
            if (currentPage < pages.Count - 1)
                currentPage++;
            UpdateUI();
        });

        acceptButton.onClick.AddListener(() =>
        {
            bool RA = Random.value < GameManager.Instance.player.randomActingProb; // 단기 기억 상실 선택시 10% 확률

            if (RA)
            {
                Debug.Log("10% 확률 반대 선택");
                HandleEvent(selectedEvent.rejectEvent);
                string result = selectedEvent.rejectText;
                ShowResult("갑자기 머리가 어지럽다. 내 의도랑 반대의 선택을 해버렸다..\n" + result);
            }
            else
            {
                HandleEvent(selectedEvent.acceptEvent);
                string result = selectedEvent.acceptText;
                ShowResult(result);
            }
        });

        rejectButton.onClick.AddListener(() =>
        {
            bool RA = Random.value < GameManager.Instance.player.randomActingProb; // 단기 기억 상실 선택시 10% 확률

            if (RA)
            {
                Debug.Log("10% 확률 반대 선택");
                HandleEvent(selectedEvent.acceptEvent);
                string result = selectedEvent.acceptText;
                ShowResult("갑자기 머리가 어지럽다. 내 의도랑 반대의 선택을 해버렸다..\n" + result);
            }
            else
            {
                HandleEvent(selectedEvent.rejectEvent);
                string result = selectedEvent.rejectText;
                ShowResult(result);
            }
        });

        nextButton2.onClick.AddListener(() =>
        {
            SwitchToDistribute();
        });
    }

    void UpdateUI()
    {
        storyText.text = pages[currentPage];

        beforeButton.gameObject.SetActive(currentPage > 0);
        nextButton.gameObject.SetActive(currentPage < pages.Count - 1);

        bool onLastPage = currentPage == pages.Count - 1;
        acceptButton.gameObject.SetActive(onLastPage);
        rejectButton.gameObject.SetActive(onLastPage);
    }
    void HandleEvent(string Key)
    {
        switch (Key)
        {
            case "GetFood":
                GameManager.Instance.itemData.foodN += 1f;
                break;
            case "LoseFood":
                GameManager.Instance.itemData.foodN -= 1f;
                break;
            case "GetWater":
                GameManager.Instance.itemData.waterN += 1f;
                break;
            case "LoseWater":
                GameManager.Instance.itemData.waterN -= 1f;
                break;
            case "GetMental":
                foreach (CharacterData cd in GameManager.Instance.characterList)
                {
                    if (cd == null) continue;
                    cd.mental += 5;
                }
                break;
            case "LoseMental":
                foreach (CharacterData cd in GameManager.Instance.characterList)
                {
                    if (cd == null) continue;
                    cd.mental -= 5;
                }
                break;
            case "GoAdventure":
                SceneManager.LoadScene("Adventure");
                break;
            case "GetMedKit":
                GameManager.Instance.itemData.medKitN += 1;
                break;
            case "LoseHealth":
                GameManager.Instance.player.health -= 5;
                break;
            case "LoseMuchHealth":
                GameManager.Instance.player.health -= 20;
                break;
            case "TryGetBetter":
                if (GameManager.Instance.itemData.medKitN >= 1)
                {
                    GameManager.Instance.itemData.medKitN -= 1;
                    if (GameManager.Instance.doctor != null)
                        GameManager.Instance.player.health += 5;
                    else
                        GameManager.Instance.player.health -= 10;
                }
                else
                {
                    HandleEvent("LoseMuchHealth");
                }
                break;
            case "GetBattery":
                GameManager.Instance.itemData.batteryN += 1;
                break;
            case "TryFix":
                if (GameManager.Instance.itemData.fixtoolN >= 1)
                {
                    GameManager.Instance.itemData.fixtoolN-=1;
                    int n = Random.Range(0, 10);
                    if (n > 6)
                        GameManager.Instance.player.health -= 5;
                }
                else
                {
                    HandleEvent("TryNotMove");
                }
                break;
            case "TryNotMove":
                foreach (CharacterData cd in GameManager.Instance.characterList)
                {
                    if (cd == null) continue;
                    cd.health -= 10;
                }
                break;
            case "TryFun":
                if (GameManager.Instance.itemData.gameKitN >= 1)
                {
                    foreach (CharacterData cd in GameManager.Instance.characterList)
                    {
                        if (cd == null) continue;
                        cd.mental += 10;
                    }
                }
                else
                {

                    foreach (CharacterData cd in GameManager.Instance.characterList)
                    {
                        if (cd == null) continue;
                        cd.mental -= 20;
                    }
                }
                break;
            case "TryNotFun":
                foreach (CharacterData cd in GameManager.Instance.characterList)
                {
                    if (cd == null) continue;
                    cd.mental -= 10;
                }
                break;
            default:
                Debug.Log("이벤트 없음 또는 처리 없음");
                break;
        }

    }
    void ShowResult(string resultText)
    {
        storyText.text = resultText;
        acceptButton.gameObject.SetActive(false);
        rejectButton.gameObject.SetActive(false);
        beforeButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton2.gameObject.SetActive(true);
    }
    void SwitchToDistribute()
    {
        storyUI.SetActive(false);
        distributeUI.SetActive(true);
    }
}