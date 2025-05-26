using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    public Button newButton;

    void Start()
    {
        newButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("3DItem");
        });
    }

}
