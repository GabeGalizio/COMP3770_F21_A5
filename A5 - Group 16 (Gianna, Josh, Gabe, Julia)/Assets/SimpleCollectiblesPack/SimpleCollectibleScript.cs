using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, Type1, Type2, Type3, Type4, Type5}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;

	private Vector3 newVector;

	public int newX = 30;
	public int newY = 15;
	public int newZ = 45;

	// Use this for initialization
	void Start ()
	{
		newVector = new Vector3(30, 15, 45);
	}
	
	// Update is called once per frame
	void Update ()
	{

		//newVector.x = newX;
		//newVector.y = newY;
		//newVector.z = newZ;

		if (rotate)
			transform.Rotate (newVector * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player") {
			Collect ();
		}
	}

	public void Collect()
	{
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		//Below is space to add in your code for what happens based on the collectible type

		ScoreTracker.score += 1;
		Debug.Log("Score is " + ScoreTracker.score);
		Destroy (gameObject);
	}
}
