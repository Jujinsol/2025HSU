using UnityEngine;

public class NPCBuff
{
    public void DoctorBuffOnOff(bool on)
    {
        var _pl = GameManager.Instance.player;
        if (on)
        {
            _pl.healthGainUnit += 1;
            _pl.healthLossUnit -= 1;
            _pl.mentalGainUnit += 1;
            _pl.mentalLossUnit -= 1;
            _pl.sickProb -= 0.1f; //1f
        }
        else
        {
            _pl.healthGainUnit -= 1;
            _pl.healthLossUnit += 1;
            _pl.mentalGainUnit -= 1;
            _pl.mentalLossUnit += 1;
            _pl.sickProb += 0.1f;
        }
    }

    public void EngineerBuffOnOff(bool on)
    {
        var _pl = GameManager.Instance.player;
        if (on)
        {
            _pl.facilityFailProb -= 0.1f;
            _pl.repairProb += 0.3f;
        }
        else
        {
            _pl.facilityFailProb += 0.1f;
            _pl.repairProb -= 0.3f;
        }
    }

    public void GuardBuffOnOff(bool on)
    {
        var _pl = GameManager.Instance.player;
        if (on)
        {
            Debug.Log("이벤트나 탐사 관련 버프");
        }
        else
        {
            Debug.Log("이벤트나 탐사 관련 너프");
        }
    }
}
