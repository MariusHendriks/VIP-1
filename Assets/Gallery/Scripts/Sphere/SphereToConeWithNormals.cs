using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToConeWithNormals : MonoBehaviour
{
    public float height = 2f;
    public float radius = 1f;

    public int steps = 30;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> points = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();

        points.Add(new Vector3(0, 0, 0));
        normals.Add(new Vector3(0, -1, 0));

        List<int> triangles = new List<int>();

        // Points
        for (int i = 0; i < steps * 3; i++)
        {
            // 1
            Vector3 point = new Vector3(Mathf.Cos(i * 2 * Mathf.PI / steps) * radius, 0, Mathf.Sin(i * 2 * Mathf.PI / steps) * radius);
            points.Add(point);
            normals.Add(new Vector3(0, -1, 0));

            // 2
            points.Add(point);
            Vector3 normal = new Vector3(Mathf.Cos(i * 2 * Mathf.PI / steps) * radius, 0, Mathf.Sin(i * 2 * Mathf.PI / steps) * radius);
            normals.Add(normal);

            // 3
            points.Add(new Vector3(0, height, 0));
            normal = new Vector3(Mathf.Cos(i * 2 * Mathf.PI / steps), 0, Mathf.Sin(i * 2 * Mathf.PI / steps));
            normals.Add(normal);
        }

        // Onderkant
        for (int i = 0; i < steps * 3; i += 3)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            if (i == steps - 1)
            {
                triangles.Add(1);
            }
            else
            {
                triangles.Add(i + 4);
            }
        }

        // Zijkant
        for (int i = 0; i < steps * 3; i += 3)
        {
            triangles.Add(i + 2);
            triangles.Add(i + 3);
            if (i == (steps * 3) - 1)
            {
                triangles.Add(2);
            }
            else
            {
                triangles.Add(i + 5);
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = points.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
