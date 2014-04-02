using UnityEngine;
using UnityEngine;
using System.Collections;

public class TileMakeScript : MonoBehaviour {
	
	
	public GameObject grassTile;
	public GameObject hillTile;
	public GameObject waterTile;
	public GameObject purpleTile;
	public GameObject[,] tileArray = new GameObject[90,30];
	private float cornerX = 0;
	private float cornerZ = 0;
	
	// Use this for initialization
	void Start () {
		
		cornerX = transform.position.x;
		cornerZ = transform.position.z;
		
		for (float x = 0f; x < 90f; x++) {
			for (float z = 0f; z <30f; z++) {
				tileArray[(int)x,(int)z] = Instantiate (grassTile, new Vector3 (cornerX + x, 1.701241f , cornerZ + z), Quaternion.identity) as GameObject;
			}
		}
		
		
		for (int k = 0; k < 200; k++) {
			int mountX = Random.Range (0, 90);
			int mountZ = Random.Range (0, 30);
			Destroy (tileArray [mountX, mountZ]);
			tileArray [mountX, mountZ] = Instantiate (hillTile, new Vector3 (cornerX + mountX, 1.701241f, cornerZ + mountZ), Quaternion.identity) as GameObject;
		}

		for (int l = 0; l < 300; l++) {
			int waterX = Random.Range (0, 90);
			int waterZ = Random.Range (0, 30);
			Destroy (tileArray [waterX, waterZ]);
			tileArray [waterX, waterZ] = Instantiate (waterTile, new Vector3 (cornerX + waterX, 1.701241f, cornerZ + waterZ), Quaternion.identity) as GameObject;
		}

		for (int m = 0; m < 30; m++) {
			int purpleX = Random.Range (0, 90);
			int purpleZ = Random.Range (0, 30);
			Destroy (tileArray [purpleX, purpleZ]);
			tileArray [purpleX, purpleZ] = Instantiate (purpleTile, new Vector3 (cornerX + purpleX, 1.701241f, cornerZ + purpleZ), Quaternion.identity) as GameObject;
		}
		
		
		
		//////////////////////////
		/*Destroy (tileArray [5, 5]); 
		Destroy (tileArray [5, 4]); 
		Destroy (tileArray [5, 3]); 
		Destroy (tileArray [5, 2]); 
		tileArray[5,5] = Instantiate (waterTile, new Vector3 (cornerX + 5, 1.701241f , cornerZ + 5), Quaternion.identity) as GameObject;
		tileArray[5,4] = Instantiate (waterTile, new Vector3 (cornerX + 5, 1.701241f , cornerZ + 4), Quaternion.identity) as GameObject;
		tileArray[5,3] = Instantiate (waterTile, new Vector3 (cornerX + 5, 1.701241f , cornerZ + 3), Quaternion.identity) as GameObject;
		tileArray[5,2] = Instantiate (waterTile, new Vector3 (cornerX + 5, 1.701241f , cornerZ + 2), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [6, 6]);
		Destroy (tileArray [6, 5]);
		Destroy (tileArray [6, 4]);
		Destroy (tileArray [6, 3]);
		Destroy (tileArray [6, 2]);
		Destroy (tileArray [6, 1]);
		tileArray[6,6] = Instantiate (waterTile, new Vector3 (cornerX + 6, 1.701241f , cornerZ + 6), Quaternion.identity) as GameObject;
		tileArray[6,5] = Instantiate (waterTile, new Vector3 (cornerX + 6, 1.701241f , cornerZ + 5), Quaternion.identity) as GameObject;
		tileArray[6,4] = Instantiate (waterTile, new Vector3 (cornerX + 6, 1.701241f , cornerZ + 4), Quaternion.identity) as GameObject;
		tileArray[6,3] = Instantiate (waterTile, new Vector3 (cornerX + 6, 1.701241f , cornerZ + 3), Quaternion.identity) as GameObject;
		tileArray[6,2] = Instantiate (waterTile, new Vector3 (cornerX + 6, 1.701241f , cornerZ + 2), Quaternion.identity) as GameObject;
		tileArray[6,1] = Instantiate (waterTile, new Vector3 (cornerX + 6, 1.701241f , cornerZ + 1), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [7, 7]);
		Destroy (tileArray [7, 6]);
		Destroy (tileArray [7, 5]);
		Destroy (tileArray [7, 4]);
		Destroy (tileArray [7, 3]);
		Destroy (tileArray [7, 2]);
		Destroy (tileArray [7, 1]);
		tileArray[7,7] = Instantiate (waterTile, new Vector3 (cornerX + 7, 1.701241f , cornerZ + 7), Quaternion.identity) as GameObject;
		tileArray[7,6] = Instantiate (waterTile, new Vector3 (cornerX + 7, 1.701241f , cornerZ + 6), Quaternion.identity) as GameObject;
		tileArray[7,5] = Instantiate (waterTile, new Vector3 (cornerX + 7, 1.701241f , cornerZ + 5), Quaternion.identity) as GameObject;
		tileArray[7,4] = Instantiate (waterTile, new Vector3 (cornerX + 7, 1.701241f , cornerZ + 4), Quaternion.identity) as GameObject;
		tileArray[7,3] = Instantiate (waterTile, new Vector3 (cornerX + 7, 1.701241f , cornerZ + 3), Quaternion.identity) as GameObject;
		tileArray[7,2] = Instantiate (waterTile, new Vector3 (cornerX + 7, 1.701241f , cornerZ + 2), Quaternion.identity) as GameObject;
		tileArray[7,1] = Instantiate (waterTile, new Vector3 (cornerX + 7, 1.701241f , cornerZ + 1), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [8, 7]);
		Destroy (tileArray [8, 6]);
		Destroy (tileArray [8, 5]);
		Destroy (tileArray [8, 4]);
		Destroy (tileArray [8, 3]);
		Destroy (tileArray [8, 2]);
		Destroy (tileArray [8, 1]);
		tileArray[8,7] = Instantiate (waterTile, new Vector3 (cornerX + 8, 1.701241f , cornerZ + 7), Quaternion.identity) as GameObject;
		tileArray[8,6] = Instantiate (waterTile, new Vector3 (cornerX + 8, 1.701241f , cornerZ + 6), Quaternion.identity) as GameObject;
		tileArray[8,5] = Instantiate (waterTile, new Vector3 (cornerX + 8, 1.701241f , cornerZ + 5), Quaternion.identity) as GameObject;
		tileArray[8,4] = Instantiate (waterTile, new Vector3 (cornerX + 8, 1.701241f , cornerZ + 4), Quaternion.identity) as GameObject;
		tileArray[8,3] = Instantiate (waterTile, new Vector3 (cornerX + 8, 1.701241f , cornerZ + 3), Quaternion.identity) as GameObject;
		tileArray[8,2] = Instantiate (waterTile, new Vector3 (cornerX + 8, 1.701241f , cornerZ + 2), Quaternion.identity) as GameObject;
		tileArray[8,1] = Instantiate (waterTile, new Vector3 (cornerX + 8, 1.701241f , cornerZ + 1), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [9, 7]);
		Destroy (tileArray [9, 6]);
		Destroy (tileArray [9, 5]);
		Destroy (tileArray [9, 4]);
		Destroy (tileArray [9, 3]);
		Destroy (tileArray [9, 2]);
		Destroy (tileArray [9, 1]);
		tileArray[9,7] = Instantiate (waterTile, new Vector3 (cornerX + 9, 1.701241f , cornerZ + 7), Quaternion.identity) as GameObject;
		tileArray[9,6] = Instantiate (waterTile, new Vector3 (cornerX + 9, 1.701241f , cornerZ + 6), Quaternion.identity) as GameObject;
		tileArray[9,5] = Instantiate (waterTile, new Vector3 (cornerX + 9, 1.701241f , cornerZ + 5), Quaternion.identity) as GameObject;
		tileArray[9,4] = Instantiate (waterTile, new Vector3 (cornerX + 9, 1.701241f , cornerZ + 4), Quaternion.identity) as GameObject;
		tileArray[9,3] = Instantiate (waterTile, new Vector3 (cornerX + 9, 1.701241f , cornerZ + 3), Quaternion.identity) as GameObject;
		tileArray[9,2] = Instantiate (waterTile, new Vector3 (cornerX + 9, 1.701241f , cornerZ + 2), Quaternion.identity) as GameObject;
		tileArray[9,1] = Instantiate (waterTile, new Vector3 (cornerX + 9, 1.701241f , cornerZ + 1), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [10, 6]);
		Destroy (tileArray [10, 5]);
		Destroy (tileArray [10, 4]);
		Destroy (tileArray [10, 3]);
		Destroy (tileArray [10, 2]);
		tileArray[10,6] = Instantiate (waterTile, new Vector3 (cornerX + 10, 1.701241f , cornerZ + 6), Quaternion.identity) as GameObject;
		tileArray[10,5] = Instantiate (waterTile, new Vector3 (cornerX + 10, 1.701241f , cornerZ + 5), Quaternion.identity) as GameObject;
		tileArray[10,4] = Instantiate (waterTile, new Vector3 (cornerX + 10, 1.701241f , cornerZ + 4), Quaternion.identity) as GameObject;
		tileArray[10,3] = Instantiate (waterTile, new Vector3 (cornerX + 10, 1.701241f , cornerZ + 3), Quaternion.identity) as GameObject;
		tileArray[10,2] = Instantiate (waterTile, new Vector3 (cornerX + 10, 1.701241f , cornerZ + 2), Quaternion.identity) as GameObject;
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		
		
		
		//////////////////////////
		Destroy (tileArray [5+3, 5+9]); 
		Destroy (tileArray [5+3, 4+9]); 
		Destroy (tileArray [5+3, 3+9]); 
		Destroy (tileArray [5+3, 2+9]); 
		tileArray[5+3,5+9] = Instantiate (waterTile, new Vector3 (cornerX + 5+3, 1.701241f , cornerZ + 5+9), Quaternion.identity) as GameObject;
		tileArray[5+3,4+9] = Instantiate (waterTile, new Vector3 (cornerX + 5+3, 1.701241f , cornerZ + 4+9), Quaternion.identity) as GameObject;
		tileArray[5+3,3+9] = Instantiate (waterTile, new Vector3 (cornerX + 5+3, 1.701241f , cornerZ + 3+9), Quaternion.identity) as GameObject;
		tileArray[5+3,2+9] = Instantiate (waterTile, new Vector3 (cornerX + 5+3, 1.701241f , cornerZ + 2+9), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [6+3, 6+9]);
		Destroy (tileArray [6+3, 5+9]);
		Destroy (tileArray [6+3, 4+9]);
		Destroy (tileArray [6+3, 3+9]);
		Destroy (tileArray [6+3, 2+9]);
		Destroy (tileArray [6+3, 1+9]);
		tileArray[6+3,6+9] = Instantiate (waterTile, new Vector3 (cornerX + 6+3, 1.701241f , cornerZ + 6+9), Quaternion.identity) as GameObject;
		tileArray[6+3,5+9] = Instantiate (waterTile, new Vector3 (cornerX + 6+3, 1.701241f , cornerZ + 5+9), Quaternion.identity) as GameObject;
		tileArray[6+3,4+9] = Instantiate (waterTile, new Vector3 (cornerX + 6+3, 1.701241f , cornerZ + 4+9), Quaternion.identity) as GameObject;
		tileArray[6+3,3+9] = Instantiate (waterTile, new Vector3 (cornerX + 6+3, 1.701241f , cornerZ + 3+9), Quaternion.identity) as GameObject;
		tileArray[6+3,2+9] = Instantiate (waterTile, new Vector3 (cornerX + 6+3, 1.701241f , cornerZ + 2+9), Quaternion.identity) as GameObject;
		tileArray[6+3,1+9] = Instantiate (waterTile, new Vector3 (cornerX + 6+3, 1.701241f , cornerZ + 1+9), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [7+3, 9+9]);
		Destroy (tileArray [7+3, 8+9]);
		Destroy (tileArray [7+3, 7+9]);
		Destroy (tileArray [7+3, 6+9]);
		Destroy (tileArray [7+3, 5+9]);
		Destroy (tileArray [7+3, 4+9]);
		Destroy (tileArray [7+3, 3+9]);
		Destroy (tileArray [7+3, 2+9]);
		Destroy (tileArray [7+3, 1+9]);
		tileArray[7+3,9+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 9+9), Quaternion.identity) as GameObject;
		tileArray[7+3,8+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 8+9), Quaternion.identity) as GameObject;
		tileArray[7+3,7+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 7+9), Quaternion.identity) as GameObject;
		tileArray[7+3,6+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 6+9), Quaternion.identity) as GameObject;
		tileArray[7+3,5+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 5+9), Quaternion.identity) as GameObject;
		tileArray[7+3,4+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 4+9), Quaternion.identity) as GameObject;
		tileArray[7+3,3+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 3+9), Quaternion.identity) as GameObject;
		tileArray[7+3,2+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 2+9), Quaternion.identity) as GameObject;
		tileArray[7+3,1+9] = Instantiate (waterTile, new Vector3 (cornerX + 7+3, 1.701241f , cornerZ + 1+9), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [8+3, 9+9]);
		Destroy (tileArray [8+3, 8+9]);
		Destroy (tileArray [8+3, 7+9]);
		Destroy (tileArray [8+3, 6+9]);
		Destroy (tileArray [8+3, 5+9]);
		Destroy (tileArray [8+3, 4+9]);
		Destroy (tileArray [8+3, 3+9]);
		Destroy (tileArray [8+3, 2+9]);
		Destroy (tileArray [8+3, 1+9]);
		tileArray[8+3,9+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 9+9), Quaternion.identity) as GameObject;
		tileArray[8+3,8+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 8+9), Quaternion.identity) as GameObject;
		tileArray[8+3,7+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 7+9), Quaternion.identity) as GameObject;
		tileArray[8+3,6+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 6+9), Quaternion.identity) as GameObject;
		tileArray[8+3,5+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 5+9), Quaternion.identity) as GameObject;
		tileArray[8+3,4+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 4+9), Quaternion.identity) as GameObject;
		tileArray[8+3,3+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 3+9), Quaternion.identity) as GameObject;
		tileArray[8+3,2+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 2+9), Quaternion.identity) as GameObject;
		tileArray[8+3,1+9] = Instantiate (waterTile, new Vector3 (cornerX + 8+3, 1.701241f , cornerZ + 1+9), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [9+3, 8+9]);
		Destroy (tileArray [9+3, 7+9]);
		Destroy (tileArray [9+3, 6+9]);
		Destroy (tileArray [9+3, 5+9]);
		Destroy (tileArray [9+3, 4+9]);
		Destroy (tileArray [9+3, 3+9]);
		Destroy (tileArray [9+3, 2+9]);
		Destroy (tileArray [9+3, 1+9]);
		tileArray[9+3,8+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 8+9), Quaternion.identity) as GameObject;
		tileArray[9+3,7+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 7+9), Quaternion.identity) as GameObject;
		tileArray[9+3,6+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 6+9), Quaternion.identity) as GameObject;
		tileArray[9+3,5+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 5+9), Quaternion.identity) as GameObject;
		tileArray[9+3,4+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 4+9), Quaternion.identity) as GameObject;
		tileArray[9+3,3+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 3+9), Quaternion.identity) as GameObject;
		tileArray[9+3,2+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 2+9), Quaternion.identity) as GameObject;
		tileArray[9+3,1+9] = Instantiate (waterTile, new Vector3 (cornerX + 9+3, 1.701241f , cornerZ + 1+9), Quaternion.identity) as GameObject;
		
		Destroy (tileArray [10+3, 6+9]);
		Destroy (tileArray [10+3, 5+9]);
		Destroy (tileArray [10+3, 4+9]);
		Destroy (tileArray [10+3, 3+9]);
		Destroy (tileArray [10+3, 2+9]);
		tileArray[10+3,6+9] = Instantiate (waterTile, new Vector3 (cornerX + 10+3, 1.701241f , cornerZ + 6+9), Quaternion.identity) as GameObject;
		tileArray[10+3,5+9] = Instantiate (waterTile, new Vector3 (cornerX + 10+3, 1.701241f , cornerZ + 5+9), Quaternion.identity) as GameObject;
		tileArray[10+3,4+9] = Instantiate (waterTile, new Vector3 (cornerX + 10+3, 1.701241f , cornerZ + 4+9), Quaternion.identity) as GameObject;
		tileArray[10+3,3+9] = Instantiate (waterTile, new Vector3 (cornerX + 10+3, 1.701241f , cornerZ + 3+9), Quaternion.identity) as GameObject;
		tileArray[10+3,2+9] = Instantiate (waterTile, new Vector3 (cornerX + 10+3, 1.701241f , cornerZ + 2+9), Quaternion.identity) as GameObject;*/
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
	}
}
