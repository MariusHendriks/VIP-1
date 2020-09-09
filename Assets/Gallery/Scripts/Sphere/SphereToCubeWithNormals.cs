using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToCubeWithNormals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] vertices =
        {
            new Vector3(-0.5f,-0.5f,-0.5f), //0  0
            new Vector3(-0.5f,-0.5f,-0.5f), //1  0
            new Vector3(-0.5f,-0.5f,-0.5f), //2  0
            new Vector3(0.5f,-0.5f,-0.5f),  //3  1
            new Vector3(0.5f,-0.5f,-0.5f),  //4  1
            new Vector3(0.5f,-0.5f,-0.5f),  //5  1
            new Vector3(-0.5f,0.5f,-0.5f),  //6  2
            new Vector3(-0.5f,0.5f,-0.5f),  //7  2
            new Vector3(-0.5f,0.5f,-0.5f),  //8  2
            new Vector3(0.5f,0.5f,-0.5f),   //9  3
            new Vector3(0.5f,0.5f,-0.5f),   //10 3
            new Vector3(0.5f,0.5f,-0.5f),   //11 3
            new Vector3(-0.5f,-0.5f,0.5f),  //12 4
            new Vector3(-0.5f,-0.5f,0.5f),  //13 4
            new Vector3(-0.5f,-0.5f,0.5f),  //14 4
            new Vector3(0.5f,-0.5f,0.5f),   //15 5
            new Vector3(0.5f,-0.5f,0.5f),   //16 5
            new Vector3(0.5f,-0.5f,0.5f),   //17 5
            new Vector3(-0.5f,0.5f,0.5f),   //18 6
            new Vector3(-0.5f,0.5f,0.5f),   //19 6
            new Vector3(-0.5f,0.5f,0.5f),   //20 6
            new Vector3(0.5f,0.5f,0.5f),    //21 7
            new Vector3(0.5f,0.5f,0.5f),    //22 7
            new Vector3(0.5f,0.5f,0.5f),    //23 7
        };

        Vector3[] normals =
        {
            new Vector3(-1,0,0), //0 0
            new Vector3(0,0,-1), //1 0
            new Vector3(0,-1,0), //2  0
            new Vector3(1,0,0),  //3  1
            new Vector3(0,-1,0), //4  1
            new Vector3(0,0,-1), //5  1
            new Vector3(0,1,0),  //6  2
            new Vector3(-1,0,0), //7  2
            new Vector3(0,0,-1), //8  2
            new Vector3(0,1,0),  //9  3
            new Vector3(1,0,0),  //10 3
            new Vector3(0,0,-1), //11 3
            new Vector3(-1,0,0), //12 4
            new Vector3(0,0,1),  //13 4
            new Vector3(0,-1,0), //14 4
            new Vector3(0,-1,0), //15 5
            new Vector3(1,0,0),  //16 5
            new Vector3(0,0,1),  //17 5
            new Vector3(-1,0,0), //18 6
            new Vector3(0,1,0),  //19 6
            new Vector3(0,0,1),  //20 6
            new Vector3(0,1,0),  //21 7
            new Vector3(0,0,1),  //22 7
            new Vector3(1,0,0),  //23 7
        };

        int[] triangles = { 1, 8, 11, 11, 5, 1, 9, 6, 19, 19, 21, 9, 22, 20, 13, 13, 17, 22, 15, 14, 4, 4, 14, 2, 0, 12, 7, 7, 12, 18, 3, 10, 16, 16, 10, 23 };
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
