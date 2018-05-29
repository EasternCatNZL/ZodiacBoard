using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour {

    [System.Serializable]
    public struct TileInformation{
        public int xPos; //the x pos of the tile in grid
        public int zPos; //the z pos of the tile in grid
        public int height; //the height of the tile in map
    }

    [System.Serializable]
    public struct TileProperties
    {
        public TileType type; //the type of tile, including whether it exists or not
    }

    public enum TileType
    {
        NONE,
        BASIC
    }

    public TileInformation information;
    public TileProperties properties;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
