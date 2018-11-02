using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitHandler : MonoBehaviour {

    [Header("Level set up vars")]
    public TileBehavior cursorStartTile;
    public List<TileBehavior> teamAStartTiles = new List<TileBehavior>();
    public List<TileBehavior> teamBStartTiles = new List<TileBehavior>();

    [Header("Tags")]
    public string boardTag = "Board";
    public string cursorTag = "Cursor";

    [Header("Script refs")]
    public BoardManager board;
    public MapCursor cursor;

	// Use this for initialization
	void Start () {
        //get things if not set
        if (!board)
        {
            board = GameObject.FindGameObjectWithTag(boardTag).GetComponent<BoardManager>();
        }
        if (!cursor)
        {
            cursor = GameObject.FindGameObjectWithTag(cursorTag).GetComponent<MapCursor>();
        }

        cursor.GoToTile(cursorStartTile);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
