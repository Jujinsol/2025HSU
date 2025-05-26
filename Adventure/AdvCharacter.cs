using TMPro;
using UnityEngine;

//모험 캐릭터 이동
public class AdvCharacter : MonoBehaviour
{
    public int move; // 하루에 움직일 수 있는 횟수
    public bool moveLock = false; // 이벤트 중 움직임 잠금
    public string area; // 모험할 구역 -> 이벤트, 아이템 획득 상태가 달라짐
    public bool endCheck = false; // 종료 체크
    public int AdvDay; // 모험 진행 일자
    public int advDate; // 모험 가능 일자 기본 3일 특성 2일

    public GameObject GM;
    AdvRoomEvent advRoomEvent; // 방에서 일어나는 일들
    public Status status; // 캐릭터의 스탯

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
        Vector2 pos = transform.position; //오브젝트의 위치 복사
        if (move != 0 && moveLock == false)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // 오브젝트의 위치에서 좌표 +1
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
            if (AdvDay >= advDate) // 탐험 완전 종료
            {
                Debug.Log("All Adventure End");
                endCheck = true;
                advRoomEvent.endEvent = false;

                Status roomStatus = PanelClick.selectedImg.linkCharacter;
                Status charStatus = status.GetComponent<Status>();

                roomStatus.hp = charStatus.hp; // 모험 캐릭터의 스탯을 방 캐릭터로 옮김
                roomStatus.isGoAdventure = false;

                coverSpwaner.deleteCover(); // 남은 방 삭제

                transform.position = new Vector2(37f, 0.65f); // 캐릭터 위치 초기화

                showResult();

                if (charStatus.itemList.Count > 0) // 탐험 시 가져온 아이템 제거
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


                itemSelect.setSprite(); // itemSelect sprite 갱신
                advRoomEvent.sendItem(); // 아이템 전달, 초기화
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

        roomStatus.hp = charStatus.hp; // 모험 캐릭터의 스탯을 방 캐릭터로 옮김
        roomStatus.isGoAdventure = false;

        coverSpwaner.deleteCover(); // 남은 방 삭제

        transform.position = new Vector2(37f, 0.65f); // 캐릭터 위치 초기화

        if (charStatus.itemList.Count > 0)
        {
            charStatus.itemList.RemoveAt(0); // 탐험 시 가져온 아이템 제거
        }

        PanelClick.selectedImg.border.SetActive(false);
        PanelClick.selectedImg = null;
        PanelClick.selectedID = null;

        SelectMap.selectedImg.border.SetActive(false);
        SelectMap.selectedImg = null;
        SelectMap.selectedMap = null;


        itemSelect.setSprite(); // itemSelect sprite 갱신
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

    public void showResult() // 탐험 종료시 보여줄 창
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

    public void showDeadResult() // 탐험 종료시 보여줄 창
    {
        itemList.text = "캐릭터가 사망하였습니다.";
        advEndPanel.SetActive(true);
    }
}
