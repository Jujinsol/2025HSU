using UnityEngine;
using UnityEngine.UI;

public class OilManager : MonoBehaviour
{
    public Image Oil; // 오일 이미지
    public float fillSpeed = 0.5f; // 채워지는 속도

    public bool isFull = false;

    public void setOil()
    {
        isFull = false;
        Oil.fillAmount = 0;
    }

    void Update()
    {
        if (isFull == false)
        {

            if (Input.GetKey(KeyCode.Space) && Oil.fillAmount < 1f)
            {
                Oil.fillAmount += Time.deltaTime * fillSpeed;
            }
            else
            {
                Oil.fillAmount -= Time.deltaTime * fillSpeed;
            }

            if(Oil.fillAmount >= 1f)
            {
                Debug.Log("Clear");
                isFull = true;
                return;
            }

            Oil.fillAmount = Mathf.Clamp01(Oil.fillAmount);
        }
    }
}
