//Script that manages switching to scenes. 
//Contains functions which are primarily to be called by buttons.

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {

	public GameObject victim;
	//Load Menu Scene
	public void ToMenu() {
		SceneManager.LoadScene("Main Menu");
	}
	
	//Load Level 1 Scene
	public void ToLevel1() {
		SceneManager.LoadScene("Scene1");
	}
	
	//Load Level 2 Scene
	public void ToLevel2() {
		SceneManager.LoadScene("Level 2");
	}
	
	//Load Level 3 Scene
	public void ToLevel3() {
		SceneManager.LoadScene("Level 3");
	}
	
	//Load Completion Scene
	public void ToCompletionScreen() {
		SceneManager.LoadScene("Completion Screen");
	}

	//Load Splash Screen Scene
	public void ToSplashScreen() {
		SceneManager.LoadScene("Splash Screen");
    }

	//Quit Game
	public void QuitGame() {
		Application.Quit();
    }
	
	//Kills, makes Die, Murder Kills The thing
	public void KillDieMurder() {
		Destroy(victim);
    }
}
