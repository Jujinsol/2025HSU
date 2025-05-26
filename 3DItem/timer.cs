using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public float timeLeft = 60f; // Ÿ�̸� ���� �ð� (��)
    public TextMeshProUGUI timerText; // TextMeshPro ��� ��
    // public Text timerText; // �Ϲ� Text ��� ��

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
            // �ð� ������ �� ���� (��: ���� ����)
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}