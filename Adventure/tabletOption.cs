using UnityEngine;
using UnityEngine.UI;

public class tabletOption : MonoBehaviour
{
    public Button NextBtn;

    public GameObject AdvScreen;

    public RoomEvent RoomManager;
    public GameObject advCharacter;

    public GameObject AdvCanvas;
    public GameObject RoomCanvas;
    public Image AdvCharacterImg;
    public Camera roomCam;  // ?? ????
    public Camera advCam; // ???? ????

    public GameObject AdvOn;
    public GameObject AdvObject;

    void Start()
    {
        NextBtn.onClick.AddListener(NextClick);
    }

    public void NextClick()
    {
        if (AdvScreen.activeSelf)
        {
            AdvGo advGo = AdvOn.GetComponent<AdvGo>();
            advGo.checkSend();
            if (advGo.canSend == true)
            {
                AdvCanvas.SetActive(true);
                AdvCharacterImg.enabled = true;
                RoomCanvas.SetActive(false);
                AdvObject.SetActive(true);
            }
        }
    }
}
