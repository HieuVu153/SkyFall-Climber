using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float time;
    public bool isPlaying;

    void Update()
    {
        if (isPlaying)
        {
            time += Time.deltaTime;
        }
    }

    public int GetTime()
    {
        return Mathf.FloorToInt(time);
    }

    public void ResetTime()
    {
        time = 0;
    }
}