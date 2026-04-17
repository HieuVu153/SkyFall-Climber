using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float time;
    public bool isPlaying;

    void Update()
    {
        if (isPlaying)
        {
            time += Time.unscaledDeltaTime; // ✅ chuẩn nhất
        }
    }

    public int GetTime()
    {
        return Mathf.FloorToInt(time);
    }

    public void SetTime(int value)
    {
        time = value;
    }

    public void ResetTime()
    {
        time = 0f;
    }

    public void StartTimer()
    {
        isPlaying = true;
    }

    public void StopTimer()
    {
        isPlaying = false;
    }
}