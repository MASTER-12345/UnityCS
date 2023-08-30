using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

public class TileCollection : MonoBehaviour
{
    public enum TileName
    {
        grassland, sandland
    }
    [Serializable]
    public class Package_Tile
    {
        public TileName name;
        public TileBase tile;
    }
    [TableList]
    public List<TileCollection.Package_Tile> PakageTile_List = new List<TileCollection.Package_Tile>();

    public TileBase GetTile(TileName tilename)
    {
        for(int i = 0; i < PakageTile_List.Count; i++)
        {
            if (PakageTile_List[i].name == tilename)
            {
                return PakageTile_List[i].tile;
            }
        }
        return null;
    }
    
}
