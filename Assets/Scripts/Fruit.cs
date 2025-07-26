using System;
using UnityEngine;

public class Fruit : MonoBehaviour
{

	private bool canBeDestroyed = false;
	private float spawnTime;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		spawnTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time - spawnTime >= 0.1f)
		{
			canBeDestroyed = true;
		}
	}

	void FixedUpdate()
	{
		/* Used to determine how much height the object lost before it reaches the player
		if (Math.Abs(this.transform.position.z - 0.5f) < 0.01f)
		{
			Debug.Log("Position" + this.transform.position.ToString());
		}*/
	}

	private void OnTriggerEnter(Collider other)
	{
		if (canBeDestroyed)
		{
			if (other.CompareTag("Deadzone"))
			{
				Destroy(this.gameObject);
			}
		}
	}
}
