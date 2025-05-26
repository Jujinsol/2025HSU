/*
 * File :   NextDay.cs
 * Desc :   다음 날로 넘어갈 때
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

            // 다음 날로 증가
            if (character.job == "Player")
                GameManager.Instance.day++;

            Debug.Log(_die.RemoveDeadCharacters());

            // 30일 차 게임 종료
            if (GameManager.Instance.day >= 30)
                Debug.Log("Game end");
        }
    }

    void HandleNPCTurn(CharacterData cd, bool goodLeader)
    {
        // 호감도 감소 (리더쉽 부족 특성)
        if (!goodLeader)
            statChanger.LoseAffection(cd, 1); //5

        // 음식, 물 수치 일정 이하 시, 건강 수치 감소
        if (cd.food <= 30 && cd.water <= 30)
        {
            statChanger.LoseHealth(cd, 5); //25
        }
        else if (cd.food <= 30 ||  cd.water <= 30)
        {
            statChanger.LoseHealth(cd, 2); //10
        }

        // 아사와 갈사
        if (TryDeath(cd, cd.food <= 0, "아사")) return;
        if (TryDeath(cd, cd.water <= 0, "갈사")) return;

        float dp = GameManager.Instance.player.deathProb; //기본 0.5f

        // 정신 이상도에 따른 확률형 사망
        if (cd.mental <= 0) if (TryChanceDeath(cd, dp, "자살")) return;
        else if (cd.mental <= 30) if (TryChanceDeath(cd, dp / 5, "자살")) return;

        // 호감도에 따른 확률형 사망
        if (cd.affection <= 0) if (TryChanceDeath(cd, dp, "나를 피해 떠나감")) return;
        else if (cd.affection <= 20) if (TryChanceDeath(cd, dp / 5, "나를 피해 떠나감")) return;
        
        // 발병 여부에 따른 확률형 사망
        if (cd.isSick) if (TryChanceDeath(cd, dp, "병사")) return;

        // 건강 수치에 따른 확률형 발병
        float sp = GameManager.Instance.player.sickProb; // 기본 1f

        if (cd.health <= 0) TryChanceSick(cd, sp);
        else if (cd.health <= 30) TryChanceSick(cd, sp / 3);

    }

    void HandlePlayerTurn(CharacterData pl)
    {
        // 음식, 물 수치 일정 이하 시, 건강 수치 감소
        if (pl.food <= 30 && pl.water <= 30)
        {
            statChanger.LoseHealth(pl, 5); //25
        }
        else if (pl.food <= 30 || pl.water <= 30)
        {
            statChanger.LoseHealth(pl, 2); //10
        }

        // 아사와 갈사(위기 대응 전문가 적용)
        if (TrySurviveOrDeath(pl, pl.food <= 0, "아사", 10)) return;
        if (TrySurviveOrDeath(pl, pl.water <= 0, "갈사", 10)) return;

        float dp = GameManager.Instance.player.deathProb; //0.5f

        // 정신 이상도에 따른 확률형 사망 및 과민반응
        if (pl.mental <= 0)
        {
            if (TryChanceDeath(pl, dp, "자살")) return;
            if (GameManager.Instance.player.overreaction)
            {
                usingItem.DestroyItem(2);
                sb.AppendLine("불안증세에 잠도 제대로 못잔 것 같다. 어젯밤 눈에 보이는 물건들을 부숴버린거 같은데 기억이 희미하다.. 한번 확인해보자.");
            }
        }
        else if (pl.mental <= 30)
        {
            if (TryChanceDeath(pl, dp / 5, "자살")) return;
            if (GameManager.Instance.player.overreaction)
            {
                usingItem.DestroyItem(1);
                sb.AppendLine("불안증세에 잠도 제대로 못잔 것 같다. 어젯밤 눈에 보이는 물건을 부숴버린거 같은데 기억이 희미하다.. 한번 확인해보자.");
            }
        }
        // 발병 여부에 따른 확률형 사망
        if (pl.isSick) if (TryChanceDeath(pl, dp, "병사")) return;

        // 건강 수치에 따른 확률형 발병
        float sp = GameManager.Instance.player.sickProb; // 기본 1f

        if (pl.health <= 0) TryChanceSick(pl, sp);
        else if (pl.health <= 30) TryChanceSick(pl, sp / 3);

        // 전력 소모 및 질식사(전원 사망)
        statChanger.LoseElectricity(pl, 1); //5
        if (pl.electricity <= 0)
            GameManager.Instance.characterList.ForEach(c =>
            {
                if (c != null)
                {
                    sb.AppendLine((GetDeathMessage(c.job, "질식사")));
                    c.isAlive = false;
                    c.deathReason = "질식사";
                }
            });
    }


    // 즉시 사망 처리
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

    // 확률 기반 사망 처리
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

    // 위기 대응 전문가용 사망 처리(아사, 갈사에 적용)
    bool TrySurviveOrDeath(CharacterData c, bool condition, string reason, int reviveValue)
    {
        if (!condition) return false;

        if (c.survivalPro)
        {
            sb.AppendLine("정확히 기억나진 않지만 어젯밤 죽음의 고비를 넘긴 것은 확실하다. 옛날에도 이런 극한의 상황을 겪어봤었지.. 하지만 다음은 없다. 좀 더 몸관리에 신경쓰자.");
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
            if (reason == "아사") return "이런.. 의사가 굶어서 결국 죽고 말았어.\n슬픈 하루의 시작이야.";
            if (reason == "갈사") return "의사는 침실에서 다시는 움직이지 못했어.\n그녀의 건조해진 시체는 너무나도 가벼웠지...";
            if (reason == "자살") return "의사가 보이지 않아 아침부터 기지 내를 뒤져봤어.\n그리고 스스로 삶을 마무리한 모습을 봐버렸지..";
            if (reason == "병사") return "아침에 들려야 할 의사의 기침 소리가 들리지 않았어..\n병이 나은거였다면 행복한 하루일텐데 오늘은 끔찍한 하루야";
            if (reason == "나를 피해 떠나감") return "의사와 그녀의 짐이 사라졌어.\n나를 경멸하던 그녀는 내가 리더인 이 곳이 싫었나봐...\n도대체 어디로 간걸까?";           
        }
        else if (job == "Guard")
        {
            if (reason == "아사") return "경비원이 주린 배를 안고 일어나지 못했어.\n처음 봤을 때의 강한 그녀의 모습이 무색해진다...";
            if (reason == "갈사") return "경비원이 탈수한 상태로 쓰러져 있었어.\n이미 그녀는 차가워진 후였지...";
            if (reason == "자살") return "모두가 잠든 밤에 총성이 들렸어.\n최근 힘들어보이던 경비원은 자신을 지켜주던 마지막 총알을 자신의 몸에 쏴버렸지..";
            if (reason == "병사") return "경비원은 병세를 숨기고 우리를 지키는데 힘써왔어.\n하지만 어젯밤 결국 자신을 지키지 못했어.. 그녀를 위해 기도해야겠어.";
            if (reason == "나를 피해 떠나감") return "경비원과 그녀의 짐이 사라졌어.\n나를 경멸하던 그녀는 내가 리더인 이 곳이 싫었나봐...\n도대체 어디로 간걸까?";
        }
        else if (job == "Engineer")
        {
            if (reason == "아사") return "정비사는 무거운 도구만 남기고 조용히 쓰러졌어.\n아무도 몰랐지.";
            if (reason == "갈사") return "정비사의 마른 기침이 오늘 아침엔 들리지 않았어.\n그리고 한 없이 말라비틀어진 그의 마지막을 목격해버렸지..";
            if (reason == "자살") return "항상 미소를 잃지 않고자 하는 정비사의 표정이 최근들어 좋지 않았어.\n그때 깨달았어야 하는데... 그를 위해 기도하자.";
            if (reason == "병사") return "정비사는 그렇게 수많은 것들을 고쳐왔지만 자신의 몸은 결국 고치지 못했어.\n내가 고쳐줬어야 하는데..";
            if (reason == "나를 피해 떠나감") return "정비사와 그의 짐이 사라졌어.\n나를 경멸하던 그는 내가 리더인 이 곳이 싫었나봐...\n도대체 어디로 간걸까?";
        }
        else if (job == "Player")
        {
            if (reason == "아사") return "배에서 꼬르륵 소리가 난지도 며칠이 지났을까.\n난 이제 내 힘으로 움직이지 못해... 나는 오늘을 넘길 수 있을까?";
            if (reason == "갈사") return "목이 너무 마르다. 기지 벽을 부숴서 바닷물이라도 마시고 싶어...\n물...물이 필요해...";
            if (reason == "자살") return "도대체 이 거지같은 상황은 언제 끝나는 걸까?\n한정된 자원에 누가 구해줄 지도 확신할 수 없는 상황.\n이렇게 죽어갈 바에 스스로 내 마지막 순간을 결정하는 게 나을 거 같아.";
            if (reason == "병사") return "어제까진 참을만 했는데 오늘 밤은 너무 힘들다.\n어떠한 욕구도 없어지고 움직일 힘도 없다.\n그저 살고 싶다는 생각뿐...";
            if (reason == "질식사") return "이제 전기가 부족한거 같아.\n기지 내에 불은 하나씩 꺼져가고 산소 공급 장치의 소리도 점점 작아져가.\n점점 어지럽고 잠을 자야만 할 거 같아...";
        }
        return "";
    }
    // 스탯 0~100 유지
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
