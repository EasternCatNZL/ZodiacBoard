using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

    [Header("Tile objects")]
    [Tooltip("Basic tile")]
    public GameObject basicTile;
    [Tooltip("Void tile")]
    public GameObject voidTile;

    [Header("Map to build")]
    [Tooltip("The map handler object holding the map details")]
    public MapHandler map;

    [Header("Prefabs for tiles")]
    [Tooltip("The objects used to represent the tiles")]
    public GameObject[] tileArray = new GameObject[0];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //builds out the map into the world
    private void BuildMap()
    {
        //if map not null, reference the board details of map
        if (map)
        {
            List<TileBehavior> board = map.GetBoardDetails();
            //for all objects in map
            for (int i = 0; i < board.Count; i++)
            {
                //if the current object is not null
                if (board[i])
                {
                    //create a tile based on 
                }
            }
        }

       
    }

    //builds connections between tiles once map is complete
    private void BuildConnections()
    {

    }
}
