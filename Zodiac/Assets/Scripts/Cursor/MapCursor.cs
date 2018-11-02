using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour {

    [Header("Positioning vars")]
    public float yOffset = 4.0f;

    //control vars
    private TileBehavior currentTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToTile(TileBehavior tile)
    {
        currentTile = tile;
        transform.position = currentTile.transform.position;
        transform.position = transform.position + new Vector3(0.0f, yOffset, 0.0f);
    }

    public void MoveXPosTile()
    {
        if (currentTile.neighbours.xPosTile)
        {
            GoToTile(currentTile.neighbours.xPosTile);
        }
    }

    public void MoveXNegTile()
    {
        if (currentTile.neighbours.xNegTile)
        {
            GoToTile(currentTile.neighbours.xNegTile);
        }
    }

    public void MoveZPosTile()
    {
        if (currentTile.neighbours.zPosTile)
        {
            GoToTile(currentTile.neighbours.zPosTile);
        }
    }

    public void MoveZNegTile()
    {
        if (currentTile.neighbours.zNegTile)
        {
            GoToTile(currentTile.neighbours.zNegTile);
        }
    }
}
