using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

// �̺�Ʈ �� �̺�Ʈ ����Ʈ
public class EventList : MonoBehaviour
{
    int randInt = 0;

    public GameObject Character;
    AdvCharacter AdvCharacter;
    AdvRoomEvent advRoomEvent;

    public GameObject panel;
    public TextMeshProUGUI eventText;

    public Button NextBtn; // �̺�Ʈ �ؽ�Ʈ �Ѿ�� ���
    public Button YesBtn;
    public Button NoBtn; // �̺�Ʈ ���� ���� ���� ��ư

    string eventType;// � �̺�Ʈ���� ���� ex)���� ���� ��

    public GameObject PW; // ��й�ȣ �̺�Ʈ ������Ʈ
    public PWMission PWMission;

    public GameObject PWBox; // ��й�ȣ�ڽ� �̺�Ʈ ������Ʈ
    public SequenceMission PWBoxMission;

    public GameObject ColorRoom; // �׸� ���� �̺�Ʈ ������Ʈ
    public ColorManager colorMission;

    public GameObject temperCheck; // �µ� ���� �̺�Ʈ ������Ʈ
    public TemperMission temperMission;

    public GameObject oilTank; // �⸧ ä��� �̺�Ʈ ������Ʈ
    public OilManager oilMission;

    // ���� �̺�Ʈ
    string[] VendingMachineEvent = new string[]
    {
        "���� ���ǱⰡ ���δ�.",
        "�ȿ� ���ڰ� �� �����ִ°� ������...",
        "�����?"
    };
    string[] BoxEvent = new string[]
    {
        "�ڽ��� �߰��ߴ�.",
        "�����ִ°� ����.",
        "Ȯ���غ���?"
    };
    string[] SoundEvent = new string[]
    {
        "��𼱰� �Ҹ��� �鸰��.",
        "������ ��û�ϴ� �Ͱ���.",
        "�Ҹ��� ���󰡺���?"
    };

    // A���� �̺�Ʈ
    string[] BedRoomEvent = new string[]
    {
        "�����ִ� ���� ���δ�.",
        "����� �����ؼ� �ƹ��͵� ������ �ʴ´�.",
        "���� �Ѻ���?"
    };
    string[] MedicineEvent = new string[]
    {
        "�ǹ��� ���ʿ� ���� ���δ�.",
        "���� �������� ���ڰ� �������ִ�.",
        "����غ���?"
    };
    string[] InfirmaryEvent = new string[]
    {
        "�ǹ��ǿ� ���Դ�.",
        "���� �ֻ�� ���̿� ������ ��ǰ�� ���δ�.",
        "��������?"
    };
    string[] ShowerRoomEvent = new string[]
    {
        "�������̴�.",
        "������� �������� �������� ������?",
        "Ȯ���غ���?"
    };
    string[] ShowerRockerEvent = new string[]
    {
        "������ ��Ŀ���̴�.",
        "�����ִ� ��Ŀ�� �ִ��� Ȯ���غ���?",
    };
    string[] PWBoxEvent = new string[]
    {
        "�ڽ��� �߰��ߴ�.",
        "����ִ°� ����.",
        "�ֺ����� ������ζ�� �޸� �ִ�.",
        "��й�ȣ�� ��������?"
    };

    // B���� �̺�Ʈ
    string[] ResearchRoomEvent = new string[]
    {
        "����ǿ� ���Դ�.",
        "�������̴� ��� ���δ�.",
        "��������?"
    };
    string[] GasRoomEvent = new string[]
    {
        "������ ���ִ� ���� ���δ�.",
        "����â���� ���� �ȿ� ���� ���δ�.",
        "����?"
    };
    string[] ToiletEvent = new string[]
    {
        "ȭ����̴�.",
        "���� �ȿ� ���� �����ִ°Ͱ���.",
        "���� ��������?"
    };
    string[] RobbyEvent = new string[]
    {
        "�κ� ������ �ٿ����ִ�.",
        "����?",
    };
    string[] PWEvent = new string[]
    {
        "����� ���ҿ� ���� ���δ�.",
        "��й�ȣ�� ����ִ�.",
        "�ֺ��� ��Ʈ�� ������ ������....",
    };
    string[] ColorEvent = new string[]
    {
        "������ ���� ���δ�.",
        "�׸� �������� ����ִ�.",
        "�Ȱ��� ����� �Ǵ°� ����.",
    };

