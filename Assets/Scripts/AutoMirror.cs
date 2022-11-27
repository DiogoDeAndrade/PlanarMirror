using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMirror : MonoBehaviour
{
    public Camera   originalCamera;
    public Vector2  mirrorSize = new Vector2(2, 3);
    public int      resolution = 512;
    public Shader   reflectionShader;

    Camera          reflectCamera;
    MeshFilter      meshFilter;
    MeshRenderer    meshRenderer;
    Mesh            mirrorMesh;
    RenderTexture   renderTarget;
    Material        material;

    void Start()
    {
        reflectCamera = GetComponentInChildren<Camera>();
        if (reflectCamera == null)
        {
            GameObject go = new GameObject();
            go.name = "ReflectionCamera";
            go.transform.SetParent(transform);
            reflectCamera = go.AddComponent<Camera>();
            reflectCamera.CopyFrom(originalCamera);
            reflectCamera.transform.localPosition = Vector3.zero;
            reflectCamera.transform.localRotation = Quaternion.identity;
            reflectCamera.transform.localScale = Vector3.one;

            int sy = (int)(resolution * mirrorSize.y / mirrorSize.x);
            renderTarget = new RenderTexture(resolution, sy, 32);
            renderTarget.name = "ReflectionTexture";
            reflectCamera.targetTexture = renderTarget;
        }

        BuildMirrorObject();
    }

    void BuildMirrorObject()
    {
        GameObject go = new GameObject();
        go.name = "MirrorObject";
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        meshFilter = go.AddComponent<MeshFilter>();
        meshRenderer = go.AddComponent<MeshRenderer>();

        mirrorMesh = new Mesh();
        mirrorMesh.name = "MirrorMesh";

        UpdateMesh();
        UpdateMaterial();
    }

    void UpdateMesh()
    { 
        float mx = mirrorSize.x * 0.5f;

        mirrorMesh.SetVertices(new Vector3[]
        {
            new Vector3(-mx, 0.0f, 0.0f),
            new Vector3(-mx, mirrorSize.y, 0.0f),
            new Vector3( mx, mirrorSize.y, 0.0f),
            new Vector3( mx, 0.0f, 0.0f)
        });
        mirrorMesh.SetUVs(0, new Vector2[]
        {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f)
        });
        mirrorMesh.SetTriangles(new int[] { 0, 2, 1, 0, 3, 2 }, 0);
        mirrorMesh.UploadMeshData(false);

        meshFilter.sharedMesh = mirrorMesh;
    }

    void UpdateMaterial()
    {
        if (material == null)
        {
            material = meshRenderer.material;
            if (material == null)
            {
                material = new Material(reflectionShader);
                meshRenderer.material = material;
            }
            else
            {
                material.shader = reflectionShader;
            }
        }
        material.SetTexture("_MainTex", renderTarget);
    }

    void Update()
    {
        Vector3 mirrorNormal = transform.forward;

        reflectCamera.transform.localPosition = new Vector3(0.0f, mirrorSize.y * 0.5f, 0.0f);

        Vector3 toMirror = (reflectCamera.transform.position - originalCamera.transform.position).normalized;
        Vector3 reflectedDir = Vector3.Reflect(toMirror, mirrorNormal);

        reflectCamera.transform.rotation = Quaternion.LookRotation(reflectedDir, Vector3.up);
    }
}
