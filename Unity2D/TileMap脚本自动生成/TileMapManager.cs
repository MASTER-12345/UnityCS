using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;
using System;
using UnityEngine.UIElements;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class TileMapManager : MonoBehaviour
{
    [BoxGroup("Prefab")]
    public GameObject tilemapPrefab;
    private TileCollection tco;

    [BoxGroup("para")]
    public int map_length;
    [BoxGroup("para")]
    public int map_width;
    [BoxGroup("para"),Tooltip("地图的边距")]
    public float map_margin;
    [BoxGroup("para")]
    public int map_count;

    [BoxGroup("mapRegister")]
    public List<GameObject> tilemap_List=new List<GameObject>();
    

    private void Start()
    {
        tco=GameObject.Find("Collection").GetComponent<TileCollection>();
        CreatTileMap(map_count);
    }
    public void CreatTileMap(int count)
    {
        for(int i = 0; i < count; i++)
        {
            int c = Random.Range(0, tco.PakageTile_List.Count);
            TileCollection.TileName tilename = (TileCollection.TileName)c;
            
            GameObject tilemap_obj = Instantiate(tilemapPrefab);
            tilemap_obj.name = i.ToString();
            tilemap_obj.transform.parent = transform;
            {
                tilemap_obj.AddComponent<TilemapCollider2D>();
                tilemap_obj.AddComponent<CompositeCollider2D>();
                tilemap_obj.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
                tilemap_obj.GetComponent<CompositeCollider2D>().isTrigger = true;
                tilemap_obj.GetComponent<TilemapCollider2D>().usedByComposite = true;
                tilemap_obj.GetComponent<CompositeCollider2D>().geometryType = CompositeCollider2D.GeometryType.Polygons;
            }
            Tilemap tilemap = tilemap_obj.GetComponent<Tilemap>();
            {
                for (int x = 0; x < map_width; x++)
                {
                    for (int y = 0; y < map_length; y++)
                    {
                        //this tilemap set the tile
                        tilemap.SetTile(new Vector3Int(x, y, 0), tco.GetTile(tilename));
                    }
                }
            }
            tilemap_List.Add(tilemap_obj);
        }
        SortTileMap();
    }  
    public void SortTileMap()
    {
        int code = 0;
        float Ismetric_length = 0;
        for(int y = 0; y <4; y++)
        {
            for(int x=0;x<4; x++)
            {
                float y_length = (y + 1) * map_margin/2f;
                float x_length = ((x + 1) * map_margin)  -y_length*2f ;
                
                tilemap_List[code].transform.position = new Vector3(x_length, y_length-Ismetric_length, 0);
                code += 1;
                Ismetric_length -= map_margin/2;
            }
            Ismetric_length = 0;
        }

    }
}

