using TMPro;
using UnityEngine;

//���� ĳ���� �̵�
public class AdvCharacter : MonoBehaviour
{
    public int move; // �Ϸ翡 ������ �� �ִ� Ƚ��
    public bool moveLock = false; // �̺�Ʈ �� ������ ���
    public string area; // ������ ���� -> �̺�Ʈ, ������ ȹ�� ���°� �޶���
    public bool endCheck = false; // ���� üũ
    public int AdvDay; // ���� ���� ����
    public int advDate; // ���� ���� ���� �⺻ 3�� Ư�� 2��

    public GameObject GM;
    AdvRoomEvent advRoomEvent; // �濡�� �Ͼ�� �ϵ�
    public Status status; // ĳ������ ����

    public GameObject EL;
    EventList eventList;

    public GameObject advEndPanel;
    public TextMeshProUGUI itemList;

    public CoverSpwan coverSpwaner;
    public itemSelect itemSelect;

    public TextMeshProUGUI moveNum;

    void Start()
    {
        advRoomEvent = GM.GetComponent<AdvRoomEvent>();
        eventList = EL.GetComponent<EventList>();
    }
    void Update()
    {
        Vector2 pos = transform.position; //������Ʈ�� ��ġ ����
        if (move != 0 && moveLock == false)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // ������Ʈ�� ��ġ���� ��ǥ +1
            {
                pos += new Vector2(0f, 1f);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                pos += new Vector2(0f, -1f);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pos += new Vector2(-1f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                pos += new Vector2(1f, 0f);
            }

            if (pos.x >= 37 && pos.x <= 43 && pos.y <= 1.65 && pos.y >= -0.4)
            {
                transform.position = pos;
            }
        }

        if ((move == 0 && eventList.endState == true && endCheck == false) || (move == 0 && advRoomEvent.endEvent == true && endCheck == false))
        {
            if (AdvDay >= advDate) // Ž�� ���� ����
            {
                Debug.Log("All Adventure End");
                endCheck = true;
                advRoomEvent.endEvent = false;

                Status roomStatus = PanelClick.selectedImg.linkCharacter;
                Status charStatus = status.GetComponent<Status>();

                roomStatus.hp = charStatus.hp; // ���� ĳ������ ������ �� ĳ���ͷ� �ű�
                roomStatus.isGoAdventure = false;

                coverSpwaner.deleteCover(); // ���� �� ����

                transform.position = new Vector2(37f, 0.65f); // ĳ���� ��ġ �ʱ�ȭ

                showResult();

                if (charStatus.itemList.Count > 0) // Ž�� �� ������ ������ ����
                {
                    charStatus.itemList.RemoveAt(0);
                }
                area = "";

                PanelClick.selectedImg.border.SetActive(false);
                PanelClick.selectedImg = null;
                PanelClick.selectedID = null;

                SelectMap.selectedImg.border.SetActive(false);
                SelectMap.selectedImg = null;
                SelectMap.selectedMap = null;


                itemSelect.setSprite(); // itemSelect sprite ����
                advRoomEvent.sendItem(); // ������ ����, �ʱ�ȭ
            }
            else
            {
                Debug.Log("Today Adventure End");
                endCheck = true;
                advRoomEvent.endEvent = false;
                showResult();
                advRoomEvent.sendItem();
            }
        }

        moveNum.text = "Move: " + move.ToString();
    }

    public void dead()
    {
        Debug.Log("Dead");
        move = 0;
        endCheck = true;
        advRoomEvent.endEvent = false;

        Status roomStatus = PanelClick.selectedImg.linkCharacter;
        Status charStatus = status.GetComponent<Status>();

        roomStatus.hp = charStatus.hp; // ���� ĳ������ ������ �� ĳ���ͷ� �ű�
        roomStatus.isGoAdventure = false;

        coverSpwaner.deleteCover(); // ���� �� ����

        transform.position = new Vector2(37f, 0.65f); // ĳ���� ��ġ �ʱ�ȭ

        if (charStatus.itemList.Count > 0)
        {
            charStatus.itemList.RemoveAt(0); // Ž�� �� ������ ������ ����
        }

        PanelClick.selectedImg.border.SetActive(false);
        PanelClick.selectedImg = null;
        PanelClick.selectedID = null;

        SelectMap.selectedImg.border.SetActive(false);
        SelectMap.selectedImg = null;
        SelectMap.selectedMap = null;


        itemSelect.setSprite(); // itemSelect sprite ����
        advRoomEvent.deleteItem();
        showDeadResult();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Normal"))
        {
            advRoomEvent.endEvent = false;
            advRoomEvent.getNormal();
        }
        if (other.CompareTag("Empty"))
        {
            advRoomEvent.endEvent = false;
            advRoomEvent.getEmpty();
        }
        if (other.CompareTag("Event"))
        {
            advRoomEvent.endEvent = false;
            advRoomEvent.getEvent();
            moveLock = true;
        }
        move--;
        Destroy(other.gameObject);
        endCheck = false;
    }

    public void showResult() // Ž�� ����� ������ â
    {
        if (advRoomEvent.Food > 0)
        {
            itemList.text += "Food = " + advRoomEvent.Food;
        }
        if (advRoomEvent.Water > 0)
        {
            itemList.text += "\tWater = " + advRoomEvent.Water;
        }
        if (advRoomEvent.Medicine > 0)
        {
            itemList.text += "\tMedicine = " + advRoomEvent.Medicine;
        }
        itemList.text += "\n";

        if (advRoomEvent.Tool > 0)
        {
            itemList.text += "Tool = " + advRoomEvent.Tool;
        }
        if (advRoomEvent.Battery > 0)
        {
            itemList.text += "\tBattery = " + advRoomEvent.Battery;
        }
        if (advRoomEvent.Oxygentank > 0)
        {
            itemList.text += "\tOxygentank = " + advRoomEvent.Oxygentank;
        }
        itemList.text += "\n";

        if (advRoomEvent.Game > 0)
        {
            itemList.text += "Game = " + advRoomEvent.Game;
        }
        if (advRoomEvent.Map > 0)
        {
            itemList.text += "\tMap = " + advRoomEvent.Map;
        }
        if (advRoomEvent.Ax > 0)
        {
            itemList.text += "\tAx = " + advRoomEvent.Ax;
        }
        itemList.text += "\n";

        if (advRoomEvent.Light > 0)
        {
            itemList.text += "\tLight = " + advRoomEvent.Light;
        }
        if (advRoomEvent.Research > 0)
        {
            itemList.text += "\tResearch = " + advRoomEvent.Research;
        }
        itemList.text += "\n";

        if (advRoomEvent.Diver > 0)
        {
            itemList.text += "Diver = " + advRoomEvent.Diver;
        }

        advEndPanel.SetActive(true);
    }

    public void showDeadResult() // Ž�� ����� ������ â
    {
        itemList.text = "ĳ���Ͱ� ����Ͽ����ϴ�.";
        advEndPanel.SetActive(true);
    }
}
