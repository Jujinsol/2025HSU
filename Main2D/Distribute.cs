/*
 * File :   Distribute.cs
 * Desc :   
*/

using UnityEngine;

public class Distribute
{
    public void DistributeWater(CharacterData character, bool distributed)
    {
        if (character != null)
        {
            if (distributed)
            {
                character.water += 30 * GameManager.Instance.player.waterGainUnit; 
                GameManager.Instance.itemData.waterN -= 0.25f;
            }
            else if (character.job != "Player")
            {
                character.affection -= GameManager.Instance.player.affectionLossUnit;
            }
        }

    }

    public void DistributeFood(CharacterData character, bool distributed)
    {
        if (character != null)
        {
            if (distributed)
            {
                switch (character.job)
                {
                    case "Player":
                        character.food += 20 * GameManager.Instance.player.foodGainUnit;
                        break;
                    case "Doctor":
                        character.food += 25 * GameManager.Instance.player.foodGainUnit; 
                        break;
                    case "Engineer":
                        character.food += 20 * GameManager.Instance.player.foodGainUnit; 
                        break;
                    case "Guard":
                        character.food += 20 * GameManager.Instance.player.foodGainUnit; 
                        break;
                    default:
                        break;
                }
                GameManager.Instance.itemData.foodN -= 0.25f;
            }
            else if (character.job != "Player")
            {
                character.affection -= GameManager.Instance.player.affectionLossUnit;
            }
        }

    }

    public void DistributeMedKit(CharacterData character, bool distributed)
    {
        if (character != null)
        {
            if (distributed)
            {
                character.isSick = false;
                GameManager.Instance.itemData.medKitN--;
            }
            else if (character.job != "Player")
            {
                character.affection -= GameManager.Instance.player.affectionLossUnit;
            }
        }
    }
}
