﻿using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GlobalConst : MonoBehaviour {
	
	public enum MinionType {Melee, Range, Defensive};
	public enum Team {Yellow, Purple};
	public enum Direction {
		Left, Up, Right, Down
	};
	public static int[,] unitMap;
	public static bool onMove = false;
	public static GameObject selectedObj;
	// unitMap to indicate unit grid position, 0 means no unit on that tile
	// 1 means yellow minion, 2 means purple minion

	// Use this for initialization
	void Start () {
		unitMap = new int[20, 20];
		for (int i = 0; i < 20; ++i) {
			for (int j = 0; j < 20; ++j) {
				unitMap[i, j] = 0; 
			}
		}

		// left corner is 0, 0 and top right corner is 19, 19
		unitMap[0, 0] = 1;
		unitMap[0, 1] = 1;
		unitMap[0, 2] = 1;
		unitMap[19, 17] = 2;
		unitMap[19, 18] = 2;
		unitMap[19, 19] = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void Set_Position(Vector3 pos1, Vector3 pos2){
		pos1.x = pos2.x;
		pos1.z = pos2.z;
	}

	public static KeyCode camera_switch_key = KeyCode.Alpha1;
}

public class Constants
{
	public const float cMaxRayCastDistance = 1000.0f;
}
