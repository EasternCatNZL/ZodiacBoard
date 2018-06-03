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

    //compares the two tiles, and returns true if the same
    public bool CheckIfSame(TileBehavior other)
    {
        bool same = false;
        if(information.xPos == other.information.xPos
            && information.zPos == other.information.zPos
            /*&& properties.type == other.properties.type*/)
        {
            same = true;
        }

        return same;
    }
}
