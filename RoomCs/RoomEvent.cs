using TMPro;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{
    public int day = GameManager.Instance.day;
    public bool isAdv; //현재 모험중인 캐릭터가 있는지 체크

    //public Camera roomCam;  // 방 카메라
    //public Camera advCam; // 모험 카메라

    public TextMeshProUGUI DayText; //현재 날짜 표시 text

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
