using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

// 이벤트 방 이벤트 리스트
public class EventList : MonoBehaviour
{
    int randInt = 0;

    public GameObject Character;
    AdvCharacter AdvCharacter;
    AdvRoomEvent advRoomEvent;

    public GameObject panel;
    public TextMeshProUGUI eventText;

    public Button NextBtn; // 이벤트 텍스트 넘어가기 기능
    public Button YesBtn;
    public Button NoBtn; // 이벤트 진행 여부 선택 버튼

    string eventType;// 어떤 이벤트인지 저장 ex)수리 상자 등

    public GameObject PW; // 비밀번호 이벤트 오브젝트
    public PWMission PWMission;

    public GameObject PWBox; // 비밀번호박스 이벤트 오브젝트
    public SequenceMission PWBoxMission;

    public GameObject ColorRoom; // 그림 패턴 이벤트 오브젝트
    public ColorManager colorMission;

    public GameObject temperCheck; // 온도 조절 이벤트 오브젝트
    public TemperMission temperMission;

    public GameObject oilTank; // 기름 채우기 이벤트 오브젝트
    public OilManager oilMission;

    // 공통 이벤트
    string[] VendingMachineEvent = new string[]
    {
        "과자 자판기가 보인다.",
        "안에 과자가 좀 남아있는거 같은데...",
        "열어볼까?"
    };
    string[] BoxEvent = new string[]
    {
        "박스를 발견했다.",
        "열려있는것 같다.",
        "확인해볼까?"
    };
    string[] SoundEvent = new string[]
    {
        "어디선가 소리가 들린다.",
        "도움을 요청하는 것같다.",
        "소리를 따라가볼까?"
    };

    // A구역 이벤트
    string[] BedRoomEvent = new string[]
    {
        "열려있는 문이 보인다.",
        "방안이 깜깜해서 아무것도 보이지 않는다.",
        "불을 켜볼까?"
    };
    string[] MedicineEvent = new string[]
    {
        "의무실 한쪽에 약인 보인다.",
        "무슨 약인지는 글자가 지워져있다.",
        "사용해볼까?"
    };
    string[] InfirmaryEvent = new string[]
    {
        "의무실에 들어왔다.",
        "깨진 주사기 사이에 멀쩡한 약품이 보인다.",
        "꺼내볼까?"
    };
    string[] ShowerRoomEvent = new string[]
    {
        "샤워실이다.",
        "배수구에 수돗물이 남아있지 않을까?",
        "확인해볼까?"
    };
    string[] ShowerRockerEvent = new string[]
    {
        "샤워실 락커룸이다.",
        "열려있는 락커가 있는지 확인해볼까?",
    };
    string[] PWBoxEvent = new string[]
    {
        "박스를 발견했다.",
        "잠겨있는것 같다.",
        "주변에는 순서대로라는 메모가 있다.",
        "비밀번호를 눌러볼까?"
    };

    // B구역 이벤트
    string[] ResearchRoomEvent = new string[]
    {
        "실험실에 들어왔다.",
        "실험중이던 장비가 보인다.",
        "가져갈까?"
    };
    string[] GasRoomEvent = new string[]
    {
        "가스가 차있는 방이 보인다.",
        "유리창으로 보니 안에 총이 보인다.",
        "들어갈까?"
    };
    string[] ToiletEvent = new string[]
    {
        "화장실이다.",
        "변기 안에 물이 남아있는것같다.",
        "물을 가져갈까?"
    };
    string[] RobbyEvent = new string[]
    {
        "로비에 지도가 붙여져있다.",
        "뜯어갈까?",
    };
    string[] PWEvent = new string[]
    {
        "실험실 한켠에 문이 보인다.",
        "비밀번호로 잠겨있다.",
        "주변에 힌트가 있을것 같은데....",
    };
    string[] ColorEvent = new string[]
    {
        "경비원의 방이 보인다.",
        "그림 패턴으로 잠겨있다.",
        "똑같이 만들면 되는것 같다.",
    };

