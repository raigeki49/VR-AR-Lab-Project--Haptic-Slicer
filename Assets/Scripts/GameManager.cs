using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

	public GameObject FruitPrefab;
	delegate void SpawnObjects();
	private List<SpawnObjects> spawnObjects = new List<SpawnObjects>();

	private float nextSpawnTime = 2f;
	private float lastSpawnTime = 0f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

		//spawnObjects.Add(SpawnIn);
		spawnObjects.Add(FlyInFromBottom);
		spawnObjects.Add(FlyInFromSides);
		spawnObjects.Add(FlyTowardsPlayer);

	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time - lastSpawnTime >= nextSpawnTime)
		{
			int spawnType = Random.Range(0, spawnObjects.Count);
			spawnObjects[spawnType]();

			lastSpawnTime = Time.time;
			nextSpawnTime = Random.Range(0.2f, 1.5f);
		}
	}

	GameObject CreateObject()
	{
		return Instantiate(FruitPrefab);
	}

	void SpawnInQuadrant(GameObject fruit)
	{
		int quadrant = Random.Range(0, 4);
		float[] minMaxCoords = GetMinMaxCoordsForQuadrant(quadrant);

		float x = Random.Range(minMaxCoords[0], minMaxCoords[1]);
		float y = Random.Range(minMaxCoords[2], minMaxCoords[3]);
		float z = 0.5f;

		Vector3 Position = new Vector3(x, y, z);
		fruit.transform.position = Position;
	}

	void SpawnIn()
	{
		GameObject fruit = CreateObject();
		float x = Random.Range(-0.36f, 0.36f);
		float y = Random.Range(0.34f, 0.66f);
		float z = 0.5f;

		Vector3 Position = new Vector3(x, y, z);
		fruit.transform.position = Position;
	}

	void FlyInFromBottom()
	{
		GameObject fruit = CreateObject();
		float x = Random.Range(-0.36f, 0.36f);
		float y = 0.08f;
		float z = 0.5f;

		Vector3 Position = new Vector3(x, y, z);
		fruit.transform.position = Position;

		Rigidbody fruitRigidbody = fruit.GetComponent<Rigidbody>();
		Vector3 Force = new Vector3(0, 55f, 0);
		fruitRigidbody.AddForce(Force);
	}

	void FlyInFromSides()
	{
		GameObject fruit = CreateObject();
		int side = Random.Range(0, 2) == 0 ? -1 : 1; //-1 = spawns left side & 1 = spawns right side

		float x = 0.7f * side;
		float y = Random.Range(0.34f, 0.66f);
		float z = 0.5f;

		Vector3 Position = new Vector3(x, y, z);
		fruit.transform.position = Position;

		Rigidbody fruitRigidbody = fruit.GetComponent<Rigidbody>();
		Vector3 Force = new Vector3(55f * -side, 0, 0);
		fruitRigidbody.AddForce(Force);
	}

	void FlyTowardsPlayer()
	{
		GameObject fruit = CreateObject();
		float x = Random.Range(-0.36f, 0.36f);
		float y = Random.Range(0.34f, 0.66f) + 0.24f; //+0.24f because the object will have lost 0.24f in height when reaching the player.
		float z = 3f;

		Vector3 Position = new Vector3(x, y, z);
		fruit.transform.position = Position;

		Rigidbody fruitRigidbody = fruit.GetComponent<Rigidbody>();
		Vector3 Force = new Vector3(0, 0, -150f);
		fruitRigidbody.AddForce(Force);
	}

	float[] GetMinMaxCoordsForQuadrant(int quadrant)
	{
		float minX;
		float maxX;
		float minY;
		float maxY;

		if (quadrant == 1)
		{
			minX = -0.36f;
			maxX = -0.12f;
			minY = 0.61f;
			maxY = 0.66f;
		}
		else if (quadrant == 2)
		{
			minX = 0.12f;
			maxX = 0.36f;
			minY = 0.61f;
			maxY = 0.66f;
		}
		else if (quadrant == 3)
		{
			minX = -0.36f;
			maxX = -0.12f;
			minY = 0.34f;
			maxY = 0.39f;
		}
		else
		{
			minX = 0.12f;
			maxX = 0.36f;
			minY = 0.34f;
			maxY = 0.39f;
		}

		return new float[] {minX, maxX, minY, maxY};
	}
}
