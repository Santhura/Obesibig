using UnityEngine;
using System.Collections;

public class SplitMeshIntoTriangles : MonoBehaviour {

    IEnumerator SplitMesh() {
        SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer>();
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh m = mf.mesh;
        Vector3[] verts = m.vertices;
        Vector3[] normls = m.normals;
        Vector2[] uvs = m.uv;

        for (int submesh = 0; submesh < m.subMeshCount; submesh++) {
            int[] indices = m.GetTriangles(submesh);
            for (int i = 0; i < indices.Length; i += 3) {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormls = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];

                for (int n = 0; n < 3; n++) {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormls[n] = normls[index];
                }
                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormls;
                mesh.uv = newUvs;

                mesh.triangles = new int[] {0,1,2,
                                            2,1,0};
                GameObject go = new GameObject("Triangles " + (1 / 3));
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;
                go.AddComponent<MeshRenderer>().material = smr.materials[submesh];
                go.AddComponent<MeshFilter>().mesh = mesh;
                go.AddComponent<BoxCollider>();
                go.AddComponent<Rigidbody>().AddExplosionForce(10, transform.position, 10);
                Destroy(go, 5 + (Random.Range(0.0f, 0.5f)));
            }
        }
        smr.enabled = false;
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    void OnMouseDown() {
        StartCoroutine(SplitMesh());

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (WinOrLoseScript.isDead) {
            StartCoroutine(SplitMesh());
        }
    }

    IEnumerator WaitForSeconds(float time) {
        yield return new WaitForSeconds(time);

    }
}
