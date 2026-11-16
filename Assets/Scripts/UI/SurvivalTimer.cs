using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    [SerializeField] private Text label;
    private float start;
    private int lastCentisecond;

    public void Begin()
    {
        start = Time.time;
        lastCentisecond = -1;
        enabled = true;
        Update();
    }

    public void Stop()
    {
        Update();
        enabled = false;
    }

    private void Update()
    {
        int centiseconds = Mathf.FloorToInt((Time.time - start) * 100);
        if (centiseconds == lastCentisecond) return;

        lastCentisecond = centiseconds;
        label.text = $"{centiseconds / 6000:00}:{centiseconds / 100 % 60:00}.{centiseconds % 100:00}";
    }
}
