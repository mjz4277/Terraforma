using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum BiomeType
{
    Default,
    Mountain,
    Forest,
    Lake,
    Tundra,
    Volcano,
    Woods,
    Hills
}
public class Biome : MonoBehaviour {
    private List<Tile> tiles = new List<Tile>();
    private BiomeType b_type;
    private Transform center;
    private Transform t_biome;
    private int size;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuildBiome()
    {
        /*
        float tile_size = 1.5f;
        float tile_spacing = 0.05f;
        float tile_height = 2.0f;
        float tile_width = (Mathf.Sqrt(3) / 2) * tile_height;

        t_biome = new GameObject("Biome").transform;

        float offset = (tile_size / 2) * Mathf.Sqrt(2);
        offset = (tile_width / 2) + (tile_spacing / 2);

        int rows = (size * 2) - 1;

        Vector3 center_pos = center.position;
        Vector3 row_start;
        int row_size = size + 1;

        for (int i = 0; i < rows; i++)
        {
            float nextOff = (i % 2 == 0) ? 0 : offset;

            for (int j = 0; j < row_size; j++)
            {
                Vector3 pos = new Vector3(i * ((tile_height * 0.75f) + tile_spacing), 0, j * (tile_width + tile_spacing) + nextOff);
                mapPositions.Add(pos);
                GameObject tile = Instantiate(tile_types[0], pos, Quaternion.identity) as GameObject;
                tile.name = "Tile";
                tile.transform.SetParent(boardHolder);
                map[i, j] = tile;
                Tile t = tile.GetComponent<Tile>();
                tiles.Add(t);
            }
        }
         * */
    }
}
