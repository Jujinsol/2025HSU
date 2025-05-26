using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyPad : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI password;
    public PWMission PWMission;

    string number;
    void Start()
    {
        number = GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void OnPointerClick(PointerEventData eventdata)
    {
        if (number != "X" && number != "O")
        {
            if (password.text.Length < 8) // 8자리 비밀번호
            {
                password.text += number;
            }
        }
        else if(number == "X" && password.text.Length > 0) // 지우기 버튼
        {
            string text = password.text;
            text = text.Substring(0, text.Length - 1);
            password.text = text;
        }
        else if(number == "O")
        {
            PWMission.CheckPW();
        }
    }
}
