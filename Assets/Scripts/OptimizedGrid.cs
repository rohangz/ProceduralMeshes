using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class OptimizedGrid : AbstractMesh
{
    public int minRandomInteger;
    public int maxRandomInteger;
    private void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        origin = gameObject.transform.position;
        GenerateMesh(numberOfFigures, lengthOfSingleFigure);
            
    }
    protected Vector3[] SetVertices(int figures,float length)
    {
        Vector3[] ans = new Vector3[(figures+1)*(figures+1)];
        Vector3 tempOrigin = origin;
        int k = 0;
        for (int i = 0; i < ans.Length; i++)
        {
            ans[i] = tempOrigin;
            ans[i].y = Random.Range(minRandomInteger, maxRandomInteger);
            k++;
            if ((figures + 1) == k)
            {
                k = 0;
                tempOrigin.z = tempOrigin.z - length;
                tempOrigin.x = origin.x;
            }
            else
                tempOrigin.x = tempOrigin.x + length;
        }
        return ans;
    }
    protected int[] SetTriangles(int figures)
    {
        int[] ans = new int[6 * figures * figures];
        int j = 0;
        int k = 0;
        for (int i = 0; i < ans.Length;)
        {
            if (j == figures)
            {
                j = 0;
                k++;
            }
            else
            {
                ans[i] = k;
                ans[i + 1] = k + 1;
                ans[i + 2] = k + figures + 2;

                ans[i + 3] = k;
                ans[i + 4] = k + figures + 2;
                ans[i + 5] = k + figures + 1;
                k++;
                j++;
                i += 6;
            }
        }
        return ans;
    }
    public override void GenerateMesh(int figures, float length)
    {
        vertices = SetVertices(figures, length);
        triangles = SetTriangles(figures);
        SetMesh();
       
    }
}
