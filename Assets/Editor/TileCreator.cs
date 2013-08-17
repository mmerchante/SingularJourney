using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TileCreator : MonoBehaviour
{

    // Build a tile from selected mesh object
	[MenuItem ("Tileset/Create Tile")]
	static void CreateTile() 
    {
        GameObject[] selected = Selection.gameObjects;

        foreach (GameObject go in selected)
            CreateTile(go);
	}

    static void CreateTile(GameObject go)
    {
        go.transform.parent = null;
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        Renderer[] renderers = go.GetComponentsInChildren<Renderer>(true);

        if (renderers.Length > 0)
        {
            Bounds bounds = renderers[0].bounds;

            foreach (Renderer r in renderers)
                bounds.Encapsulate(r.bounds);
           
            GameObject tile = new GameObject(go.name);

            Tile t = tile.AddComponent<Tile>();

            t.bounds = bounds;

            go.transform.parent = tile.transform;
            go.transform.localPosition = bounds.center * -1f;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            
        }
    }

}
