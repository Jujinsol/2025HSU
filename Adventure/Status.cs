using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public string CharacterName; 
    public string Job; 

    public int hp;
    public int mental;

    public bool isGoAdventure;
    public List<string> itemList = new List<string>();

    public bool quickAdv;
    public bool randomAdv;
}
