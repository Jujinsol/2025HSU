/*
 * File :   NextDay.cs
 * Desc :   ���� ���� �Ѿ ��
 */

using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class NextDay
{
    private Die _die = new Die();
    private UsingItem usingItem = new UsingItem();
    private StatChanger statChanger = new StatChanger();
    private StringBuilder sb = new StringBuilder();

    public void RunAllNextDay()
    {
        sb.Clear();

        RunNextDay(GameManager.Instance.player, GameManager.Instance.player.goodLeader);
        if (!GameManager.Instance.player.isAlive)
        {
            GameManager.Instance.nextDayLog = sb.ToString();
            SceneManager.LoadScene("PlayerDeadEnding");
            return;
        }
        RunNextDay(GameManager.Instance.doctor, GameManager.Instance.player.goodLeader);
        RunNextDay(GameManager.Instance.engineer, GameManager.Instance.player.goodLeader);
        RunNextDay(GameManager.Instance.guard, GameManager.Instance.player.goodLeader);

        GameManager.Instance.nextDayLog = sb.ToString();
    }

    void RunNextDay(CharacterData character, bool goodLeader)
    {
        if (character != null)
        {
            statChanger.LoseFood(character, 4); //20
            statChanger.LoseWater(character, 5); //25
            statChanger.LoseMental(character, 1); //5

            if (character.job == "Player")
                HandlePlayerTurn(character);
            else
                HandleNPCTurn(character, goodLeader);

            ClampStats(character);

            // ���� ���� ����
            if (character.job == "Player")
                GameManager.Instance.day++;

            Debug.Log(_die.RemoveDeadCharacters());

            // 30�� �� ���� ����
            if (GameManager.Instance.day >= 30)
                Debug.Log("Game end");
        }
    }

    void HandleNPCTurn(CharacterData cd, bool goodLeader)
    {
        // ȣ���� ���� (������ ���� Ư��)
        if (!goodLeader)
            statChanger.LoseAffection(cd, 1); //5

        // ����, �� ��ġ ���� ���� ��, �ǰ� ��ġ ����
        if (cd.food <= 30 && cd.water <= 30)
        {
            statChanger.LoseHealth(cd, 5); //25
        }
        else if (cd.food <= 30 ||  cd.water <= 30)
        {
            statChanger.LoseHealth(cd, 2); //10
        }

        // �ƻ�� ����
        if (TryDeath(cd, cd.food <= 0, "�ƻ�")) return;
        if (TryDeath(cd, cd.water <= 0, "����")) return;

        float dp = GameManager.Instance.player.deathProb; //�⺻ 0.5f

        // ���� �̻󵵿� ���� Ȯ���� ���
        if (cd.mental <= 0) if (TryChanceDeath(cd, dp, "�ڻ�")) return;
        else if (cd.mental <= 30) if (TryChanceDeath(cd, dp / 5, "�ڻ�")) return;

        // ȣ������ ���� Ȯ���� ���
        if (cd.affection <= 0) if (TryChanceDeath(cd, dp, "���� ���� ������")) return;
        else if (cd.affection <= 20) if (TryChanceDeath(cd, dp / 5, "���� ���� ������")) return;
        
        // �ߺ� ���ο� ���� Ȯ���� ���
        if (cd.isSick) if (TryChanceDeath(cd, dp, "����")) return;

        // �ǰ� ��ġ�� ���� Ȯ���� �ߺ�
        float sp = GameManager.Instance.player.sickProb; // �⺻ 1f

        if (cd.health <= 0) TryChanceSick(cd, sp);
        else if (cd.health <= 30) TryChanceSick(cd, sp / 3);

    }

    void HandlePlayerTurn(CharacterData pl)
    {
        // ����, �� ��ġ ���� ���� ��, �ǰ� ��ġ ����
        if (pl.food <= 30 && pl.water <= 30)
        {
            statChanger.LoseHealth(pl, 5); //25
        }
        else if (pl.food <= 30 || pl.water <= 30)
        {
            statChanger.LoseHealth(pl, 2); //10
        }

        // �ƻ�� ����(���� ���� ������ ����)
        if (TrySurviveOrDeath(pl, pl.food <= 0, "�ƻ�", 10)) return;
        if (TrySurviveOrDeath(pl, pl.water <= 0, "����", 10)) return;

        float dp = GameManager.Instance.player.deathProb; //0.5f

        // ���� �̻󵵿� ���� Ȯ���� ��� �� ���ι���
        if (pl.mental <= 0)
        {
            if (TryChanceDeath(pl, dp, "�ڻ�")) return;
            if (GameManager.Instance.player.overreaction)
            {
                usingItem.DestroyItem(2);
                sb.AppendLine("�Ҿ������� �ᵵ ����� ���� �� ����. ������ ���� ���̴� ���ǵ��� �ν������� ������ ����� ����ϴ�.. �ѹ� Ȯ���غ���.");
            }
        }
        else if (pl.mental <= 30)
        {
            if (TryChanceDeath(pl, dp / 5, "�ڻ�")) return;
            if (GameManager.Instance.player.overreaction)
            {
                usingItem.DestroyItem(1);
                sb.AppendLine("�Ҿ������� �ᵵ ����� ���� �� ����. ������ ���� ���̴� ������ �ν������� ������ ����� ����ϴ�.. �ѹ� Ȯ���غ���.");
            }
        }
        // �ߺ� ���ο� ���� Ȯ���� ���
        if (pl.isSick) if (TryChanceDeath(pl, dp, "����")) return;

        // �ǰ� ��ġ�� ���� Ȯ���� �ߺ�
        float sp = GameManager.Instance.player.sickProb; // �⺻ 1f

        if (pl.health <= 0) TryChanceSick(pl, sp);
        else if (pl.health <= 30) TryChanceSick(pl, sp / 3);

        // ���� �Ҹ� �� ���Ļ�(���� ���)
        statChanger.LoseElectricity(pl, 1); //5
        if (pl.electricity <= 0)
            GameManager.Instance.characterList.ForEach(c =>
            {
                if (c != null)
                {
                    sb.AppendLine((GetDeathMessage(c.job, "���Ļ�")));
                    c.isAlive = false;
                    c.deathReason = "���Ļ�";
                }
            });
    }


    // ��� ��� ó��
    bool TryDeath(CharacterData c, bool condition, string reason)
    {
        if (condition)
        {
            sb.AppendLine((GetDeathMessage(c.job, reason)));
            c.isAlive = false;
            c.deathReason = reason;
            return true;
        }
        return false;
    }

    // Ȯ�� ��� ��� ó��
    bool TryChanceDeath(CharacterData c, float chance, string reason)
    {
        if (Random.value < chance)
        {
            sb.AppendLine((GetDeathMessage(c.job, reason)));
            c.isAlive = false;
            return true;
        }
        return false;
    }

    void TryChanceSick(CharacterData c, float chance)
    {
        if (Random.value < chance)
        {
            c.isSick = true;
        }
    }

    // ���� ���� �������� ��� ó��(�ƻ�, ���翡 ����)
    bool TrySurviveOrDeath(CharacterData c, bool condition, string reason, int reviveValue)
    {
        if (!condition) return false;

        if (c.survivalPro)
        {
            sb.AppendLine("��Ȯ�� ��ﳪ�� ������ ������ ������ ��� �ѱ� ���� Ȯ���ϴ�. �������� �̷� ������ ��Ȳ�� �޾�þ���.. ������ ������ ����. �� �� �������� �Ű澲��.");
            c.isAlive = true;
            c.survivalPro = false;
            if (c.food <= 0)
            {
                c.food = reviveValue;
            }

            if (c.water <= 0)
            {
                c.water = reviveValue;
            }

            return false;
        }
        else
        {
            sb.AppendLine((GetDeathMessage(c.job, reason)));
            c.isAlive = false;
            c.deathReason = reason;
            return true;
        }
    }
    string GetDeathMessage(string job, string reason)
    {
        if (job == "Doctor")
        {
            if (reason == "�ƻ�") return "�̷�.. �ǻ簡 ��� �ᱹ �װ� ���Ҿ�.\n���� �Ϸ��� �����̾�.";
            if (reason == "����") return "�ǻ�� ħ�ǿ��� �ٽô� �������� ���߾�.\n�׳��� �������� ��ü�� �ʹ����� ��������...";
            if (reason == "�ڻ�") return "�ǻ簡 ������ �ʾ� ��ħ���� ���� ���� �����þ�.\n�׸��� ������ ���� �������� ����� ��������..";
            if (reason == "����") return "��ħ�� ����� �� �ǻ��� ��ħ �Ҹ��� �鸮�� �ʾҾ�..\n���� �����ſ��ٸ� �ູ�� �Ϸ����ٵ� ������ ������ �Ϸ��";
            if (reason == "���� ���� ������") return "�ǻ�� �׳��� ���� �������.\n���� ����ϴ� �׳�� ���� ������ �� ���� �Ⱦ�����...\n����ü ���� ���ɱ�?";           
        }
        else if (job == "Guard")
        {
            if (reason == "�ƻ�") return "������ �ָ� �踦 �Ȱ� �Ͼ�� ���߾�.\nó�� ���� ���� ���� �׳��� ����� ����������...";
            if (reason == "����") return "������ Ż���� ���·� ������ �־���.\n�̹� �׳�� �������� �Ŀ���...";
            if (reason == "�ڻ�") return "��ΰ� ��� �㿡 �Ѽ��� ��Ⱦ�.\n�ֱ� �����̴� ������ �ڽ��� �����ִ� ������ �Ѿ��� �ڽ��� ���� ��������..";
            if (reason == "����") return "������ ������ ����� �츮�� ��Ű�µ� ����Ծ�.\n������ ������ �ᱹ �ڽ��� ��Ű�� ���߾�.. �׳ฦ ���� �⵵�ؾ߰ھ�.";
            if (reason == "���� ���� ������") return "������ �׳��� ���� �������.\n���� ����ϴ� �׳�� ���� ������ �� ���� �Ⱦ�����...\n����ü ���� ���ɱ�?";
        }
        else if (job == "Engineer")
        {
            if (reason == "�ƻ�") return "������ ���ſ� ������ ����� ������ ��������.\n�ƹ��� ������.";
            if (reason == "����") return "������� ���� ��ħ�� ���� ��ħ�� �鸮�� �ʾҾ�.\n�׸��� �� ���� �����Ʋ���� ���� �������� ����ع�����..";
            if (reason == "�ڻ�") return "�׻� �̼Ҹ� ���� �ʰ��� �ϴ� ������� ǥ���� �ֱٵ�� ���� �ʾҾ�.\n�׶� ���޾Ҿ�� �ϴµ�... �׸� ���� �⵵����.";
            if (reason == "����") return "������ �׷��� ������ �͵��� ���Ŀ����� �ڽ��� ���� �ᱹ ��ġ�� ���߾�.\n���� �������� �ϴµ�..";
            if (reason == "���� ���� ������") return "������ ���� ���� �������.\n���� ����ϴ� �״� ���� ������ �� ���� �Ⱦ�����...\n����ü ���� ���ɱ�?";
        }
        else if (job == "Player")
        {
            if (reason == "�ƻ�") return "�迡�� ������ �Ҹ��� ������ ��ĥ�� ��������.\n�� ���� �� ������ �������� ����... ���� ������ �ѱ� �� ������?";
            if (reason == "����") return "���� �ʹ� ������. ���� ���� �ν��� �ٴ幰�̶� ���ð� �;�...\n��...���� �ʿ���...";
            if (reason == "�ڻ�") return "����ü �� �������� ��Ȳ�� ���� ������ �ɱ�?\n������ �ڿ��� ���� ������ ���� Ȯ���� �� ���� ��Ȳ.\n�̷��� �׾ �ٿ� ������ �� ������ ������ �����ϴ� �� ���� �� ����.";
            if (reason == "����") return "�������� ������ �ߴµ� ���� ���� �ʹ� �����.\n��� �屸�� �������� ������ ���� ����.\n���� ��� �ʹٴ� ������...";
            if (reason == "���Ļ�") return "���� ���Ⱑ �����Ѱ� ����.\n���� ���� ���� �ϳ��� �������� ��� ���� ��ġ�� �Ҹ��� ���� �۾�����.\n���� �������� ���� �ھ߸� �� �� ����...";
        }
        return "";
    }
    // ���� 0~100 ����
    void ClampStats(CharacterData c)
    {
        c.food = Mathf.Clamp(c.food, 0, 100);
        c.water = Mathf.Clamp(c.water, 0, 100);
        c.mental = Mathf.Clamp(c.mental, 0, 100);
        c.affection = Mathf.Clamp(c.affection, 0, 100);
        c.electricity = Mathf.Clamp(c.electricity, 0, 100);
        c.health = Mathf.Clamp(c.health, 0, 100);
    }

}
