using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Unity;

namespace UnityEditor
{
    [CreateAssetMenu]
    [CustomGridBrush(false, true, false, "Prefab Brush")]
    public class CustomBrush : GridBrush
    {
        public GameObject[] m_Prefabs;

        [HideInInspector]
        public int index = 0;

        public override void Paint( GridLayout grid, GameObject brushTarget, Vector3Int position )
        {

            GameObject prefab = m_Prefabs[index];
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo((UnityEngine.Object)instance, "Paint Prefabs");
            if ( instance != null )
            {
                instance.transform.SetParent(brushTarget.transform);
                instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, 0) + new Vector3(.5f, .5f, .5f)));

            }
        }

        public override void Erase( GridLayout grid, GameObject brushTarget, Vector3Int position )
        {
            // Do not allow editing palettes
            if ( brushTarget.layer == 31 )
                return;

            Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, position.z));
            if ( erased != null )
                Undo.DestroyObjectImmediate(erased.gameObject);
        }

        private static Transform GetObjectInCell( GridLayout grid, Transform parent, Vector3Int position )
        {
            int childCount = parent.childCount;
            Vector3 min = grid.LocalToWorld(grid.CellToLocalInterpolated(position));
            Vector3 max = grid.LocalToWorld(grid.CellToLocalInterpolated(position + Vector3Int.one));
            Bounds bounds = new Bounds((max + min) * .5f, max - min);

            for ( int i = 0; i < childCount; i++ )
            {
                Transform child = parent.GetChild(i);
                if ( bounds.Contains(child.position) )
                    return child;
            }
            return null;
        }

    }

    [CustomEditor(typeof(CustomBrush))]
    public class PrefabBrushEditor : GridBrushEditor
    {
        private CustomBrush prefabBrush { get { return target as CustomBrush; } }

        GameObject holder;

        public override void OnMouseLeave()
        {
            base.OnMouseLeave();

            if ( holder )
                DestroyImmediate(holder);
        }


        public override void OnPaintSceneGUI( GridLayout gridLayout, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing )
        {

            base.OnPaintSceneGUI(gridLayout, null, position, tool, executing);

            Event e = Event.current;

            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.C )
            {
                prefabBrush.index++;
                if ( prefabBrush.index == prefabBrush.m_Prefabs.Length )
                    prefabBrush.index = 0;

                DestroyImmediate(holder);
                holder = Instantiate(prefabBrush.m_Prefabs[prefabBrush.index]);

            }

            if ( !holder )
                holder = Instantiate(prefabBrush.m_Prefabs[prefabBrush.index]);

            holder.transform.position = position.position + new Vector3(.5f, 0.5f, 0.0f);

        }

    }
}