    // C구역 이벤트
    string[] PowerRoomEvent = new string[]
    {
        "발전실이 보인다.",
        "전기가 누출되지 않았을까...",
        "한번 살펴볼까?"
    };
    string[] WareHouseEvent = new string[]
    {
        "창고에 들어갔다.",
        "안이 매우 복잡해 보인다.",
        "창고를 둘러볼까?"
    };
    string[] RestaurantEvent = new string[]
    {
        "식당이다.",
        "먹을게 남아있는지 찾아볼까?"
    };
    string[] OilEvent = new string[]
    {
        "세척기 안에 무언가 보인다.",
        "기름이 없어 작동을 멈춘 것 같다.",
        "주변에 기름이 보인다.",
        "기름을 채워 넣을까?"
    };
    string[] TemperEvent = new string[]
    {
        "방 온도가 비정상이다.",
        "온도 때문에 기계가 멈춘듯하다",
        "온도를 조절할까?"
    };

    int index;// 현재 이벤트 텍스트의 인덱스
    public bool eventState; // false = 이벤트 진행중, true = 이벤트 끝
    public bool endState;
    void Start()
    {
        AdvCharacter = Character.GetComponent<AdvCharacter>(); // 구역을 활용하기 위해 연결

        NextBtn.onClick.AddListener(NextBtnClicked);
        YesBtn.onClick.AddListener(YesBtnClicked);
        NoBtn.onClick.AddListener(NoBtnClicked);

        advRoomEvent = AdvCharacter.GM.GetComponent<AdvRoomEvent>(); // 인벤토리와 연결
    }
    public void NextBtnClicked()
    {
        // 공통
        if (eventType == "vendingMachine") // 태그에 따른 이벤트 진행
        {
            if (index < VendingMachineEvent.Length)
            {
                eventText.text += VendingMachineEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "box")
        {
            if (index < BoxEvent.Length)
            {
                eventText.text += BoxEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "sound")
        {
            if (index < SoundEvent.Length)
            {
                eventText.text += SoundEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }

        // A구역
        if (eventType == "BedRoom")
        {
            if (index < BedRoomEvent.Length)
            {
                eventText.text += BedRoomEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Medicine")
        {
            if (index < MedicineEvent.Length)
            {
                eventText.text += MedicineEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Infirmary")
        {
            if (index < InfirmaryEvent.Length)
            {
                eventText.text += InfirmaryEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Shower")
        {
            if (index < ShowerRoomEvent.Length)
            {
                eventText.text += ShowerRoomEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Rocker")
        {
            if (index < ShowerRockerEvent.Length)
            {
                eventText.text += ShowerRockerEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "PWBox")
        {
            if (index < PWBoxEvent.Length)
            {
                eventText.text += PWBoxEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }

        // 구역B
        if (eventType == "Research") // 태그에 따른 이벤트 진행
        {
            if (index < ResearchRoomEvent.Length)
            {
                eventText.text += ResearchRoomEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Gas") // 태그에 따른 이벤트 진행
        {
            if (index < GasRoomEvent.Length)
            {
                eventText.text += GasRoomEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Toilet") // 태그에 따른 이벤트 진행
        {
            if (index < ToiletEvent.Length)
            {
                eventText.text += ToiletEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Robby") // 태그에 따른 이벤트 진행
        {
            if (index < RobbyEvent.Length)
            {
                eventText.text += RobbyEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "PW")
        {
            if (index < PWEvent.Length)
            {
                eventText.text += PWEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Color")
        {
            if (index < ColorEvent.Length)
            {
                eventText.text += ColorEvent[index] + "\n";
                index++;
            }
            else
            {
                NextBtn.gameObject.SetActive(false);

                if (eventState == false)
                {
                    YesBtn.gameObject.SetActive(true);
                    NoBtn.gameObject.SetActive(true);
                }
                else
                {
                    eventText.gameObject.SetActive(false);
                    PW.SetActive(false);
                    AdvCharacter.moveLock = false;
                    endState = true;
                }
            }
        }

        // 구역C
        if (eventType == "PowerRoom")
        {
            if (index < PowerRoomEvent.Length)
            {
                eventText.text += PowerRoomEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Warehouse")
        {
            if (index < WareHouseEvent.Length)
            {
                eventText.text += WareHouseEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Restaurant")
        {
            if (index < RestaurantEvent.Length)
            {
                eventText.text += RestaurantEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Oil")
        {
            if (index < OilEvent.Length)
            {
                eventText.text += OilEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }
        if (eventType == "Temper")
        {
            if (index < TemperEvent.Length)
            {
                eventText.text += TemperEvent[index] + "\n";
                index++;
            }
            else
            {
                CommonPart();
            }
        }

        if (AdvCharacter.status.hp <= 0) // 이벤트 종료 후 사망 체크
        {
            AdvCharacter.dead();
        }
    }

    public void CommonPart()
    {
        NextBtn.gameObject.SetActive(false);

        if (eventState == false)
        {
            YesBtn.gameObject.SetActive(true);
            NoBtn.gameObject.SetActive(true);
        }
        else
        {
            eventText.gameObject.SetActive(false);
            AdvCharacter.moveLock = false;
            endState = true;
        }
    }

    public void YesBtnClicked()
    {
        // 전 구역 공통
        if (eventType == "vendingMachine")
        {
            randInt = Random.Range(0, 100);

            int success = AdvCharacter.status.mental; // 성공확률
            if (AdvCharacter.status.Job == "Mechanic")
            {
                success += 10;
            }
            if (AdvCharacter.status.itemList[0] == "Tool")
            {
                success += 10;
            }

            if (randInt <= success) // mental에 따른 이벤트 성공확률
            {
                eventText.text = "자판기를 열어 과자를 얻었다.\n음식 1획득";
                advRoomEvent.Food++;
            }
            else
            {
                eventText.text = "자판기를 열다가 손을 다쳤다...\n체력 10감소";
                AdvCharacter.status.hp -= 10;
            }
            endNormalEvent();
        }
        if (eventType == "box")
        {
            randInt = Random.Range(0, 3);
            if (randInt == 0)
            {
                eventText.text = "상자 안에 음식이 있다.\n음식 1획득";
                advRoomEvent.Food++;
            }
            else if (randInt == 1)
            {
                eventText.text = "상자 안에 물이 있다.\n물 1획득";
                advRoomEvent.Water++;
            }
            else
            {
                eventText.text = "아무것도 없다...";
            }
            endNormalEvent();
        }
        if (eventType == "sound")
        {
            randInt = Random.Range(0, 11);
            if (randInt <= 4)
            {
                eventText.text = "산소통이 뒹굴고 있다.\n상태가 괜찮은것같다.\n산소통 1획득";
                advRoomEvent.Oxygentank++;
            }
            else if (randInt > 4 && randInt <= 9)
            {
                eventText.text = "깨진 Tv와 리모콘이 있다.\n리모콘에 건전지가 들어있다.\n배터리 1획득";
                advRoomEvent.Battery++;
            }
            else if (randInt == 10)
            {
                eventText.text = "다른 생존자들이다.\n그들이 나를 발견하고 달려들었다.";
                AdvCharacter.status.hp -= 1000;
            }
            endNormalEvent();
        }

        // 구역A
        if (eventType == "BedRoom")
        {
            if (AdvCharacter.status.Job == "Mechanic")
            {
                int randInt = Random.Range(0, 2);
                if (randInt == 0)
                {
                    eventText.text = "스위치가 위험해 보인다.\n스위치를 수리하고 눌렀다.\n책상 위에 보드게임이 있다.\n보드게임 1획득";
                    advRoomEvent.Game++;
                }
                else
                {
                    eventText.text = "스위치가 위험해 보인다.\n스위치를 수리하고 눌렀다.\n책상 위에 손전등이 있다.\n손전등 1획득";
                    advRoomEvent.Light++;
                }
            }
            else
            {
                AdvCharacter.status.hp -= 10; //체력감소
                eventText.text = "감전당했다!!\n체력 10감소";
            }
            endNormalEvent();
        }
        if (eventType == "Medicine")
        {
            randInt = Random.Range(0, 100);
            if (randInt <= 50)
            {
                AdvCharacter.status.hp += 20;
                eventText.text = "건강해지는 느낌이다.\n체력 20증가";

            }
            else
            {
                AdvCharacter.status.hp -= 20;
                eventText.text = "몸이 무거워진다....\n체력 20감소";
            }
            endNormalEvent();
        }
        if (eventType == "Infirmary")
        {
            randInt = Random.Range(0, 100);
            if (randInt <= AdvCharacter.status.mental)
            {
                eventText.text = "파편에 찔리지 않고 약을 꺼냈다.\n약 1획득";
                advRoomEvent.Medicine++;
            }
            else
            {
                eventText.text = "약을 꺼냈지만,\n파편에 손을 찔려서 피가 난다...\n체력 20감소\n약1 획득";
                AdvCharacter.status.hp -= 20;
                advRoomEvent.Medicine++;
            }
            endNormalEvent();
        }
        if (eventType == "Shower")
        {
            if (advRoomEvent.Water > 0)
            {
                advRoomEvent.Water--;
            }
            eventText.text = "바닷물이 나왔다.\n물병의 물을 버렸다.\n물 1감소";
            endNormalEvent();
        }
        if (eventType == "Rocker")
        {
            randInt = Random.Range(0, 100);
            if (randInt <= AdvCharacter.status.mental)
            {
                eventText.text = "락커 안에서 잠수복을 발견했다.\n잠수복 1획득";
            }
            else
            {
                eventText.text = "락커를 열어봤지만\n아무것도 찾지 못했다.";
            }
            endNormalEvent();
        }
        if (eventType == "PWBox")
        {
            PWBox.SetActive(true);
            PWBoxMission.end = false;
            StartCoroutine(WaitPWBox());
        }

        // 구역 B
        if (eventType == "Research")
        {
            int randint = Random.Range(0, 100);
            int success = AdvCharacter.status.mental;
            if (AdvCharacter.status.Job == "Researcher")
            {
                success += 10;
            }
            if (randint <= success)
            {
                eventText.text = "정상적으로 작동하는 산소통을 획득했다.\n통신기 1획득";
                advRoomEvent.Oxygentank++;
            }
            else
            {
                eventText.text = "장비가 고장나 있다.\n아무것도 얻지 못했다.";
            }
            endNormalEvent();
        }
        if (eventType == "Toilet")
        {
            advRoomEvent.Water++;
            AdvCharacter.status.mental -= 10;
            eventText.text = "마실 수 있는 물 같다.\n하지만 뭔가 찝찝하다...\n물 1획득\n 멘탈 10감소";
            endNormalEvent();
        }
        if (eventType == "Robby")
        {
            int randInt = Random.Range(0, 100);
            int success = AdvCharacter.status.mental;
            if (randInt <= success)
            {
                eventText.text = "지도를 깨끗하게 뜯었다.\n지도 1획득";
                advRoomEvent.Map++;
            }
            else
            {
                eventText.text = "지도가 찢어졌다....";
            }
            endNormalEvent();
        }
        if (eventType == "PW")
        {
            PW.SetActive(true);
            PWMission.MakePW();
            StartCoroutine(WaitPW());
        }
        if (eventType == "Color")
        {
            ColorRoom.SetActive(true);
            colorMission.MakePattern();
            StartCoroutine(WaitPattern());
        }

        // 구역 C
        if (eventType == "PowerRoom")
        {
            advRoomEvent.Battery++;
            eventText.text = "배터리를 발견했다.\n배터리 1획득";
            endNormalEvent();
        }
        if (eventType == "Warehouse")
        {
            randInt = Random.Range(0, 100);
            int success = AdvCharacter.status.mental;
            if (AdvCharacter.status.Job == "Guard")
            {
                success += 10;
            }
            if (randInt <= success) // mental에 따른 이벤트 성공확률
            {
                int randInt2 = Random.Range(0, 2);
                if (randInt2 == 0)
                {
                    eventText.text = "도끼를 발견했다\n도끼 1획득";
                    advRoomEvent.Ax++;
                }
                else
                {
                    eventText.text = "공구를 발견했다\n도구 1획득";
                    advRoomEvent.Tool++;
                }
            }
            else
            {
                eventText.text = "복잡해서 아무것도 찾지 못했다.";
            }
            endNormalEvent();
        }
        if (eventType == "Restaurant")
        {
            int randint = Random.Range(0, 4);
            if (randint == 0)
            {
                advRoomEvent.Food += 2;
                eventText.text = "음식을 발견했다.\n음식 2획득";
            }
            else if (randint == 1)
            {
                advRoomEvent.Water += 2;
                eventText.text = "물을 발견했다.\n물 2획득";
            }
            else if (randint == 2)
            {
                advRoomEvent.Food++;
                advRoomEvent.Water++;
                eventText.text = "음식과 물을 발견했다.\n음식, 물 1획득";
            }
            else
            {
                eventText.text = "아무것도 찾지 못했다.";
            }
            endNormalEvent();
        }
        if (eventType == "Oil")
        {
            oilTank.SetActive(true);
            oilMission.setOil();
            StartCoroutine(WaitOil());
        }
        if (eventType == "Temper")
        {
            temperCheck.SetActive(true);
            temperMission.makeTemper();
            StartCoroutine(WaitTemper());
        }
    }

    public void NoBtnClicked()
    {
        // 공통
        if (eventType == "vendingMachine")
        {
            endEvent();
        }
        if (eventType == "box")
        {
            endEvent();
        }
        if (eventType == "sound")
        {
            endEvent();
        }

        // 구역A
        if (eventType == "Shower" || eventType == "BedRoom" || eventType == "Medicine" || eventType == "Infirmary" || eventType == "Rocker")
        {
            endEvent();
        }

        // 구역B
        if (eventType == "Research" || eventType == "Gas" || eventType == "Toilet" || eventType == "Robby")
        {
            endEvent();
        }

        // 구역C
        if (eventType == "PowerRoom" || eventType == "Warehouse" || eventType == "Restaurant")
        {
            endEvent();
        }

        // 미니게임
        if (eventType == "PW" || eventType == "PWBox" || eventType == "Color" || eventType == "Oil" || eventType == "Temper")
        {
            endEvent();
        }
    }

    public void callEvent() // 확률에 따라 이벤트 발생
    {
        eventState = false;
        endState = false;

        index = 0; // 인덱스 초기화
        eventText.text = ""; // 텍스트 초기화

        Debug.Log("이벤트 리스트 정상");
        randInt = Random.Range(0, 100);

        if (AdvCharacter.area == "A")
        {
            if (randInt >= 0 && randInt < 1)
            {
                eventType = "vendingMachine";
            }
            else if (randInt >= 1 && randInt < 2)
            {
                eventType = "box";
            }
            else if (randInt >= 2 && randInt < 3)
            {
                eventType = "sound";
            }
            else if (randInt >= 3 && randInt < 4)
            {
                eventType = "BedRoom";
            }
            else if (randInt >= 4 && randInt < 5)
            {
                eventType = "Medicine";
            }
            else if (randInt >= 5 && randInt < 6)
            {
                eventType = "Infirmary";
            }
            else if (randInt >= 6 && randInt < 7)
            {
                eventType = "Shower";
            }
            else if (randInt >= 7 && randInt < 8)
            {
                eventType = "Rocker";
            }
            else if (randInt >= 8 && randInt < 100)
            {
                eventType = "PWBox";
            }
        }
        else if (AdvCharacter.area == "B")
        {
            if (randInt >= 0 && randInt < 1)
            {
                eventType = "vendingMachine";
            }
            else if (randInt >= 1 && randInt < 2)
            {
                eventType = "box";
            }
            else if (randInt >= 2 && randInt < 3)
            {
                eventType = "sound";
            }
            else if (randInt >= 3 && randInt < 4)
            {
                eventType = "Research";
            }
            else if (randInt >= 4 && randInt < 5)
            {
                eventType = "Gas";
            }
            else if (randInt >= 5 && randInt < 6)
            {
                eventType = "Toilet";
            }
            else if (randInt >= 6 && randInt < 7)
            {
                eventType = "Robby";
            }
            else if (randInt >= 7 && randInt < 8)
            {
                eventType = "PW";
            }
            else if (randInt >= 8 && randInt < 100)
            {
                eventType = "Color";
            }
        }
        else if (AdvCharacter.area == "C")
        {
            if (randInt >= 0 && randInt < 1)
            {
                eventType = "vendingMachine";
            }
            else if (randInt >= 1 && randInt < 2)
            {
                eventType = "box";
            }
            else if (randInt >= 2 && randInt < 3)
            {
                eventType = "sound";
            }
            else if (randInt >= 3 && randInt < 4)
            {
                eventType = "PowerRoom";
            }
            else if (randInt >= 4 && randInt < 5)
            {
                eventType = "Warehouse";
            }
            else if (randInt >= 5 && randInt < 6)
            {
                eventType = "Restaurant";
            }
            else if (randInt >= 6 && randInt < 7)
            {
                eventType = "Oil";
            }
            else if (randInt >= 7 && randInt < 100)
            {
                eventType = "Temper";
            }
        }
        NextBtn.gameObject.SetActive(true); // 이벤트 시작 시 next 버튼과 text창이 보임
        eventText.gameObject.SetActive(true);
    }

    public void endEvent()
    {
        eventText.text = "아무일도 일어나지 않았다.";
        YesBtn.gameObject.SetActive(false);
        NoBtn.gameObject.SetActive(false);

        NextBtn.gameObject.SetActive(true);
        eventState = true;
    }
    public void endNormalEvent()
    {
        YesBtn.gameObject.SetActive(false);
        NoBtn.gameObject.SetActive(false);

        NextBtn.gameObject.SetActive(true);
        eventState = true;
    }

    private IEnumerator WaitPW() // 미션이 끝날때까지 기다림
    {
        yield return new WaitUntil(() => PWMission.end == true);
        advRoomEvent.Research++;
        eventText.text = "잠김문을 열어보니 연구자료가 들어있다.\n연구자료 1 획득.";
        PW.SetActive(false);
        endNormalEvent();
    }

    private IEnumerator WaitPWBox()
    {
        yield return new WaitUntil(() => PWBoxMission.end == true);
        advRoomEvent.Food += 2;
        advRoomEvent.Water += 2;
        eventText.text = "상자안에 비상식량이 들어있다.\n식량, 물 2 획득.";
        PWBox.SetActive(false);
        endNormalEvent();
    }

    private IEnumerator WaitPattern()
    {
        yield return new WaitUntil(() => colorMission.end == true);
        advRoomEvent.Oxygentank++;
        eventText.text = "경비실 안에 산소통이 있다.\n산소통 1획득";
        ColorRoom.SetActive(false);
        endNormalEvent();
    }

    private IEnumerator WaitOil()
    {
        yield return new WaitUntil(() => oilMission.isFull == true);
        advRoomEvent.Food++;
        eventText.text = "세척기가 작동한다.\n밸트를 따라 통조림이 나왔다.\n음식 1획득";
        oilTank.SetActive(false);
        endNormalEvent();
    }
    private IEnumerator WaitTemper()
    {
        yield return new WaitUntil(() => temperMission.end == true);
        advRoomEvent.Battery += 2;
        eventText.text = "온도를 조절하니 기계가 작동한다.\n기계에서 배터리가 나왔다\n배터리 2획득";
        temperCheck.SetActive(false);
        endNormalEvent();
    }
}
