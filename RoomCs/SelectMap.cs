using UnityEngine;
using UnityEngine.EventSystems;

// 맵 이미지 선택 시
public class SelectMap : MonoBehaviour, IPointerClickHandler
{
    public GameObject border; // 테두리

    public string Map;
    public static SelectMap selectedImg; //현재 선택된 이미지
    public static string selectedMap;

    void Start()
    {
        border.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventdata)
    {
        if (selectedImg != null) // 선택된 이미지가 없을 시 테두리 끄기
        {
            selectedImg.border.SetActive(false);
        }

        if (selectedImg == this)
        {
            border.SetActive(false);
            selectedImg = null;
            selectedMap = null;
            Debug.Log("선택 해제됨");
            return;
        }

        border.SetActive(true); // 테두리 켜기
        selectedImg = this;
        selectedMap = Map; // 맵 이름 저장

        Debug.Log("선택된 이미지: " + selectedMap);
    }
}
