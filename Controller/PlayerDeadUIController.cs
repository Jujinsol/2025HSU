using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeadUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private Button toFirst;

    void Start()
    {
        logText.text = GameManager.Instance.nextDayLog;
        toFirst.onClick.AddListener(() =>
         {
             SceneManager.LoadScene("Start");
         });
    }
}
