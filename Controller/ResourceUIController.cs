using UnityEngine;

public class ResourceUIController : MonoBehaviour
{
    public GameObject[] waterImages;  // water �̹��� 10��
    public GameObject[] foodImages;   // food �̹��� 10��

    void Start()
    {
        UpdateWaterDisplay(GameManager.Instance.itemData.waterN);
        UpdateFoodDisplay(GameManager.Instance.itemData.foodN);
    }

    public void UpdateWaterDisplay(float waterCount)
    {
        int activeCount = Mathf.Clamp(Mathf.FloorToInt(waterCount), 0, waterImages.Length);
        for (int i = 0; i < waterImages.Length; i++)
            waterImages[i].SetActive(i < activeCount);
    }

    public void UpdateFoodDisplay(float foodCount)
    {
        int activeCount = Mathf.Clamp(Mathf.FloorToInt(foodCount), 0, foodImages.Length);
        for (int i = 0; i < foodImages.Length; i++)
            foodImages[i].SetActive(i < activeCount);
    }
}