using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MakeMedicineUIController : MonoBehaviour
{
    private UsingItem usingItem = new UsingItem();

    public GameObject distributeUI;
    public GameObject makeMedicineUI;

    public Button plusButton;
    public Button minusButton;
    public Button okButton;

    public TMP_Text countText;

    public TMP_Text waterNumber;
    public TMP_Text foodNumber;

    private float currentWater;
    private float currentFood;

    public int currentCount = 0;
    public int maxCount = 99;

    void Start()
    {
        currentWater = GameManager.Instance.itemData.waterN;
        currentFood = GameManager.Instance.itemData.foodN;

        UpdateText();
        UpdateNumberText();
        UpdateButtonInteractable();


        plusButton.onClick.AddListener(() =>
        {
            if (currentCount < maxCount)
            {
                currentCount++;
                UpdateText();
                currentWater -= 0.5f;
                currentFood -= 0.5f;
                UpdateNumberText();
                UpdateButtonInteractable();
            }

        });

        minusButton.onClick.AddListener(() =>
        {
            if (currentCount > 0)
            {
                currentCount--;
                UpdateText();
                currentWater += 0.5f;
                currentFood += 0.5f;
                UpdateNumberText();
                UpdateButtonInteractable();
            }

        });

        okButton.onClick.AddListener(() =>
        {
            usingItem.MakeMedicine(currentCount);

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
            SwitchToDistribute();
            currentCount = 0;
            UpdateText();
        });
    }
    void UpdateNumberText()
    {
        waterNumber.text = $"{currentWater:F2}";
        foodNumber.text = $"{currentFood:F2}";
    }
    void UpdateButtonInteractable()
    {
        plusButton.interactable = (currentCount < maxCount && currentWater >= 0.25f && currentFood >= 0.25f);
        minusButton.interactable = (currentCount > 0);
    }
    void UpdateText()
    {
        countText.text = currentCount.ToString();
    }
    void SwitchToDistribute()
    {
        makeMedicineUI.SetActive(false);
        distributeUI.SetActive(true);
    }
}
