/*
 * File :   BuffAndNerf.cs
 * Desc :   버프와 너프 선택에 따른 포인트 값 반환 및 수치 조정
 */

using UnityEngine;

public class BuffAndNerf
{
    public int BuffPoint(int value)
    {
        switch (value)
        {
            case 1: // 심리학자
                return 4;
            case 2: // 약사
                return 6;
            case 3: // 연구소장
                return 4;
            case 4: // 요리사
                return 8;
            case 5: // 직감적 판단
                return 10;
            case 6: // 시설 기술자
                return 6;
            case 7: // 다재다능
                return 6;
            case 8: //  설득력있는 화법
                return 4;
            case 9: // 위기 대응 전문가
                return 6;
            case 10: // 신속한 반응
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
            case 1: // 불편한 잠자리
                return 4;
            case 2: // 운 나쁨
                return 8;
            case 3: // 대식가들
                return 8;
            case 4: // 거슬리는 소음
                return 6;
            case 5: // 단기 기억상실
                return 10;
            case 6: // 손재주 부족
                return 4;
            case 7: // 리더십 부족
                return 4;
            case 8: // 과민 반응
                return 4;
            case 9: // NPC와 호감도 상승량 감소
                return 4;
            case 10: // 길치
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
            case 1: // 심리학자, 호감도 감소율 감소
                GameManager.Instance.player.affectionLossUnit -= 2;
                break;
            case 2: // 약사, 음식과 물로 약 제조 가능
                GameManager.Instance.player.pharmacist = true;
                break;
            case 3: // 연구소장, 호감도가 높은 상태로 시작
                foreach (CharacterData character in GameManager.Instance.characterList)
                {
                    if (character != null && character.job != "Player")
                    {
                        character.affection += 30;
                    }
                }
                break;
            case 4: // 요리사, 식량 배급에 따른 만족도 증가
                GameManager.Instance.player.foodGainUnit += 1;
                break;
            case 5: // 직감적 판단(이벤트 구현 이후 작업)
                break;
            case 6: // 시설 기술자, 시설물 고장 확률 감소
                GameManager.Instance.player.facilityFailProb -= 0.1f;
                break;
            case 7: // 다재다능, 고장난 물품 수리 확률 증가
                GameManager.Instance.player.repairProb += 0.3f;
                break;
            case 8: //  설득력있는 화법, 호감도 증가량 증가 및 감소량 감소
                GameManager.Instance.player.affectionGainUnit += 1;
                GameManager.Instance.player.affectionLossUnit -= 1;
                break;
            case 9: // 위기 대응 전문가, 아사로 인한 죽음을 한 번 견딤
                GameManager.Instance.player.survivalPro = true;
                break;
            case 10: // 신속한 반응, 탐사 일수 1일 감소
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
            case 1: // 불편한 잠자리 ,정신력 빨리 감소
                GameManager.Instance.player.mentalLossUnit += 1;
                break;
            case 2: // 운 나쁨, 모든 확률 관련 디버프
                GameManager.Instance.player.facilityFailProb += 0.1f;
                GameManager.Instance.player.repairProb -= 0.1f;
                GameManager.Instance.player.deathProb += 0.1f;
                break;
            case 3: // 대식가들, 음식 소모량 증가
                GameManager.Instance.player.foodLossUnit += 1;
                break;
            case 4: // 거슬리는 소음 ,정신력을 더 빠르게 감소
                GameManager.Instance.player.mentalLossUnit += 3;
                break;
            case 5: // 단기 기억상실, 지정된 명령을 수행하지 않을 확률
                GameManager.Instance.player.randomActingProb += 0.1f;
                break;
            case 6: // 손재주 부족, 수리 성공률 감소
                GameManager.Instance.player.repairProb -= 0.2f;
                break;
            case 7: // 리더십 부족, NPC들의 호감도가 천천히 감소
                GameManager.Instance.player.goodLeader = false;
                break;
            case 8: // 과민 반응, 불안할 시 랜덤 아이템 파괴
                GameManager.Instance.player.overreaction = true;
                break;
            case 9: // 고립된 성격, 호감도 증가량 감소 및 감소량 증가
                GameManager.Instance.player.affectionGainUnit -= 1;
                GameManager.Instance.player.affectionLossUnit += 1;
                break;
            case 10: // 길치(탐사 구현 이후 작업)
                break;
            default:
                break;
        }
    }
}
