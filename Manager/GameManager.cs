using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CharacterData player;
    public CharacterData doctor;
    public CharacterData engineer;
    public CharacterData guard;

    public int day = 1;

    public string nextDayLog = "";

    public List<CharacterData> characterList = new List<CharacterData>();

    public ItemData itemData = new ItemData();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RefreshCharacterList()
    {
        characterList.Clear();
        characterList.Add(player);
        characterList.Add(doctor);
        characterList.Add(engineer);
        characterList.Add(guard);
    }
}