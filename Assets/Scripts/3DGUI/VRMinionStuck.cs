using UnityEngine;
using System.Collections;

public class VRMinionStuck : MonoBehaviour {

    public UILabel label;

    void Update()
    {
        label.text = "Minions Stuck: " + StopDetectionScript.minionStopCount.ToString();

    }
}