    // C���� �̺�Ʈ
    string[] PowerRoomEvent = new string[]
    {
        "�������� ���δ�.",
        "���Ⱑ ������� �ʾ�����...",
        "�ѹ� ���캼��?"
    };
    string[] WareHouseEvent = new string[]
    {
        "â�� ����.",
        "���� �ſ� ������ ���δ�.",
        "â�� �ѷ�����?"
    };
    string[] RestaurantEvent = new string[]
    {
        "�Ĵ��̴�.",
        "������ �����ִ��� ã�ƺ���?"
    };
    string[] OilEvent = new string[]
    {
        "��ô�� �ȿ� ���� ���δ�.",
        "�⸧�� ���� �۵��� ���� �� ����.",
        "�ֺ��� �⸧�� ���δ�.",
        "�⸧�� ä�� ������?"
    };
    string[] TemperEvent = new string[]
    {
        "�� �µ��� �������̴�.",
        "�µ� ������ ��谡 ������ϴ�",
        "�µ��� �����ұ�?"
    };

    int index;// ���� �̺�Ʈ �ؽ�Ʈ�� �ε���
    public bool eventState; // false = �̺�Ʈ ������, true = �̺�Ʈ ��
    public bool endState;
    void Start()
    {
        AdvCharacter = Character.GetComponent<AdvCharacter>(); // ������ Ȱ���ϱ� ���� ����

        NextBtn.onClick.AddListener(NextBtnClicked);
        YesBtn.onClick.AddListener(YesBtnClicked);
        NoBtn.onClick.AddListener(NoBtnClicked);

        advRoomEvent = AdvCharacter.GM.GetComponent<AdvRoomEvent>(); // �κ��丮�� ����
    }
    public void NextBtnClicked()
    {
        // ����
        if (eventType == "vendingMachine") // �±׿� ���� �̺�Ʈ ����
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

        // A����
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

        // ����B
        if (eventType == "Research") // �±׿� ���� �̺�Ʈ ����
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
        if (eventType == "Gas") // �±׿� ���� �̺�Ʈ ����
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
        if (eventType == "Toilet") // �±׿� ���� �̺�Ʈ ����
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
        if (eventType == "Robby") // �±׿� ���� �̺�Ʈ ����
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

        // ����C
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

        if (AdvCharacter.status.hp <= 0) // �̺�Ʈ ���� �� ��� üũ
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
        // �� ���� ����
        if (eventType == "vendingMachine")
        {
            randInt = Random.Range(0, 100);

            int success = AdvCharacter.status.mental; // ����Ȯ��
            if (AdvCharacter.status.Job == "Mechanic")
            {
                success += 10;
            }
            if (AdvCharacter.status.itemList[0] == "Tool")
            {
                success += 10;
            }

            if (randInt <= success) // mental�� ���� �̺�Ʈ ����Ȯ��
            {
                eventText.text = "���Ǳ⸦ ���� ���ڸ� �����.\n���� 1ȹ��";
                advRoomEvent.Food++;
            }
            else
            {
                eventText.text = "���Ǳ⸦ ���ٰ� ���� ���ƴ�...\nü�� 10����";
                AdvCharacter.status.hp -= 10;
            }
            endNormalEvent();
        }
        if (eventType == "box")
        {
            randInt = Random.Range(0, 3);
            if (randInt == 0)
            {
                eventText.text = "���� �ȿ� ������ �ִ�.\n���� 1ȹ��";
                advRoomEvent.Food++;
            }
            else if (randInt == 1)
            {
                eventText.text = "���� �ȿ� ���� �ִ�.\n�� 1ȹ��";
                advRoomEvent.Water++;
            }
            else
            {
                eventText.text = "�ƹ��͵� ����...";
            }
            endNormalEvent();
        }
        if (eventType == "sound")
        {
            randInt = Random.Range(0, 11);
            if (randInt <= 4)
            {
                eventText.text = "������� �߱��� �ִ�.\n���°� �������Ͱ���.\n����� 1ȹ��";
                advRoomEvent.Oxygentank++;
            }
            else if (randInt > 4 && randInt <= 9)
            {
                eventText.text = "���� Tv�� �������� �ִ�.\n�����ܿ� �������� ����ִ�.\n���͸� 1ȹ��";
                advRoomEvent.Battery++;
            }
            else if (randInt == 10)
            {
                eventText.text = "�ٸ� �����ڵ��̴�.\n�׵��� ���� �߰��ϰ� �޷������.";
                AdvCharacter.status.hp -= 1000;
            }
            endNormalEvent();
        }

        // ����A
        if (eventType == "BedRoom")
        {
            if (AdvCharacter.status.Job == "Mechanic")
            {
                int randInt = Random.Range(0, 2);
                if (randInt == 0)
                {
                    eventText.text = "����ġ�� ������ ���δ�.\n����ġ�� �����ϰ� ������.\nå�� ���� ��������� �ִ�.\n������� 1ȹ��";
                    advRoomEvent.Game++;
                }
                else
                {
                    eventText.text = "����ġ�� ������ ���δ�.\n����ġ�� �����ϰ� ������.\nå�� ���� �������� �ִ�.\n������ 1ȹ��";
                    advRoomEvent.Light++;
                }
            }
            else
            {
                AdvCharacter.status.hp -= 10; //ü�°���
                eventText.text = "�������ߴ�!!\nü�� 10����";
            }
            endNormalEvent();
        }
        if (eventType == "Medicine")
        {
            randInt = Random.Range(0, 100);
            if (randInt <= 50)
            {
                AdvCharacter.status.hp += 20;
                eventText.text = "�ǰ������� �����̴�.\nü�� 20����";

            }
            else
            {
                AdvCharacter.status.hp -= 20;
                eventText.text = "���� ���ſ�����....\nü�� 20����";
            }
            endNormalEvent();
        }
        if (eventType == "Infirmary")
        {
            randInt = Random.Range(0, 100);
            if (randInt <= AdvCharacter.status.mental)
            {
                eventText.text = "���� ���� �ʰ� ���� ���´�.\n�� 1ȹ��";
                advRoomEvent.Medicine++;
            }
            else
            {
                eventText.text = "���� ��������,\n���� ���� ����� �ǰ� ����...\nü�� 20����\n��1 ȹ��";
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
            eventText.text = "�ٴ幰�� ���Դ�.\n������ ���� ���ȴ�.\n�� 1����";
            endNormalEvent();
        }
        if (eventType == "Rocker")
        {
            randInt = Random.Range(0, 100);
            if (randInt <= AdvCharacter.status.mental)
            {
                eventText.text = "��Ŀ �ȿ��� ������� �߰��ߴ�.\n����� 1ȹ��";
            }
            else
            {
                eventText.text = "��Ŀ�� ���������\n�ƹ��͵� ã�� ���ߴ�.";
            }
            endNormalEvent();
        }
        if (eventType == "PWBox")
        {
            PWBox.SetActive(true);
            PWBoxMission.end = false;
            StartCoroutine(WaitPWBox());
        }

        // ���� B
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
                eventText.text = "���������� �۵��ϴ� ������� ȹ���ߴ�.\n��ű� 1ȹ��";
                advRoomEvent.Oxygentank++;
            }
            else
            {
                eventText.text = "��� ���峪 �ִ�.\n�ƹ��͵� ���� ���ߴ�.";
            }
            endNormalEvent();
        }
        if (eventType == "Toilet")
        {
            advRoomEvent.Water++;
            AdvCharacter.status.mental -= 10;
            eventText.text = "���� �� �ִ� �� ����.\n������ ���� �����ϴ�...\n�� 1ȹ��\n ��Ż 10����";
            endNormalEvent();
        }
        if (eventType == "Robby")
        {
            int randInt = Random.Range(0, 100);
            int success = AdvCharacter.status.mental;
            if (randInt <= success)
            {
                eventText.text = "������ �����ϰ� �����.\n���� 1ȹ��";
                advRoomEvent.Map++;
            }
            else
            {
                eventText.text = "������ ��������....";
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

        // ���� C
        if (eventType == "PowerRoom")
        {
            advRoomEvent.Battery++;
            eventText.text = "���͸��� �߰��ߴ�.\n���͸� 1ȹ��";
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
            if (randInt <= success) // mental�� ���� �̺�Ʈ ����Ȯ��
            {
                int randInt2 = Random.Range(0, 2);
                if (randInt2 == 0)
                {
                    eventText.text = "������ �߰��ߴ�\n���� 1ȹ��";
                    advRoomEvent.Ax++;
                }
                else
                {
                    eventText.text = "������ �߰��ߴ�\n���� 1ȹ��";
                    advRoomEvent.Tool++;
                }
            }
            else
            {
                eventText.text = "�����ؼ� �ƹ��͵� ã�� ���ߴ�.";
            }
            endNormalEvent();
        }
        if (eventType == "Restaurant")
        {
            int randint = Random.Range(0, 4);
            if (randint == 0)
            {
                advRoomEvent.Food += 2;
                eventText.text = "������ �߰��ߴ�.\n���� 2ȹ��";
            }
            else if (randint == 1)
            {
                advRoomEvent.Water += 2;
                eventText.text = "���� �߰��ߴ�.\n�� 2ȹ��";
            }
            else if (randint == 2)
            {
                advRoomEvent.Food++;
                advRoomEvent.Water++;
                eventText.text = "���İ� ���� �߰��ߴ�.\n����, �� 1ȹ��";
            }
            else
            {
                eventText.text = "�ƹ��͵� ã�� ���ߴ�.";
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
        // ����
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

        // ����A
        if (eventType == "Shower" || eventType == "BedRoom" || eventType == "Medicine" || eventType == "Infirmary" || eventType == "Rocker")
        {
            endEvent();
        }

        // ����B
        if (eventType == "Research" || eventType == "Gas" || eventType == "Toilet" || eventType == "Robby")
        {
            endEvent();
        }

        // ����C
        if (eventType == "PowerRoom" || eventType == "Warehouse" || eventType == "Restaurant")
        {
            endEvent();
        }

        // �̴ϰ���
        if (eventType == "PW" || eventType == "PWBox" || eventType == "Color" || eventType == "Oil" || eventType == "Temper")
        {
            endEvent();
        }
    }

    public void callEvent() // Ȯ���� ���� �̺�Ʈ �߻�
    {
        eventState = false;
        endState = false;

        index = 0; // �ε��� �ʱ�ȭ
        eventText.text = ""; // �ؽ�Ʈ �ʱ�ȭ

        Debug.Log("�̺�Ʈ ����Ʈ ����");
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
        NextBtn.gameObject.SetActive(true); // �̺�Ʈ ���� �� next ��ư�� textâ�� ����
        eventText.gameObject.SetActive(true);
    }

    public void endEvent()
    {
        eventText.text = "�ƹ��ϵ� �Ͼ�� �ʾҴ�.";
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

    private IEnumerator WaitPW() // �̼��� ���������� ��ٸ�
    {
        yield return new WaitUntil(() => PWMission.end == true);
        advRoomEvent.Research++;
        eventText.text = "��蹮�� ����� �����ڷᰡ ����ִ�.\n�����ڷ� 1 ȹ��.";
        PW.SetActive(false);
        endNormalEvent();
    }

    private IEnumerator WaitPWBox()
    {
        yield return new WaitUntil(() => PWBoxMission.end == true);
        advRoomEvent.Food += 2;
        advRoomEvent.Water += 2;
        eventText.text = "���ھȿ� ���ķ��� ����ִ�.\n�ķ�, �� 2 ȹ��.";
        PWBox.SetActive(false);
        endNormalEvent();
    }

    private IEnumerator WaitPattern()
    {
        yield return new WaitUntil(() => colorMission.end == true);
        advRoomEvent.Oxygentank++;
        eventText.text = "���� �ȿ� ������� �ִ�.\n����� 1ȹ��";
        ColorRoom.SetActive(false);
        endNormalEvent();
    }

    private IEnumerator WaitOil()
    {
        yield return new WaitUntil(() => oilMission.isFull == true);
        advRoomEvent.Food++;
        eventText.text = "��ô�Ⱑ �۵��Ѵ�.\n��Ʈ�� ���� �������� ���Դ�.\n���� 1ȹ��";
        oilTank.SetActive(false);
        endNormalEvent();
    }
    private IEnumerator WaitTemper()
    {
        yield return new WaitUntil(() => temperMission.end == true);
        advRoomEvent.Battery += 2;
        eventText.text = "�µ��� �����ϴ� ��谡 �۵��Ѵ�.\n��迡�� ���͸��� ���Դ�\n���͸� 2ȹ��";
        temperCheck.SetActive(false);
        endNormalEvent();
    }
}
