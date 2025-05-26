using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TemperUpDown : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI adjust;
    public TemperMission temperMission;

    static int adjustTemper;
    public void setTemper()
    {
        adjustTemper = 0;
    }

    public void OnPointerClick(PointerEventData eventdata)
    {
        GameObject clicked = eventdata.pointerPress;

        if (clicked.name == "Up")
        {
            adjustTemper += 10;
        }
        else if (clicked.name == "Down")
        {
            adjustTemper -= 10;
        }
        adjust.text = adjustTemper.ToString();
        temperMission.checkTemper();
    }
}