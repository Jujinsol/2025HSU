using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public float timeLeft = 60f; // 타이머 시작 시간 (초)
    public TextMeshProUGUI timerText; // TextMeshPro 사용 시
    // public Text timerText; // 일반 Text 사용 시

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeLeft = 0;
            UpdateTimerUI();
            // 시간 끝났을 때 로직 (예: 게임 종료)
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}