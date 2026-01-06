using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool running = false;

    void Update()
    {
        if (!running) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }
}
