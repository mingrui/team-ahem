using UnityEngine;
using System.Collections;

public class TileMakeScript : MonoBehaviour {

	public GameObject grassTile;
	public GameObject hillTile;
	public GameObject waterTile;
	public GameObject purpleTile;
	public GameObject minion;
	public GameObject[,] tileArray = new GameObject[90,30];
	public int minionOnBoard = 0;
	float cornerX = 0;
	float cornerZ = 0;
	float timeCounter = 0;
	Vector3 dirRight = new Vector3 (1, 0, 0);
	
	// Use this for initialization
	void Start () {
		
		cornerX = transform.position.x;
		cornerZ = transform.position.z;
		
		for (float x = 0f; x < 90f; x++) {
			for (float z = 0f; z <30f; z++) {
				tileArray[(int)x,(int)z] = Instantiate (grassTile, new Vector3 (cornerX + x, 1f , cornerZ + z), Quaternion.identity) as GameObject;
			}
		}
		
		
		for (int k = 0; k < 200; k++) {
			int mountX = Random.Range (1, 90);
			int mountZ = Random.Range (0, 30);
			Destroy (tileArray [mountX, mountZ]);
			tileArray [mountX, mountZ] = Instantiate (hillTile, new Vector3 (cornerX + mountX, 1f, cornerZ + mountZ), Quaternion.identity) as GameObject;
		}

		for (int l = 0; l < 300; l++) {
			int waterX = Random.Range (1, 90);
			int waterZ = Random.Range (0, 30);
			Destroy (tileArray [waterX, waterZ]);
			tileArray [waterX, waterZ] = Instantiate (waterTile, new Vector3 (cornerX + waterX, 1f, cornerZ + waterZ), Quaternion.identity) as GameObject;
		}

		for (int m = 0; m < 30; m++) {
			int purpleX = Random.Range (10, 90);
			int purpleZ = Random.Range (0, 30);
			Destroy (tileArray [purpleX, purpleZ]);
			tileArray [purpleX, purpleZ] = Instantiate (purpleTile, new Vector3 (cornerX + purpleX, 1f, cornerZ + purpleZ), Quaternion.identity) as GameObject;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (timeCounter > 1 && minionOnBoard < 30) {
			
			int spawnPosX = 0;
			int spawnPosZ = Random.Range (0, 30);
			GameObject new_minon = Instantiate (minion, new Vector3 (cornerX + spawnPosX, 2f, cornerZ + spawnPosZ), Quaternion.identity) as GameObject;
			new_minon.transform.Rotate (0f, 0f, 180f);
			new_minon.rigidbody.velocity = dirRight * 5f;
			timeCounter = 0;
			minionOnBoard++;
		}
		timeCounter += Time.deltaTime;
	}
}
