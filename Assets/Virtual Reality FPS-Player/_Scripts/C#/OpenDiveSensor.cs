using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


//OpenDive HEad Tracking for Unity by Stefan Welker


public class OpenDiveSensor : MonoBehaviour {
	
  #if UNITY_ANDROID

	[DllImport("nativesensors")]	private static extern void initialize_sensors();
	[DllImport("nativesensors")]	private static extern float get_q0();
	[DllImport("nativesensors")]	private static extern float get_q1();
	[DllImport("nativesensors")]	private static extern float get_q2();
	[DllImport("nativesensors")]	private static extern float get_q3();
	
   #endif	
	
	public static bool reset = false;	
	
	void Start () 
	{	
  		#if UNITY_ANDROID
			initialize_sensors();		
		#endif
	}
	
	
	void Update () 
	{		
		#if UNITY_ANDROID
		Quaternion rot;
		rot.x = -get_q2();	//-2	
		rot.y =  get_q3();	// 3
		rot.z = -get_q1();	//-1
		rot.w =  get_q0();	// 0
		
		transform.rotation = rot;											
		transform.localEulerAngles = rot.eulerAngles;
		#endif
		
	}
	
	void OnGUI ()
	{
	}
}
