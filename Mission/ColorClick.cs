using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorClick : MonoBehaviour, IPointerClickHandler
{
    public Image Image;
    public Sprite[] sprites;
    public ColorManager colorManager;
    
    public int index = 0;

    public void OnPointerClick(PointerEventData eventdata)
    {
        index++;
        if(index >= sprites.Length)
        {
            index = 0;
        }

        Image.sprite = sprites[index];

        colorManager.CheckIndex(); 
    }

    public void colorReset()
    {
        index = 0;
        Image.sprite = sprites[0];
    }
}
