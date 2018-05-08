using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Circle : AbstractMesh {

	public int divisions;
	public float angle;
    public float radius;
   

	private float perAngle;

	   
	private void Start()
	{
		origin = gameObject.transform.position;
		perAngle = angle / divisions;
		mesh = gameObject.GetComponent<MeshFilter>().mesh;
		GenerateMesh(divisions, radius);

	}
	public override void GenerateMesh(int figures, float length)
    {
		vertices = new Vector3[figures];
		Vector3 tempOrigin = origin;
		for (int i = 0; i < vertices.Length;)
		{
			if (i == 0)
			{
				vertices[0] = new Vector3(origin.x,origin.y,origin.z);
				vertices[1] = new Vector3();
				vertices[2] = new Vector3();
				i += 3;
			}
			else
				i++;




		SetMesh();
    }

}
