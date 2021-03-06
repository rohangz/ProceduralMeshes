﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMesh : MonoBehaviour {

    public float lengthOfSingleFigure;
    public int numberOfFigures;
    
   protected int[] triangles;
    protected Vector3[] vertices;
    protected Vector3 origin;

    
   protected Mesh mesh;
        
    private void Awake()
    {
        origin = gameObject.transform.position;
    }
    protected void SetMesh()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles; 
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }
    abstract public void GenerateMesh(int figures, float length); 

}
