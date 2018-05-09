using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Circle : AbstractMesh
{

	public int divisions;
	public float angle;
	public float radius;


	private float perAngle;


	private void Start()
	{
		origin = gameObject.transform.position;
		perAngle = angle / divisions;
		mesh = gameObject.GetComponent<MeshFilter>().mesh;
		Debug.Log(origin);
		Debug.Log(perAngle);
		GenerateMesh(divisions, radius);

	}
	protected Vector3[] SetVertices(int figures, float length)
	{
		Vector3[] result = new Vector3[figures+1];
		Vector3 direction = new Vector3(0, 0, 1);
		for (int i = 0; i < result.Length; i++)
		{
			if (i == 0)
			{
				result[i] = origin;
            }
			else
			{
				result[i] = origin + length * direction;
				direction = Quaternion.AngleAxis(perAngle, Vector3.up) * direction;

			}
		}
		return result;
	}
	protected int[] SetTriangles(int figures)
	{
		int[] result = new int[3 * figures];
		int vertex = 1;
		for (int i = 0; i < result.Length; i += 3)
		{
            
            
            result[i] = 0; // origin vertex
            result[i + 1] = vertex;
			if (vertex == figures)
				vertex = 0;
			result[i + 2] = (vertex + 1);
		
			vertex++;
		}
		Debug.Log(result.Length);
		return result;
	}

	public override void GenerateMesh(int figures, float length)
    {
		vertices = SetVertices(figures, length);
		triangles = SetTriangles(figures);


        
		SetMesh();
    }

}
