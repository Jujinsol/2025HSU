using UnityEngine;

public class StatChanger
{
    private void ModifyStat(ref int stat, int unit, int multiple, bool increase)
    {
        int amount = unit * multiple;
        stat += increase ? amount : -amount;
    }

    public void GainAffection(CharacterData c, int m)
    {
        if (c == null) return;
        ModifyStat(ref c.affection, GameManager.Instance.player.affectionGainUnit, m, true);
    }

    public void LoseAffection(CharacterData c, int m)
    {
        if (c == null || c.job == "Player") return;
        ModifyStat(ref c.affection, GameManager.Instance.player.affectionLossUnit, m, false);
    }

    public void GainFood(CharacterData c, int m)
    {
        ModifyStat(ref c.food, GameManager.Instance.player.foodGainUnit, m, true);
    }

    public void LoseFood(CharacterData c, int m)
    {
        ModifyStat(ref c.food, GameManager.Instance.player.foodLossUnit, m, false);
    }

    public void GainWater(CharacterData c, int m)
    {
        ModifyStat(ref c.water, GameManager.Instance.player.waterGainUnit, m, true);
    }

    public void LoseWater(CharacterData c, int m)
    {
        ModifyStat(ref c.water, GameManager.Instance.player.waterLossUnit, m, false);
    }

    public void GainMental(CharacterData c, int m)
    {
        ModifyStat(ref c.mental, GameManager.Instance.player.mentalGainUnit, m, true);
    }

    public void LoseMental(CharacterData c, int m)
    {
        ModifyStat(ref c.mental, GameManager.Instance.player.mentalLossUnit, m, false);
    }

    public void GainElectricity(CharacterData c, int m)
    {
        ModifyStat(ref c.electricity, GameManager.Instance.player.electricityGainUnit, m, true);
    }

    public void LoseElectricity(CharacterData c, int m)
    {
        ModifyStat(ref c.electricity, GameManager.Instance.player.electricityLossUnit, m, false);
    }

    public void GainHealth(CharacterData c, int m)
    {
        ModifyStat(ref c.health, GameManager.Instance.player.healthGainUnit, m, true);
    }

    public void LoseHealth(CharacterData c, int m)
    {
        ModifyStat(ref c.health, GameManager.Instance.player.healthLossUnit, m, false);
    }
}