/*
 * File :   UsingItem.cs
 * Desc :   
 */

using UnityEngine;
using System.Collections.Generic;

public class UsingItem
{
    public void MakeMedicine(int num)
    {
        if (GameManager.Instance.itemData.foodN >= 0.5f && GameManager.Instance.itemData.waterN >= 0.5f)
        {
            GameManager.Instance.itemData.foodN -= num * 0.5f;
            GameManager.Instance.itemData.waterN -= num * 0.5f;
            GameManager.Instance.itemData.medKitN += num;
        }
    }

    public void DestroyItem(int num)
    {
        var item = GameManager.Instance.itemData;

        for (int i = 0; i < num; i++)
        {
            List<System.Action> availableItems = new List<System.Action>();

            if (item.foodN > 0) availableItems.Add(() => item.foodN -= Mathf.Min(1f, item.foodN));
            if (item.waterN > 0) availableItems.Add(() => item.waterN -= Mathf.Min(1f, item.waterN));
            if (item.diveKitN > 0) availableItems.Add(() => item.diveKitN--);
            if (item.gameKitN > 0) availableItems.Add(() => item.gameKitN--);
            if (item.medKitN > 0) availableItems.Add(() => item.medKitN--);
            if (item.bookN > 0) availableItems.Add(() => item.bookN--);
            if (item.mapN > 0) availableItems.Add(() => item.mapN--);
            if (item.researchN > 0) availableItems.Add(() => item.researchN--);
            if (item.batteryN > 0) availableItems.Add(() => item.batteryN--);

            if (availableItems.Count == 0)
            {
                break;
            }

            int randIndex = Random.Range(0, availableItems.Count);
            availableItems[randIndex].Invoke();
        }
    }

    public void ElectricCharge()
    {
        var item = GameManager.Instance.itemData;
        var play = GameManager.Instance.player;
        if (item.batteryN < 1)
        {
            Debug.Log("battery is not enough");
        }
        else
        {
            item.batteryN -= 1;
            play.electricity += 10;
        }
    }
}
