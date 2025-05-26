using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PWMission : MonoBehaviour
{
    public TextMeshProUGUI Hint; // 힌트
    public TextMeshProUGUI InputPW; // 입력 비밀번호
    public Image backGround;

    public string correctPW; //정답 비밀번호
    public bool end;
    public void MakePW()
    {
        correctPW = "";
        InputPW.text = "";

        backGround.color = Color.black;

        for(int i = 0; i< 8; i++)
        {
            correctPW += Random.Range(0, 10).ToString();
        }

        Hint.text = correctPW;
        end = false;
    }


    public void CheckPW()
    {
        if (Hint.text == InputPW.text)
        {
            InputPW.text = "CORRECT!!";
            backGround.color = Color.green;
            end = true;
        }
        else
        {
            InputPW.text = "";
            backGround.color = Color.red;
        }
    }
}
