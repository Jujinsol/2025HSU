using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TabletUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float offset;

    [SerializeField]
    private RectTransform TaskListUITransform;

    [SerializeField]
    private GameObject backgroundObject;

    [SerializeField]
    private GameObject taskObject;

    [SerializeField]
    private GameObject chargeObject;

    [SerializeField]
    private TMP_Text dayText;

    [SerializeField] 
    private Image electricityBar;

    [SerializeField]
    private Button ChargeButton;

    [SerializeField]
    private Button ChargeAccept;

    [SerializeField]
    private Button ChargeReject;

    [SerializeField]
    private TMP_Text chargeText;


    private bool isOpen = false;

    private float timer;

    private UsingItem usingItem =  new UsingItem();

    private const float maxElectricity = 100f;
    void Start()
    {
        UpdateElectricityBar();
        UpdateChargeButton();
        int currentDay = GameManager.Instance.day;
        dayText.text = $"{currentDay}일차";

        if (currentDay > 30)
        {
            SceneManager.LoadScene("SurviveEnding");
            return;
        }

        //시작할 때 닫힌 위치로 UI 이동
        float closedX = -(TaskListUITransform.sizeDelta.x - 50f);
        TaskListUITransform.anchoredPosition = new Vector2(closedX, TaskListUITransform.anchoredPosition.y);
        ChargeButton.onClick.AddListener(() =>
        {
            chargeObject.gameObject.SetActive(true);
            chargeText.text = "배터리를 교체해볼까? \r\n전기가 어느정도 회복될 거 같아!";
        });
        ChargeAccept.onClick.AddListener(() =>
        {
            usingItem.ElectricCharge();
            UpdateElectricityBar();
            UpdateChargeButton();
            chargeObject.gameObject.SetActive(false);
        });
        ChargeReject.onClick.AddListener(() =>
        {
            chargeObject.gameObject.SetActive(false);
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter == backgroundObject || eventData.pointerEnter == taskObject) {
            StopAllCoroutines();
            StartCoroutine(OpenAndHideUI());
        }
        else
        {
            Debug.Log("무시");
            return;
        }

    }

    private IEnumerator OpenAndHideUI()
    {
        isOpen = !isOpen;
        if (timer != 0f)
        {
            timer = 1f - timer;
        }

        while (timer <= 1f)
        {
            timer += Time.deltaTime * 2f;

            float start = isOpen ? -(TaskListUITransform.sizeDelta.x - 50f) : offset;
            float dest = isOpen ? offset : -(TaskListUITransform.sizeDelta.x - 50f);
            TaskListUITransform.anchoredPosition = new Vector2(Mathf.Lerp(start, dest, timer), TaskListUITransform.anchoredPosition.y);
            yield return null;
        }
    }
    void UpdateElectricityBar()
    {
        float ratio = Mathf.Clamp01(GameManager.Instance.player.electricity / maxElectricity);
        electricityBar.fillAmount = ratio;
    }
    void UpdateChargeButton()
    {
        var play = GameManager.Instance.player;
        var item = GameManager.Instance.itemData;
        if (play.electricity > 80 || item.batteryN < 1)
        {
            ChargeButton.gameObject.SetActive(false);
        }
        else
        {
            ChargeButton.gameObject.SetActive(true);
        }
    }
}