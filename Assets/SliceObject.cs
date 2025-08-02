using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
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
		print(other.name);
	}
}
