using UnityEngine;
using UnityEngine.UI;

public class OilManager : MonoBehaviour
{
    public Image Oil; // ���� �̹���
    public float fillSpeed = 0.5f; // ä������ �ӵ�

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
