using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CResourceTile : Tile
{
    public override void GetTileData( Vector3Int position, ITilemap tilemap, ref TileData tileData )
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override void RefreshTile( Vector3Int position, ITilemap tilemap )
    {
        base.RefreshTile(position, tilemap);
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/Resource Tile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Resource Tile", "New Resource Tile", "Asset", "Save Resource Tile", "Assets");
        if ( path == "" )
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<CResourceTile>(), path);
    }
#endif
}