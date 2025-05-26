using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// 캐릭터 이미지 클릭시 선택 효과
public class PanelClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject border; // 테두리
    public Status linkCharacter; // 연결할 캐릭터

    public GameObject Character;

    public TextMeshProUGUI statusCheck; // 모험 성공률 추측
    public TextMeshProUGUI statusCheck2; // 모험 성공률 추측
    public TextMeshProUGUI statusCheck3; // 모험 성공률 추측
    Status status, s;

    public string imgID;
    public static PanelClick selectedImg; //현재 선택된 이미지
    public static string selectedID;

    void Start()
    {
        if (gameObject.name == "Character1")
        {
            if (!Convert.ToBoolean(Storage.inst.yellow) || GameManager.Instance.engineer == null)
                gameObject.SetActive(false);
            else
            {
                s = new Status();
                s.CharacterName = "jim";
                s.Job = GameManager.Instance.engineer.job;
                s.hp = GameManager.Instance.engineer.health;
                s.mental = GameManager.Instance.engineer.mental;

                if (!Convert.ToBoolean(Storage.inst.yellow)) Character.SetActive(false);
                linkCharacter = s;
            }
        }
        else if (gameObject.name == "Character2")
        {
            if (!Convert.ToBoolean(Storage.inst.blue) || GameManager.Instance.guard == null)
                gameObject.SetActive(false);
            else
            {
                s = new Status();
                s.CharacterName = "park";
                s.Job = GameManager.Instance.guard.job;
                s.hp = GameManager.Instance.guard.health;
                s.mental = GameManager.Instance.guard.mental;

                if (!Convert.ToBoolean(Storage.inst.blue)) Character.SetActive(false);
                linkCharacter = s;
            }
        }
        else if (gameObject.name == "Character3")
        {
            if (!Convert.ToBoolean(Storage.inst.white) || GameManager.Instance.doctor == null)
                gameObject.SetActive(false);
            else
            {
                s = new Status();
                s.CharacterName = "kate";
                s.Job = GameManager.Instance.doctor.job;
                s.hp = GameManager.Instance.doctor.health;
                s.mental = GameManager.Instance.doctor.mental;

                if (!Convert.ToBoolean(Storage.inst.white)) Character.SetActive(false);
                linkCharacter = s;
            }
        }
        status = s;
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
            selectedID = null;
            Debug.Log("선택 해제됨");
            statusCheck.text = "";
            statusCheck2.text = "";
            statusCheck3.text = "";
            return;
        }

        border.SetActive(true); // 테두리 켜기
        selectedImg = this;
        selectedID = imgID; // 이미지 ID 저장

        if (status.CharacterName == "jim")
        {
            statusCheck2.text = "";
            statusCheck3.text = "";
            if (GameManager.Instance.engineer.food <= 30)
            {
                statusCheck.text = "힘들 것 같아...";
            }
            else if (GameManager.Instance.engineer.food > 30 && GameManager.Instance.engineer.food <= 60)
            {
                statusCheck.text = "할만해";
            }
            else
            {
                statusCheck.text = "자신있어!!";
            }

        }
        else if (status.CharacterName == "park")
        {
            statusCheck.text = "";
            statusCheck3.text = "";
            if (GameManager.Instance.guard.food <= 30)
            {
                statusCheck2.text = "힘들 것 같아...";
            }
            else if (GameManager.Instance.guard.food > 30 && GameManager.Instance.guard.food <= 60)
            {
                statusCheck2.text = "할만해";
            }
            else
            {
                statusCheck2.text = "자신있어!!";
            }
        }
        else if (status.CharacterName == "kate")
        {
            statusCheck.text = "";
            statusCheck2.text = "";
            if (GameManager.Instance.doctor.food <= 30)
            {
                statusCheck3.text = "힘들 것 같아...";
            }
            else if (GameManager.Instance.doctor.food > 30 && GameManager.Instance.doctor.food <= 60)
            {
                statusCheck3.text = "할만해";
            }
            else
            {
                statusCheck3.text = "자신있어!!";
            }
        }
    }

}
