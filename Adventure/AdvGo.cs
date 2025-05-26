using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 선택한 이미지의 캐릭터 정보를 모험 캐릭터에 넘겨줌
public class AdvGo : MonoBehaviour
{
    public GameObject targetObject; // 모험 캐릭터
    public Image AdvCharacterImg; // 모험 캐릭터 이미지 
    public GameObject AdvCanvas;
    public itemSelect item;

    public TextMeshProUGUI statusCheck;
    public TextMeshProUGUI statusCheck2;
    public TextMeshProUGUI statusCheck3;

    //public Camera roomCam;  // 방 카메라
    //public Camera advCam; // 모험 카메라

    public RoomEvent RoomManager; //방의 상태를 받기 위함
    public CoverSpwan coverSpwaner;
    public AdvEnd endDay;

    public bool canSend = false;

    public void checkSend()
    {
        if (PanelClick.selectedImg == null || SelectMap.selectedImg == null)
        {
            Debug.Log("선택된 이미지 없음");
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
        //GameObject selectedCharacter = PanelClick.selectedImg.linkCharacter; // 선택된 캐릭터 이미지
        GameObject seledtedMap = SelectMap.selectedImg.gameObject;

        Image Img = PanelClick.selectedImg.GetComponent<Image>();
        AdvCharacterImg.sprite = Img.sprite;


        Status roomStatus = PanelClick.selectedImg.linkCharacter; // 선택한 캐릭터 정보
        Status advStatus = targetObject.GetComponent<Status>(); // 모험 중 캐릭터 정보

        AdvCharacter AdvMove = targetObject.GetComponent<AdvCharacter>();// 모험 중 움직일 캐릭터
        AdvCanvas.SetActive(true);

        string selectedMap = SelectMap.selectedMap; // 선택된 맵 정보

        advStatus.CharacterName = roomStatus.CharacterName; // 선택된 캐릭터 status를 모험할 캐릭터에게 줌
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

        if (advStatus.quickAdv == true) //특성에 따른 모험일 설정
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

        if (advStatus.randomAdv == true) // 길치 특성 소지 시 탐험 구역 랜덤
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
            AdvMove.AdvDay = 1; //모험 진행일 1로 초기화;
            coverSpwaner.setCover();
        }

        Debug.Log("전달 완료");
    }
}
