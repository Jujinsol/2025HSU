using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class turorial : MonoBehaviour
{
    public TMP_Text guideText;

    void Start()
    {
        StartCoroutine(ShowGuide());
    }

    IEnumerator ShowGuide()
    {
       
        yield return new WaitForSeconds(5f);
        guideText.gameObject.SetActive(false);
    }
}
