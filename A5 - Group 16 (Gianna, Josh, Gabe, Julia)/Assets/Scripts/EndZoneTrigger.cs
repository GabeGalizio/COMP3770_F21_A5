using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script that triggers upon entering level end zone.
public class EndZoneTrigger : MonoBehaviour {
	private void OnTriggerEnter(Collider other)	{
		if (other.gameObject.name.Equals("Player")) {
			ScoreTracker.levels++;
			SceneManager.LoadScene("Scene1");
		}
	}
}
