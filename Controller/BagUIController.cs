using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BagUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private RectTransform TaskListUITransform;
    [SerializeField]
    private float offset;
    [SerializeField] 
    private TMP_Text[] itemTexts;

    private bool isClose = true;
    private float timer;

    void Start()
    {
        // 시작 시 오른쪽 밖에 위치시키기
        float closedX = TaskListUITransform.sizeDelta.x;
        TaskListUITransform.anchoredPosition = new Vector2(closedX, TaskListUITransform.anchoredPosition.y);
        UpdateItemTexts();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(OpenAndHideUI());
        UpdateItemTexts();
    }

    private IEnumerator OpenAndHideUI()
    {
        isClose = !isClose;

        if (timer != 0f)
        {
            timer = 1f - timer;
        }

        float start = isClose ? offset : TaskListUITransform.sizeDelta.x;
        float dest = isClose ? TaskListUITransform.sizeDelta.x : offset;

        while (timer <= 1f)
        {
            timer += Time.deltaTime * 2f;

            float xPos = Mathf.Lerp(start, dest, timer);
            TaskListUITransform.anchoredPosition = new Vector2(xPos, TaskListUITransform.anchoredPosition.y);
            yield return null;
        }
    }
    void UpdateItemTexts()
    {
        ItemData item = GameManager.Instance.itemData;

        string[] values =
        {
            $"{item.foodN:F2}",
            $"{item.waterN:F2}",
            $"{item.diveKitN}",
            $"{item.gameKitN}",
            $"{item.medKitN}",
            $"{item.bookN}",
            $"{item.mapN}",
            $"{item.researchN}",
            $"{item.batteryN}",
            $"{item.fixtoolN}"
        };

        for (int i = 0; i < itemTexts.Length && i < values.Length; i++)
        {
            itemTexts[i].text = values[i];
        }
    }
}
