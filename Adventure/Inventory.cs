using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>()
    {
        {"Hands", 1 },
        {"Food", 1},
        {"Water", 1},
        {"Medicine", 1},
        {"Tool", 1 },
        {"Battery", 1},
        {"Oxygentank", 1},
        {"Game", 1 },
        {"Map", 1},
        {"Light", 1},
        {"Research", 1},
    };

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (KeyValuePair<string, int> pair in inventory)
            {
                Debug.Log($"Key: {pair.Key}, Value: {pair.Value}");
            }
        }
    }

    public void addItem(string name, int count)
    {
        if (inventory.ContainsKey(name))
        {
            inventory[name] += count;
        }
        else
        {
            inventory[name] = count;
        }
    }
}
