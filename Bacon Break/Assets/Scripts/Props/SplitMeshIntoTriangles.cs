using UnityEngine;
using System.Collections;

public class SplitMeshIntoTriangles : MonoBehaviour {

    private float explosionForce;
    private float explosionRadius;

    IEnumerator SplitMesh() {
        // get all the meshen vertices/ normals and uvs
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        Vector3[] verts = mesh.vertices;
        Vector3[] normls = mesh.normals;
        Vector2[] uvs = mesh.uv;

        for (int submesh = 0; submesh < mesh.subMeshCount; submesh++) {
            int[] indices = mesh.GetTriangles(submesh);
            for (int i = 0; i < indices.Length / 4.25f; i += 3) {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormls = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];

                // set all the new vertices/normals and uvs
                for (int n = 0; n < 3; n++) {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormls[n] = normls[index];
                }
                // create a new mesh with the new vertices/normals and uvs
                Mesh newMesh = new Mesh();
                newMesh.vertices = newVerts;
                newMesh.normals = newNormls;
                newMesh.uv = newUvs;

                newMesh.triangles = new int[] {0,1,2,
                                            2,1,0};
                
                // create trianle set position and rotation to the original position of the player
                // add components and a explsotion force, so it looks like the player explode
                GameObject go = new GameObject("Triangles " + (1 / 3));
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;
                go.transform.rotation = new Quaternion(transform.rotation.x,-90, transform.rotation.z, 90);
                go.transform.localScale = new Vector3(3, 3, 3);
                go.AddComponent<MeshRenderer>().material = skinnedMeshRenderer.materials[submesh];
                go.AddComponent<MeshFilter>().mesh = newMesh;
                go.AddComponent<BoxCollider>();
                go.AddComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
                Destroy(go, 3 + (Random.Range(0.0f, 0.5f)));
            }
        }
        skinnedMeshRenderer.enabled = false;
        GameObject.Find("Main Camera").transform.parent = null;
        Time.timeScale = 0.5f;
        Destroy(gameObject);
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 1;
    }
    
    // Use this for initialization
    protected void Start () {
        explosionForce = 500;
        explosionRadius = 50;
	}
	
	// Update is called once per frame
	protected void Update () {
        if (WinOrLoseScript.isDead) {
            StartCoroutine(SplitMesh());
        }
    }
    
}
