using UnityEngine;
using System.Collections;

public class VRTimer : MonoBehaviour {
    public UILabel label;

    void Update()
    {
        float timeR = (int)(TimerScript.timeRemaining);
        if (timeR > 0)
        {
            label.text = "Time Remaining: " + timeR.ToString();
        }
        else
        {
            label.text = "Times Up ";
            Time.timeScale = 0;
        }

    }
}
