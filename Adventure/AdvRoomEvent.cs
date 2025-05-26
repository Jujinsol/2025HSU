using UnityEngine;

// ���� �溰 �̺�Ʈ �۵�
public class AdvRoomEvent : MonoBehaviour
{
    // �Ϲݹ濡�� ȹ�� ����
    public int Food = 0;
    public int Water = 0;
    public int Battery = 0;
    public int Tool = 0;
    public int Medicine = 0;
    public int Research = 0;
    public int Oxygentank = 0;
    public int Game = 0;
    public int Map = 0;
    public int Ax = 0;
    public int Light = 0;
    public int Diver = 0;

    int randInt = 0;
    public bool endEvent;

    public GameObject GM;
    public GameObject Character;
    public Inventory inventory;

    EventList Event; // �̺�Ʈ �� �̺�Ʈ ����
    AdvCharacter AdvCharacter;

    void Start()
    {
        Event = GM.GetComponent<EventList>();
        AdvCharacter = Character.GetComponent<AdvCharacter>(); // ������ Ȱ���ϱ� ���� ����
    }
    public void getNormal()
    {
        randInt = Random.Range(0, 10);
        if (randInt <= 3)
        {
            Food++;
        }
        if (randInt > 3 && randInt <= 7)
        {
            Water++;
        }
        if (randInt > 7 && randInt <= 8)
        {
            Battery++;
        }
        if (randInt > 8 && randInt <= 9)
        {
            Tool++;
        }
        endEvent = true;
    }

    public void getEmpty()
    {
        Debug.Log("�� ���Դϴ�.");
        endEvent = true;
    }

    public void getEvent()
    {
        Event.callEvent();
    }

    public void sendItem() // Ž�� ���� �� �������� ����
    {
        inventory.addItem("Food", Food);
        inventory.addItem("Water", Water);
        inventory.addItem("Medicine", Medicine);
        inventory.addItem("Battery", Battery);
        inventory.addItem("Oxyentank", Oxygentank);
        inventory.addItem("Game", Game);
        inventory.addItem("Map", Map);
        inventory.addItem("Ax", Ax);
        inventory.addItem("Light", Light);
        inventory.addItem("Research", Research);
        inventory.addItem("Diver", Diver);

        GameManager.Instance.itemData.foodN += Food;
        GameManager.Instance.itemData.waterN += Water;
        GameManager.Instance.itemData.medKitN += Medicine;
        GameManager.Instance.itemData.batteryN += Battery;
        GameManager.Instance.itemData.oxyentankN += Oxygentank;
        GameManager.Instance.itemData.gameKitN += Game;
        GameManager.Instance.itemData.mapN += Map;
        //GameManager.Instance.itemData.Ax += Ax;
        GameManager.Instance.itemData.researchN += Research;
        GameManager.Instance.itemData.diveKitN += Diver;

        Food = 0;
        Water = 0;
        Battery = 0;
        Tool = 0;
        Medicine = 0;
        Research = 0;
        Oxygentank = 0;
        Game = 0;
        Map = 0;
        Ax = 0;
        Light = 0;
        Diver = 0;
    }

    public void deleteItem()
    {
        Food = 0;
        Water = 0;
        Battery = 0;
        Tool = 0;
        Medicine = 0;
        Research = 0;
        Oxygentank = 0;
        Game = 0;
        Map = 0;
        Ax = 0;
        Light = 0;
        Diver = 0;
    }
}
