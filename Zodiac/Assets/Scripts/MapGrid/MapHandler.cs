using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {

    //map representation
    private TileBehavior[,] board = new TileBehavior[0, 0];
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

    //save the new board to this object
    public void SetBoard(TileBehavior[,] newBoard)
    {
        board = newBoard;
    }
}
