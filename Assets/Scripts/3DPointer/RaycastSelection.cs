using UnityEngine;
using System.Collections;

public class RaycastSelection : MonoBehaviour {

	// selected GameObject
	private GameObject mSelectedObject;
	private LineRenderer lineOne;

	// Use this for initialization
	void Start () {
		lineOne = GetComponent<LineRenderer>();
		lineOne.enabled = false;
		lineOne.SetVertexCount(2);
		lineOne.SetWidth(0.1f, 0.25f);
	}
	
	// Update is called once per frame
	void Update () {
		// process object selection
		//if (Input.GetMouseButtonDown(0))
		//{
			SelectObjectByMousePos();
		//}
	}

	private void SelectObjectByMousePos()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Constants.cMaxRayCastDistance))
		{
			// get game object
			GameObject rayCastedGO = hit.collider.gameObject;
			
			// select object
			this.SelectedObject = rayCastedGO;

			FireLasers(hit);
		}
	}

	public GameObject SelectedObject
	{
		get
		{
			return mSelectedObject;
		}
		set
		{
			// get old game object
			GameObject goOld = mSelectedObject;
			
			// assign new game object
			mSelectedObject = value;
			
			// if this object is the same - just not process this
			if (goOld == mSelectedObject)
			{
				return;
			}
			
			// set material to non-selected object
			if (goOld != null)
			{
				//goOld.renderer.material = SimpleMat;
			}
			
			// set material to selected object
			if (mSelectedObject != null)
			{
				//mSelectedObject.renderer.material = HighlightedMat;
			}
		}
	}

	private void FireLasers(RaycastHit hitOne){
		lineOne.enabled = true;
		lineOne.SetPosition(0, transform.position);
		lineOne.SetPosition(1, hitOne.point);
		Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
		lineOne.material = whiteDiffuseMat;
	}
}
