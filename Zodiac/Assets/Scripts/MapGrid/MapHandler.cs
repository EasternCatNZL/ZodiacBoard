﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {

    //map representation
    [SerializeField]
    private TileBehavior[,] board = new TileBehavior[0, 0];
    [SerializeField]
    private List<TileBehavior> boardDetails = new List<TileBehavior>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //get the board
    public TileBehavior[,] GetBoard()
    {
        return board;
    }

    public List<TileBehavior> GetBoardDetails()
    {
        return boardDetails;
    }

    public void AddTileToBoard(TileBehavior tile)
    {
        boardDetails.Add(tile);
    }

    //save the new board to this object
    public void SetBoard(TileBehavior[,] newBoard)
    {
        board = newBoard;
    }

    //recursively search through list
    public bool CheckForSameInBoard(TileBehavior tileToCheck, int currentIndex)
    {
        //set up bool
        bool sameFound = false;
        //see if current index tile is same as tile to check
        if (tileToCheck.CheckIfSame(boardDetails[currentIndex])){
            //if so, then call true
            sameFound = true;
            return sameFound;
        }
        else
        {
            //else, check that current index not last
            if(currentIndex != boardDetails.Count - 1)
            {
                //increment current index
                currentIndex++;
                //call function again
                sameFound = CheckForSameInBoard(tileToCheck, currentIndex);
            }
        }

        return sameFound;
    }
}
