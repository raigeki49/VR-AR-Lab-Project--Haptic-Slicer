using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;
public class SliceObject : MonoBehaviour
{
	public Transform planeDebug;
	public GameObject target;

	public Material insideOfSphereMaterial;

	public float sliceForce = 200f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Slice(GameObject target)
	{
		float offset = getOffset(target);
		float targetRadius = target.GetComponent<SphereCollider>().radius * target.transform.localScale.x;
		//print("target radius:" + targetRadius);

		float ratio = Mathf.Round((offset / targetRadius)*100) / 100; //ratio is how much of one half of the sphere was cut off. ratio = 0 means it was perfectly cut in half.
		//print("ratio:" + ratio);

		float score = 100 * (1 - ratio);
		print("Score:" + score);

		SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);

		if (hull != null)
		{
			GameObject upperHull = hull.CreateUpperHull(target, insideOfSphereMaterial);
			SetupSlicedComponents(upperHull);

			GameObject lowerHull = hull.CreateLowerHull(target, insideOfSphereMaterial);
			SetupSlicedComponents(lowerHull);

			Destroy(target);
		}
	}

	public void SetupSlicedComponents(GameObject slicedObject)
	{
		Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
		MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
		collider.convex = true;
		rb.AddExplosionForce(sliceForce, slicedObject.transform.position, 1);
	}

	private void OnTriggerEnter(Collider other)
	{
		Slice(other.gameObject);
	}

	public float getOffset(GameObject target) //Function returns the distance between the slice and the center of the target
	{
		Vector3 vectorBetweenCenters = planeDebug.transform.InverseTransformPoint(target.transform.position);
		return Math.Abs(vectorBetweenCenters.y);
	}
}
