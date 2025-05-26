using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorBordImg : MonoBehaviour
{
    public Image Image;
    public Sprite[] sprites;
    public ColorManager colorManager;

    public int index = 0;
    public void colorReset()
    {
        index = 0;
        Image.sprite = sprites[0];
    }
}
