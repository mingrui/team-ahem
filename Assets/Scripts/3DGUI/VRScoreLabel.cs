using UnityEngine;
using System.Collections;

public class VRScoreLabel : MonoBehaviour {

    public UILabel label;

    void Update()
    {
        label.text = "Score: " + StopDetectionScript.score.ToString();

    }
}
