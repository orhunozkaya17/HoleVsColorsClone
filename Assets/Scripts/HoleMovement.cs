using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleMovement : MonoBehaviour
{
    [Header("hole mesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshColldier;
    [Header("hole vertices radius")]
    [SerializeField] Vector2 movelimits;
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform rotatingCircle;

    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVerticesCount = 0;

    float x, y;
    Vector3 touch, targetPos;
    void Start()
    {
        Game.isMoving = false;
        Game.isGameover = false;
        holeVertices = new List<int>();
        offsets = new List<Vector3>();
        mesh = meshFilter.mesh;
        findHoleVertices();
        RotateCircleAnim();
    }
    void RotateCircleAnim()
    {
        rotatingCircle.DORotate(new Vector3(90, 0, -90), 0.2f).SetEase(Ease.Linear).From(new Vector3(90, 0, 0)).SetLoops(-1, LoopType.Incremental);
    }
    private void findHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);
            if (distance < radius)
            {

                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }
        holeVerticesCount = holeVertices.Count;
    }

    // Update is called once per frame
    void Update()
    {
#if PLATFORM_STANDALONE
        Game.isMoving = Input.GetMouseButton(0);

        if (!Game.isGameover && Game.isMoving)
        {
            Movehole();
            updateholeVerticespostion();
        }
#else
//mobile touch
        Game.isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;

        if (!Game.isGameover && Game.isMoving)
        {
            Movehole();
            updateholeVerticespostion();
        }
#endif

    }

    private void updateholeVerticespostion()
    {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshColldier.sharedMesh = mesh;
    }

    private void Movehole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");
        touch = Vector3.Lerp(holeCenter.position, holeCenter.position + new Vector3(x, 0, y), moveSpeed * Time.deltaTime);
        targetPos = new Vector3(Mathf.Clamp(touch.x, -movelimits.x, movelimits.x), touch.y, Mathf.Clamp(touch.z, -movelimits.y, movelimits.y));
        holeCenter.position = targetPos;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(holeCenter.position, radius);
    }
}
