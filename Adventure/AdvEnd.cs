using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� ������ ��ư�� ������ �� �̺�Ʈ
public class AdvEnd : MonoBehaviour
{
    public Image AdvCharacterImg;
    //public Camera roomCam;  // �� ī�޶�
    //public Camera advCam; // ���� ī�޶�
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

        roomCam.enabled = true; //ī�޶� ����
        advCam.enabled = false;
        AdvCharacterImg.enabled = false;
        advEndCanvas.SetActive(false); // ���迡�� ����ϴ� ĵ���� �ݱ�

        if (roomEvent.isAdv == true)
        {
            AdvCanvas.SetActive(false);

            roomCanvas.SetActive(true); // �� ĵ���� ����
            roomPanel.SetActive(false);

            advScreen.SetActive(false);
        }
        else
        {
            AdvCanvas.SetActive(false);

            roomCanvas.SetActive(true); // �� ĵ���� ����
            roomPanel.SetActive(true);

            advScreen.SetActive(true);
        }

        roomEvent.day += 1;
        itemList.text = "";
        */
    }
}
