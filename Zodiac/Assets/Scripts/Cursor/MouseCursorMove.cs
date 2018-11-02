using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorMove : MonoBehaviour {

    [Header("Scan vars")]
    public float timeBetweenScans = 0.1f; //to avoid rays every frame

    float lastScanTime = 0.0f;

    [Header("Script refs")]
    public MapCursor cursor;

    [Header("Tags")]
    public string tileTag = "Tile";

    TileBehavior checkedTile; //used to prevent movement if mouse is sitting stationary while using other controls

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RayScanCursorMove();
	}

    void RayScanCursorMove()
    {
        if(Time.time > lastScanTime + timeBetweenScans)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.collider.tag == tileTag)
                {
                    if(rayHit.collider.GetComponent<TileBehavior>() != checkedTile)
                    {
                        cursor.GoToTile(rayHit.collider.GetComponent<TileBehavior>());
                        checkedTile = rayHit.collider.GetComponent<TileBehavior>();
                    }                    
                }
            }
            lastScanTime = Time.time;
        }
    }
}
