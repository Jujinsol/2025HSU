using TMPro;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{
    public int day = GameManager.Instance.day;
    public bool isAdv; //���� �������� ĳ���Ͱ� �ִ��� üũ

    //public Camera roomCam;  // �� ī�޶�
    //public Camera advCam; // ���� ī�޶�

    public TextMeshProUGUI DayText; //���� ��¥ ǥ�� text

    //void Start()
    //{
    //    roomCam.enabled = true;
    //    advCam.enabled = false;
    //}

    // Update is called once per frame
    void Update()
    {
        DayText.text = "Day:" + GameManager.Instance.day;
    }
}
