using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
#if UNITY_EDITOR

    private static float SNAP_THRESHOLD_DISTANCE = 1f;
    private static float SNAP_THRESHOLD_FORCE = .5f;

    private static Vector3[] aabb = { new Vector3 (1f, 1f, 1f), new Vector3 (1f, -1f, -1f), new Vector3 (1f, 1f, -1f), new Vector3 (1f, -1f, 1f),
		new Vector3 (-1f, 1f, 1f), new Vector3 (-1f, -1f, -1f), new Vector3 (-1f, 1f, -1f), new Vector3 (-1f, -1f, 1f),		
	};

    public Bounds bounds;
    public bool snap = false;

    [HideInInspector]
    public bool closest = false;

    private static Tile[] tiles = null;

    private Vector3 previousPosition = Vector3.zero;

    private Vector3 outwards;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, bounds.size);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, Vector3.Scale(outwards, bounds.extents));
    }

    public void OnDrawGizmos()
    {
        if (closest)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, bounds.size);
        }
    }
    
    public void OnEnable()
    {
        if (Application.isEditor)
            ReloadTiles();
    }

    public void ReloadTiles()
    {
        tiles = GameObject.FindObjectsOfType(typeof(Tile)) as Tile[];
        Debug.Log("Reloading...");
    }

    public void Update()
    {
        if (Application.isEditor && snap)
        {
            if (Selection.activeGameObject == this.gameObject)
            {
                Tile closestTile = null;
                float minDistance = float.PositiveInfinity;

                Vector3 snapVector = Vector3.zero;

                foreach (Tile t in tiles)
                {
                    if (t == null)
                    {
                        ReloadTiles();
                        return;
                    }

                    if (t != this)
                    {
                        Bounds transformedBounds = TransformAABB(transform, new Bounds(transform.position, bounds.size));
                        Bounds transformedOtherBounds = TransformAABB(t.transform, new Bounds(t.transform.position, t.bounds.size));

                        Vector3 toOtherTile = transformedOtherBounds.center - transformedBounds.center;

                        Vector3 d = (VectorAbs(toOtherTile) - VectorAbs((transformedBounds.extents + transformedOtherBounds.extents)));
                        Vector3 minAxis = GetMinimumAbsAxis(d);

                        // Remove intersections
                        d = Vector3.Max(d, Vector3.zero);

                        // Any norm is useful
                        float distance = d.magnitude;

                        t.closest = false;

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestTile = t;
                            outwards = Vector3.Scale(minAxis, VectorSign(toOtherTile));
                            snapVector = outwards * Mathf.Abs(Vector3.Dot(d, outwards));
                        }
                    }
                }

                if (closestTile)
                {
                    closestTile.closest = true;

                    if (minDistance < SNAP_THRESHOLD_DISTANCE)
                    {
                        this.transform.position += snapVector;
                    }
                }
            }

            previousPosition = this.transform.position;
        }
    }

    private Vector3 VectorSign(Vector3 v)
    {
        return new Vector3(Mathf.Sign(v.x), Mathf.Sign(v.y), Mathf.Sign(v.z));
    }

    private Vector3 VectorAbs(Vector3 v)
    {
        v.x = Mathf.Abs(v.x);
        v.y = Mathf.Abs(v.y);
        v.z = Mathf.Abs(v.z);

        return v;
    }

    private Vector3 GetMinimumAbsAxis(Vector3 v)
    {
        Vector3 axis = VectorAbs(v);

        if (axis.x < axis.y && axis.x < axis.z)
            return Vector3.right;
        else if (axis.y < axis.x && axis.y < axis.z)
            return Vector3.up;
        return Vector3.forward;
    }

    private Bounds TransformAABB(Transform t, Bounds bounds)
    {
        Vector3 size = Vector3.zero;

        // No need to get efficient crazy
        foreach (Vector3 v in aabb)
        {
            Vector3 tmp = Vector3.Scale(bounds.size, v);
            Vector3 projected = new Vector3(Mathf.Abs(Vector3.Dot(tmp, t.right)),
                                             Mathf.Abs(Vector3.Dot(tmp, t.up)),
                                             Mathf.Abs(Vector3.Dot(tmp, t.forward)));
            size = Vector3.Max(size, projected);
        }

        return new Bounds(bounds.center, size);
    }

    public float GetMaxAbsComponent(Vector3 v)
    {
        return Mathf.Max(Mathf.Abs(v.x), Mathf.Max(Mathf.Abs(v.y), Mathf.Abs(v.z)));
    }

#endif
}
