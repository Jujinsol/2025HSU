using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SequenceKey : MonoBehaviour, IPointerClickHandler
{
    public Image img;
    public SequenceMission mission;
    string number;

    void Start()
    {
        number = GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void OnPointerClick(PointerEventData eventdata)
    {
        if (mission.inputSequence.Length < 7) {
            mission.inputSequence += number;
            img.color = Color.red;
        }
        else
        {
            mission.inputSequence += number;
            img.color = Color.red;
            mission.checkSequence();
        }
    }
}
