//Script to manage actions on the splash screen.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SplashScreenScript : MonoBehaviour {

    [SerializeField] private float delay;
    public AudioClip pressSFX;
    public AudioSource audio;

    void Update() {
        if (Keyboard.current.anyKey.wasPressedThisFrame) {
            audio.PlayOneShot(pressSFX, 0.7f);
            StartCoroutine(SceneTransition(delay));
        }
    }

    //Coroutine to delay transition to main menu (so sound effect has time to play.)
    public IEnumerator SceneTransition(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Main Menu");
    }
}
