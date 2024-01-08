using UnityEngine;
using System.Collections;

// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ExampleClass : MonoBehaviour
{
    public Vector3 parentLocation;
    public Vector3 parentRotation;
    public Vector3 parentScale;

    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        parentLocation = gameObject.transform.position;
        parentRotation = gameObject.transform.eulerAngles;
        parentScale = gameObject.transform.localScale;


        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        //gameObject.transform.localScale = new Vector3(0, 0, 0);
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        transform.gameObject.SetActive(true);
        gameObject.transform.position = parentLocation;
        gameObject.transform.eulerAngles = parentRotation;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}