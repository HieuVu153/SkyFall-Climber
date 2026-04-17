using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float time;
    public bool isPlaying;

    // ================== UPDATE ==================
    void Update()
    {
        if (isPlaying)
        {
            time += Time.deltaTime;
        }
    }

    // ================== GET ==================
    public int GetTime()
    {
        return Mathf.FloorToInt(time);
    }

    // ================== SET (load từ PlayFab) ==================
    public void SetTime(int value)
    {
        time = value;
    }

    // ================== RESET ==================
    public void ResetTime()
    {
        time = 0f;
    }

    // ================== START / STOP ==================
    public void StartTimer()
    {
        isPlaying = true;
    }

    public void StopTimer()
    {
        isPlaying = false;
    }
}