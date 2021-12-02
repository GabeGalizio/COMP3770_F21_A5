using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to trigger upon entering a death zone.

public class DeathZoneTrigger : MonoBehaviour {
    private Vector3 spawnPosn;
    private Quaternion spawnRotation;
    public AudioClip deathSFX;
    public AudioSource audio;
    
    //Get initial player posn for this scene.
   

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Equals("Player(Clone)")) { //Verify colliding object is player
            audio.PlayOneShot(deathSFX, 0.7f);
            other.gameObject.transform.position = new Vector3(0, 0, 0);
            other.gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
