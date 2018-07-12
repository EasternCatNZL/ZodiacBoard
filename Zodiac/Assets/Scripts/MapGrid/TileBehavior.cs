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

    [System.Serializable]
    public struct TileNeighbours
    {
        public TileBehavior xPosTile; //where other is to +ve x
        public TileBehavior zPosTile; //where other is to +ve z
        public TileBehavior xNegTile; //where other is to -ve x
        public TileBehavior zNegTile; //where other is to -ve z
    }

    public enum TileType
    {
        NONE,
        BASIC
    }

    public TileInformation information;
    public TileProperties properties;
    public TileNeighbours neighbours;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //remove self from list
    public void RemoveSelf()
    {
        if (transform.GetComponentInParent<MapHandler>())
        {
            transform.GetComponentInParent<MapHandler>().RemoveTile(this);
        }
    }

    //on destroy, tell its map to remove self from list
    private void OnDestroy()
    {
        if (transform.GetComponentInParent<MapHandler>())
        {
            transform.GetComponentInParent<MapHandler>().RemoveTile(this);
        }
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

    //Copy information from one tile to another
    public void CopyTileInfo(TileBehavior other)
    {
        //copy information
        information.xPos = other.information.xPos;
        information.zPos = other.information.zPos;
        information.height = other.information.height;

        //copy properties
        properties.type = other.properties.type;
    }

    //get the tile type
    public TileType GetTileType()
    {
        return properties.type;
    }

    //get coords
    public Vector3 GetWorldCoord()
    {
        Vector3 inWorldPos = new Vector3(information.xPos, information.height, information.zPos);
        return inWorldPos;
    }

    public int GetXCoord()
    {
        return information.xPos;
    }

    public int GetZCoord()
    {
        return information.zPos;
    }
}
