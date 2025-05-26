using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public ColorClick img1;
    public ColorClick img2;
    public ColorClick img3;
    public ColorClick img4;
    public ColorClick img5;
    public ColorClick img6;
    public ColorClick img7;
    public ColorClick img8;
    public ColorClick img9;

    int[] numbers = new int[9];
    public TextMeshProUGUI Puzzle;
    public bool end;

    public ColorBordImg[] bord;
    public void MakePattern()
    {
        for (int i = 0; i < 9; i++)
        {
            numbers[i] = Random.Range(0, 6);
            bord[i].index = numbers[i];
            bord[i].Image.sprite = bord[i].sprites[numbers[i]];
            Puzzle.text += numbers[i] + " ";
        }
        end = false;
    }

    public void CheckIndex()
    {
        if(img1.index == numbers[0] && img2.index == numbers[1] && img3.index == numbers[2] && img4.index == numbers[3] && img5.index == numbers[4] && img6.index == numbers[5] && img7.index == numbers[6] && img8.index == numbers[7] && img9.index == numbers[8])
        {
            img1.colorReset();
            img2.colorReset();
            img3.colorReset();
            img4.colorReset();
            img5.colorReset();
            img6.colorReset();
            img7.colorReset();
            img8.colorReset();
            img9.colorReset();
            Puzzle.text = "";
            for(int i=0; i<9; i++)
            {
                bord[i].colorReset();
            }
            end = true;
        }
    }
}
