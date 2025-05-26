/*
 * File :   CreateCharacter.cs
 * Desc :   캐릭터 생성
 */

using UnityEngine;

public class CreateCharacter
{
    private NPCBuff _nb = new NPCBuff();

    public CharacterData CharacterFarmed(string job, bool farmed)
    {
        switch (job)
        {
            case "Doctor":
                if (!farmed)
                    return null;
                else {
                    CharacterData doctor = new CharacterData("Doctor"); // 의사 생성시 건강, 정신 관련 버프
                    _nb.DoctorBuffOnOff(true);
                    return doctor; 
                }
                    
            case "Engineer":
                if (!farmed)
                    return null;
                else{
                    CharacterData engineer = new CharacterData("Engineer");
                    _nb.EngineerBuffOnOff(true);
                    return engineer;
                }
                
            case "Guard":
                if (!farmed)
                    return null;
                else{
                    CharacterData guard = new CharacterData("Guard");
                    _nb.GuardBuffOnOff(true);
                    return guard;
                }
                
            case "Player":
                CharacterData player = new CharacterData("Player");
                return player;
            default:
                Debug.LogWarning("Invalid job type: " + job);
                return null;
        }

    }
}
