using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class OptimizedGrid : AbstractMesh
{
    public int minRandomInteger;
    public int maxRandomInteger;

    [SerializeField]
    private float speed;
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision");
    }
    private void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        origin = gameObject.transform.position;
        GenerateMesh(numberOfFigures, lengthOfSingleFigure);
        StartCoroutine(VibrateMesh(5));
        StartCoroutine(VibrateMesh(30));
        StartCoroutine(VibrateMesh(61));
        StartCoroutine(VibrateMesh(75));
      
    }
    
    IEnumerator VibrateMesh(int index)
    {
        bool up = true;
        int i1, i2, i3, i4;
        i1 = index;
        i2 = i1 + 1;
        i3 = index + numberOfFigures + 1;
        i4 = i3 + 1;
        while (true)
        {
            if(up)
            {
                vertices[i1].y += Time.deltaTime * speed;
                vertices[i2].y += Time.deltaTime * speed;
                vertices[i3].y += Time.deltaTime * speed;
                vertices[i4].y += Time.deltaTime * speed;
                if (vertices[i1].y >= 10)
                    up = false;
                
            }
            else
            {
                vertices[i1].y -= Time.deltaTime * speed;
                vertices[i2].y -= Time.deltaTime * speed;
                vertices[i3].y -= Time.deltaTime *speed;
                vertices[i4].y -= Time.deltaTime * speed;
                if (vertices[i1].y <= -10)
                    up = true;
            }
            SetMesh();
            yield return null;
        }
    }
    public void changeMesh()
    {
        int randomIndex = Random.RandomRange(numberOfFigures,numberOfFigures+10);
        int randomIndex2 = randomIndex+1;
        int randomIndex3 = randomIndex + numberOfFigures + 1;
        int randomIndex4 = randomIndex3+1;  
        vertices[randomIndex].y = vertices[randomIndex2].y = vertices[randomIndex3].y = vertices[randomIndex4].y = 9f;
        SetMesh();
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            changeMesh();
       
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
