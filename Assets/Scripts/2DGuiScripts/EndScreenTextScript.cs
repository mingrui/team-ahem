using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class EndScreenTextScript : MonoBehaviour {

	private bool gameEnd = false;
	private String curHighScoreString;

	// Use this for initialization
	void Start () {
		guiText.text = null;
	}
	
	// Update is called once per frame
	void Update () {

		if (gameEnd == false) {
			guiText.text = null;
			if (TimerScript.timeRemaining > 1) {
				guiText.text = null;
			} else {
				FileInfo theSourceFile = new FileInfo (Application.dataPath + "/highScore.txt");
				StreamReader reader = theSourceFile.OpenText();
				curHighScoreString = reader.ReadLine();
				int curHighScore = Convert.ToInt32(curHighScoreString);
				reader.Close();
				Debug.Log("cur high score " + curHighScore);
				Debug.Log("cur score " + StopDetectionScript.score);
				if (StopDetectionScript.score >= curHighScore) {
					curHighScoreString = StopDetectionScript.score.ToString();
					StreamWriter writer = new StreamWriter(Application.dataPath + "/highScore.txt");
					//writer = theSourceFile.OpenText();
					writer.WriteLine(curHighScoreString);
					writer.Close();
				}

				guiText.text = "High Score: " + curHighScoreString + "\n";
				guiText.text += "Score: " + curHighScoreString + '\n' +
					"Minions Saved: " + MinionHomeScript.minionCount.ToString () + "/" + TileMakeScript.maxMinions.ToString() + '\n' +
						"Minions Dead: " + StopDetectionScript.minionDead.ToString () + "/" + TileMakeScript.maxMinions.ToString();
				Debug.Log(guiText.text + '\n');
				gameEnd = true;
			}
		}
	}
}
