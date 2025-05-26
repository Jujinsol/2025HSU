/*
 * File :   BuffAndNerf.cs
 * Desc :   ������ ���� ���ÿ� ���� ����Ʈ �� ��ȯ �� ��ġ ����
 */

using UnityEngine;

public class BuffAndNerf
{
    public int BuffPoint(int value)
    {
        switch (value)
        {
            case 1: // �ɸ�����
                return 4;
            case 2: // ���
                return 6;
            case 3: // ��������
                return 4;
            case 4: // �丮��
                return 8;
            case 5: // ������ �Ǵ�
                return 10;
            case 6: // �ü� �����
                return 6;
            case 7: // ����ٴ�
                return 6;
            case 8: //  ������ִ� ȭ��
                return 4;
            case 9: // ���� ���� ������
                return 6;
            case 10: // �ż��� ����
                return 8;
            default:
                break;
        }
        return 0;
    }

    public int NerfPoint(int value)
    {
        switch (value)
        {
            case 1: // ������ ���ڸ�
                return 4;
            case 2: // �� ����
                return 8;
            case 3: // ��İ���
                return 8;
            case 4: // �Ž����� ����
                return 6;
            case 5: // �ܱ� �����
                return 10;
            case 6: // ������ ����
                return 4;
            case 7: // ������ ����
                return 4;
            case 8: // ���� ����
                return 4;
            case 9: // NPC�� ȣ���� ��·� ����
                return 4;
            case 10: // ��ġ
                return 8;
            default:
                break;
        }
        return 0;
    }

    public void FixBuff(int value)
    {
        switch (value)
        {
            case 1: // �ɸ�����, ȣ���� ������ ����
                GameManager.Instance.player.affectionLossUnit -= 2;
                break;
            case 2: // ���, ���İ� ���� �� ���� ����
                GameManager.Instance.player.pharmacist = true;
                break;
            case 3: // ��������, ȣ������ ���� ���·� ����
                foreach (CharacterData character in GameManager.Instance.characterList)
                {
                    if (character != null && character.job != "Player")
                    {
                        character.affection += 30;
                    }
                }
                break;
            case 4: // �丮��, �ķ� ��޿� ���� ������ ����
                GameManager.Instance.player.foodGainUnit += 1;
                break;
            case 5: // ������ �Ǵ�(�̺�Ʈ ���� ���� �۾�)
                break;
            case 6: // �ü� �����, �ü��� ���� Ȯ�� ����
                GameManager.Instance.player.facilityFailProb -= 0.1f;
                break;
            case 7: // ����ٴ�, ���峭 ��ǰ ���� Ȯ�� ����
                GameManager.Instance.player.repairProb += 0.3f;
                break;
            case 8: //  ������ִ� ȭ��, ȣ���� ������ ���� �� ���ҷ� ����
                GameManager.Instance.player.affectionGainUnit += 1;
                GameManager.Instance.player.affectionLossUnit -= 1;
                break;
            case 9: // ���� ���� ������, �ƻ�� ���� ������ �� �� �ߵ�
                GameManager.Instance.player.survivalPro = true;
                break;
            case 10: // �ż��� ����, Ž�� �ϼ� 1�� ����
                GameManager.Instance.player.missionDays -= 1;
                break;
            default:
                break;
        }
    }

    public void FixNerf(int value)
    {
        switch (value)
        {
            case 1: // ������ ���ڸ� ,���ŷ� ���� ����
                GameManager.Instance.player.mentalLossUnit += 1;
                break;
            case 2: // �� ����, ��� Ȯ�� ���� �����
                GameManager.Instance.player.facilityFailProb += 0.1f;
                GameManager.Instance.player.repairProb -= 0.1f;
                GameManager.Instance.player.deathProb += 0.1f;
                break;
            case 3: // ��İ���, ���� �Ҹ� ����
                GameManager.Instance.player.foodLossUnit += 1;
                break;
            case 4: // �Ž����� ���� ,���ŷ��� �� ������ ����
                GameManager.Instance.player.mentalLossUnit += 3;
                break;
            case 5: // �ܱ� �����, ������ ����� �������� ���� Ȯ��
                GameManager.Instance.player.randomActingProb += 0.1f;
                break;
            case 6: // ������ ����, ���� ������ ����
                GameManager.Instance.player.repairProb -= 0.2f;
                break;
            case 7: // ������ ����, NPC���� ȣ������ õõ�� ����
                GameManager.Instance.player.goodLeader = false;
                break;
            case 8: // ���� ����, �Ҿ��� �� ���� ������ �ı�
                GameManager.Instance.player.overreaction = true;
                break;
            case 9: // ���� ����, ȣ���� ������ ���� �� ���ҷ� ����
                GameManager.Instance.player.affectionGainUnit -= 1;
                GameManager.Instance.player.affectionLossUnit += 1;
                break;
            case 10: // ��ġ(Ž�� ���� ���� �۾�)
                break;
            default:
                break;
        }
    }
}
