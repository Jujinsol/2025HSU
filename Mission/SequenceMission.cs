using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SequenceMission : MonoBehaviour
{
    public string correctSequence ="12345678"; //정답 비밀번호
    public string inputSequence = "";
    public bool end = false;

    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;
    public Image img5;
    public Image img6;
    public Image img7;
    public Image img8;

    public void checkSequence()
    {
        if (correctSequence == inputSequence)
        {
            Debug.Log("정답");
            end = true;
            img1.color = new Color32(131, 255, 153, 255);
            img2.color = new Color32(131, 255, 153, 255);
            img3.color = new Color32(131, 255, 153, 255);
            img4.color = new Color32(131, 255, 153, 255);
            img5.color = new Color32(131, 255, 153, 255);
            img6.color = new Color32(131, 255, 153, 255);
            img7.color = new Color32(131, 255, 153, 255);
            img8.color = new Color32(131, 255, 153, 255);
        }
        else
        {
            img1.color = new Color32(131, 255, 153, 255);
            img2.color = new Color32(131, 255, 153, 255);
            img3.color = new Color32(131, 255, 153, 255);
            img4.color = new Color32(131, 255, 153, 255);
            img5.color = new Color32(131, 255, 153, 255);
            img6.color = new Color32(131, 255, 153, 255);
            img7.color = new Color32(131, 255, 153, 255);
            img8.color = new Color32(131, 255, 153, 255);
            inputSequence = "";
        }
    }
}
