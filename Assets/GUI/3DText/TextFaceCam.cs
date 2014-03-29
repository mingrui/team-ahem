using UnityEngine;
using System.Collections;

public class TextFaceCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(-1, 1, 1);
        renderer.sortingLayerID = -1;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
	}
}
