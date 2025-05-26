using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ������ �̹����� ĳ���� ������ ���� ĳ���Ϳ� �Ѱ���
public class AdvGo : MonoBehaviour
{
    public GameObject targetObject; // ���� ĳ����
    public Image AdvCharacterImg; // ���� ĳ���� �̹��� 
    public GameObject AdvCanvas;
    public itemSelect item;

    public TextMeshProUGUI statusCheck;
    public TextMeshProUGUI statusCheck2;
    public TextMeshProUGUI statusCheck3;

    //public Camera roomCam;  // �� ī�޶�
    //public Camera advCam; // ���� ī�޶�

    public RoomEvent RoomManager; //���� ���¸� �ޱ� ����
    public CoverSpwan coverSpwaner;
    public AdvEnd endDay;

    public bool canSend = false;

    public void checkSend()
    {
        if (PanelClick.selectedImg == null || SelectMap.selectedImg == null)
        {
            Debug.Log("���õ� �̹��� ����");
            canSend = false;
            endDay.goRoom();
        }
        else
        {
            canSend = true;
            AdvCharacterImg.enabled = true;
            SendStatus();
        }
    }

    public void SendStatus()
    {
        //GameObject selectedCharacter = PanelClick.selectedImg.linkCharacter; // ���õ� ĳ���� �̹���
        GameObject seledtedMap = SelectMap.selectedImg.gameObject;

        Image Img = PanelClick.selectedImg.GetComponent<Image>();
        AdvCharacterImg.sprite = Img.sprite;


        Status roomStatus = PanelClick.selectedImg.linkCharacter; // ������ ĳ���� ����
        Status advStatus = targetObject.GetComponent<Status>(); // ���� �� ĳ���� ����

        AdvCharacter AdvMove = targetObject.GetComponent<AdvCharacter>();// ���� �� ������ ĳ����
        AdvCanvas.SetActive(true);

        string selectedMap = SelectMap.selectedMap; // ���õ� �� ����

        advStatus.CharacterName = roomStatus.CharacterName; // ���õ� ĳ���� status�� ������ ĳ���Ϳ��� ��
        advStatus.hp = roomStatus.hp;
        advStatus.mental = roomStatus.mental;
        advStatus.quickAdv = roomStatus.quickAdv;
        advStatus.randomAdv = roomStatus.randomAdv;
        item.sendToAdvItem();

        statusCheck.text = "";
        statusCheck2.text = "";
        statusCheck3.text = "";

        advStatus.isGoAdventure = true;
        roomStatus.isGoAdventure = true;

        if (advStatus.quickAdv == true) //Ư���� ���� ������ ����
        {
            AdvMove.advDate = 2;
        }
        else
        {
            AdvMove.advDate = 3;
        }

        
        if(roomStatus.hp >= 100)
        {
            AdvMove.move = 7;
        }
        else if (roomStatus.hp < 100 && roomStatus.hp >= 50)
        {
            AdvMove.move = 6;
        }
        else if (roomStatus.hp < 50)
        {
            AdvMove.move = 5;
        }

        if (advStatus.randomAdv == true) // ��ġ Ư�� ���� �� Ž�� ���� ����
        {
            Debug.Log(advStatus.randomAdv);
            int randInt = Random.Range(0, 3);
            if (randInt == 0)
            {
                AdvMove.area = "A";
            }
            else if (randInt == 1)
            {
                AdvMove.area = "B";
            }
            else
            {
                AdvMove.area = "C";
            }
        }
        else
        {
            AdvMove.area = selectedMap;
        }


        //advCam.enabled = true;
        //roomCam.enabled = false;

        if (RoomManager.isAdv == false)
        {
            AdvMove.AdvDay = 1; //���� ������ 1�� �ʱ�ȭ;
            coverSpwaner.setCover();
        }

        Debug.Log("���� �Ϸ�");
    }
}
