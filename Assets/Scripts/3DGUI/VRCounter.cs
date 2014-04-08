using UnityEngine;
using System.Collections;

public class VRCounter : MonoBehaviour {

    public UILabel label;

    void Update()
    {
        label.text = "Minions Saved: " + MinionHomeScript.minionCount.ToString();

    }
}
