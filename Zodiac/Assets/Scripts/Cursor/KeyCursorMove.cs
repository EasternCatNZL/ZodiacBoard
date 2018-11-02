using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCursorMove : MonoBehaviour {

    [Header("Input strings")]
    public string horizontal = "Horizontal";
    public string vertical = "Vertical";

    [Header("Budget input control")]
    public float timeBetweenInputs = 0.1f;

    [Header("Script refs")]
    public MapCursor cursor;

    //control vars
    float lastInputTime = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    void HandleInput()
    {
        if(Time.time > lastInputTime + timeBetweenInputs)
        {
            if (Luminosity.IO.InputManager.GetAxisRaw(vertical) > 0f)
            {
                cursor.MoveZPosTile();
                lastInputTime = Time.time;
            }
            else if (Luminosity.IO.InputManager.GetAxisRaw(horizontal) < 0f)
            {
                cursor.MoveXNegTile();
                lastInputTime = Time.time;
            }
            else if (Luminosity.IO.InputManager.GetAxisRaw(horizontal) > 0f)
            {
                cursor.MoveXPosTile();
                lastInputTime = Time.time;
            }
            else if (Luminosity.IO.InputManager.GetAxisRaw(vertical) < 0f)
            {
                cursor.MoveZNegTile();
                lastInputTime = Time.time;
            }
        }
        
    }
}
