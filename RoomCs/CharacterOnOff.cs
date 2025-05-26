using UnityEngine;

public class CharacterOnOff : MonoBehaviour
{
    public GameObject cha;
    public GameObject character;
    private Status status;

    void Start()
    {
        status = cha.GetComponent<Status>();
    }
    void Update()
    {
        if (status.isGoAdventure == true)
        {
            character.SetActive(false);
        }
        else if(status.isGoAdventure == false)
        {
            character.SetActive(true);
        }
    }
}
