using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// ĳ���� �̹��� Ŭ���� ���� ȿ��
public class PanelClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject border; // �׵θ�
    public Status linkCharacter; // ������ ĳ����

    public GameObject Character;

    public TextMeshProUGUI statusCheck; // ���� ������ ����
    public TextMeshProUGUI statusCheck2; // ���� ������ ����
    public TextMeshProUGUI statusCheck3; // ���� ������ ����
    Status status, s;

    public string imgID;
    public static PanelClick selectedImg; //���� ���õ� �̹���
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
        if (selectedImg != null) // ���õ� �̹����� ���� �� �׵θ� ����
        {
            selectedImg.border.SetActive(false);
        }

        if (selectedImg == this)
        {
            border.SetActive(false);
            selectedImg = null;
            selectedID = null;
            Debug.Log("���� ������");
            statusCheck.text = "";
            statusCheck2.text = "";
            statusCheck3.text = "";
            return;
        }

        border.SetActive(true); // �׵θ� �ѱ�
        selectedImg = this;
        selectedID = imgID; // �̹��� ID ����

        if (status.CharacterName == "jim")
        {
            statusCheck2.text = "";
            statusCheck3.text = "";
            if (GameManager.Instance.engineer.food <= 30)
            {
                statusCheck.text = "���� �� ����...";
            }
            else if (GameManager.Instance.engineer.food > 30 && GameManager.Instance.engineer.food <= 60)
            {
                statusCheck.text = "�Ҹ���";
            }
            else
            {
                statusCheck.text = "�ڽ��־�!!";
            }

        }
        else if (status.CharacterName == "park")
        {
            statusCheck.text = "";
            statusCheck3.text = "";
            if (GameManager.Instance.guard.food <= 30)
            {
                statusCheck2.text = "���� �� ����...";
            }
            else if (GameManager.Instance.guard.food > 30 && GameManager.Instance.guard.food <= 60)
            {
                statusCheck2.text = "�Ҹ���";
            }
            else
            {
                statusCheck2.text = "�ڽ��־�!!";
            }
        }
        else if (status.CharacterName == "kate")
        {
            statusCheck.text = "";
            statusCheck2.text = "";
            if (GameManager.Instance.doctor.food <= 30)
            {
                statusCheck3.text = "���� �� ����...";
            }
            else if (GameManager.Instance.doctor.food > 30 && GameManager.Instance.doctor.food <= 60)
            {
                statusCheck3.text = "�Ҹ���";
            }
            else
            {
                statusCheck3.text = "�ڽ��־�!!";
            }
        }
    }

}
