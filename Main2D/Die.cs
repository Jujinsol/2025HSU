/*
 * File :   Die.cs
 * Desc :   
 */
using UnityEngine;
using System.Collections.Generic;

public class Die
{
    private NPCBuff _nf = new NPCBuff();

    public string RemoveDeadCharacters()
    {
        List<CharacterData> toRemove = new List<CharacterData>();

        foreach (CharacterData character in GameManager.Instance.characterList)
        {
            if (character != null && !character.isAlive)
            {
                toRemove.Add(character);
            }
        }

        string message = "";

        foreach (CharacterData character in toRemove)
        {
            string jobName = character.job switch
            {
                "Doctor" => "?˜?‚¬",
                "Engineer" => "? •ë¹„ì‚¬",
                "Guard" => "ê²½ë¹„",
                "Player" => "?”Œ? ˆ?´?–´",
                _ => "?•Œ ?ˆ˜ ?—†?Œ"
            };

            switch (character.job)
            {
                case "Doctor":
                    _nf.DoctorBuffOnOff(false);
                    GameManager.Instance.doctor = null;
                    break;
                case "Engineer":
                    _nf.EngineerBuffOnOff(false);
                    GameManager.Instance.engineer = null;
                    break;
                case "Guard":
                    _nf.GuardBuffOnOff(false);
                    GameManager.Instance.guard = null;
                    break;
            }


            int idx = GameManager.Instance.characterList.IndexOf(character);
            if (idx >= 0)
                GameManager.Instance.characterList[idx] = null;

            message += $"{jobName}: {character.deathReason}\n";
        }

        return message.TrimEnd();
    }

}
