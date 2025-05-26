using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 모험 끝내기 버튼을 눌렀을 때 이벤트
public class AdvEnd : MonoBehaviour
{
    public Image AdvCharacterImg;
    //public Camera roomCam;  // 방 카메라
    //public Camera advCam; // 모험 카메라
    public GameObject AdvCanvas;

    public GameObject advEndCanvas;
    public GameObject roomCanvas;
    public GameObject roomPanel;

    public GameObject advScreen;
    public GameObject RoomEventManaer;

    public TextMeshProUGUI itemList;

    public void goRoom()
    {
        SceneManager.LoadScene("Main2DScene");
        GameManager.Instance.day++;

        /*
        RoomEvent roomEvent = RoomEventManaer.GetComponent<RoomEvent>();

        roomCam.enabled = true; //카메라 변경
        advCam.enabled = false;
        AdvCharacterImg.enabled = false;
        advEndCanvas.SetActive(false); // 모험에서 사용하는 캔버스 닫기

        if (roomEvent.isAdv == true)
        {
            AdvCanvas.SetActive(false);

            roomCanvas.SetActive(true); // 방 캔버스 설정
            roomPanel.SetActive(false);

            advScreen.SetActive(false);
        }
        else
        {
            AdvCanvas.SetActive(false);

            roomCanvas.SetActive(true); // 방 캔버스 설정
            roomPanel.SetActive(true);

            advScreen.SetActive(true);
        }

        roomEvent.day += 1;
        itemList.text = "";
        */
    }
}
