using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class itemSelect : MonoBehaviour, IPointerClickHandler
{
    public Image Image; // 클릭 할 이미지
    public Sprite[] sprites; // 이미지 목록

    public Inventory Inventory; // 인벤토리 딕셔너리
    public Dictionary<string, Sprite> spriteMap = new Dictionary<string, Sprite>(); // 이미지 딕셔너리
    public List<string> list = new List<string>();

    public AdvCharacter character;
    Status status;

    public int index = 0;

    void Start()
    {
        status = character.GetComponent<Status>();

        spriteMap.Add("Hands", sprites[0]);
        spriteMap.Add("Food", sprites[1]);
        spriteMap.Add("Water", sprites[2]);
        spriteMap.Add("Medicine", sprites[3]);
        spriteMap.Add("Tool", sprites[4]);
        spriteMap.Add("Battery", sprites[5]);
        spriteMap.Add("Oxygentank", sprites[6]);
        spriteMap.Add("Game", sprites[7]);
        spriteMap.Add("Map", sprites[8]);
        spriteMap.Add("Light", sprites[9]);
        spriteMap.Add("Research", sprites[10]); 
        

        var filteredItem = Inventory.inventory.Where(pair => pair.Value > 0).ToList();  // value가 0보다 큰 것만 골라냄
        sprites = filteredItem.Select(pair => spriteMap[pair.Key]).ToArray();
        list = filteredItem.Select(pair => pair.Key).ToList();

        if (sprites.Length > 0)
            Image.sprite = sprites[0];
    }

    public void setSprite()
    {
        status = character.GetComponent<Status>();

        var filteredItem = Inventory.inventory.Where(pair => pair.Value > 0).ToList();  // value가 0보다 큰 것만 골라냄
        sprites = filteredItem.Select(pair => spriteMap[pair.Key]).ToArray();
        list = filteredItem.Select(pair => pair.Key).ToList();

        if (sprites.Length > 0)
            Image.sprite = sprites[0];
    }

    public void OnPointerClick(PointerEventData eventdata)
    {
        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }

        Image.sprite = sprites[index];
    }

    public void sendToAdvItem() // 모험 캐릭터에게 아이템을 보냄
    {
        string key = list[index];

        if (index < 0 || index >= list.Count)
        {
            Debug.LogWarning($"인덱스 범위 초과: index={index}, list.Count={list.Count}");
            return;
        }

        if (key == "Hands")
        {
            status.itemList.Add("none"); // 아이템 없음 표시
            return;
        }
        status.itemList.Add(key);

        if (Inventory.inventory.ContainsKey(key) && Inventory.inventory[key] > 0)
        {
            Inventory.inventory[key]--; // 값 1 감소
        }

    }
}
