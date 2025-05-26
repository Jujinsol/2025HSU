using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class TemperMission : MonoBehaviour
{
    public TemperUpDown temperUpDown;

    public TextMeshProUGUI adjust;
    public TextMeshProUGUI goal;

    int plusTemper;
    int minusTemper;
    public bool end;

    public void makeTemper()
    {
        temperUpDown.setTemper();
        adjust.text = "";
        end = false;

        plusTemper = Random.Range(1, 16) * 10; // 10 ~ 150 抄荐 积己
        minusTemper = Random.Range(-15, 0) * 10; // -150 ~ -10 抄荐 积己

        int randint = Random.Range(0, 2);

        if(randint == 0)
        {
            goal.text = plusTemper.ToString();
        }
        else
        {
            goal.text = minusTemper.ToString();
        }
    }

    public void checkTemper()
    {
        if (adjust.text == goal.text)
        {
            goal.text = "SUCCESS";
            end = true;
        }

    }
}
