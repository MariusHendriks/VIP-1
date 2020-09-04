using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToCone : MonoBehaviour
{
    public float height = 2f;
    public float radius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(new Vector3(0, 0, 0));
        var steps = 30;

        List<int> triangles = new List<int>();

        for (int i = 0; i < steps; i++)
        {
            points.Add(new Vector3(Mathf.Cos(i * 2 * Mathf.PI / steps) * radius, 0, Mathf.Sin(i * 2 * Mathf.PI / steps) * radius));
        }

        points.Add(new Vector3(0, height, 0));

        for (int i = 0; i < steps; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            if (i == steps - 1)
            {
                triangles.Add(1);
            }
            else
            {
                triangles.Add(i + 2);
            }
        }

        for (int i = 0; i < steps; i++)
        {
            triangles.Add(i + 1);
            triangles.Add(31);
            if (i == steps - 1)
            {
                triangles.Add(1);
            }
            else
            {
                triangles.Add(i + 2);
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = points.ToArray();
        mesh.triangles = triangles.ToArray();

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        mesh.RecalculateNormals();
        if (gameObject.GetComponent<MeshCollider>() != null) //Check if there exists a meshcollider on the gameobject
        {
            gameObject.GetComponent<MeshCollider>().sharedMesh = mesh; //Add collider
        }

    }
}