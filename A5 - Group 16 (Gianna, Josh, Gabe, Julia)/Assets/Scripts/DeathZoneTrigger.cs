using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Script to trigger upon entering a death zone.

public class DeathZoneTrigger : MonoBehaviour {
    public AudioClip deathSFX;
    public AudioSource audio;


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Equals("Player(Clone)")) { //Verify colliding object is player
            audio.PlayOneShot(deathSFX, 0.7f);
            SceneManager.LoadScene("Scene1");
        }
    }
}