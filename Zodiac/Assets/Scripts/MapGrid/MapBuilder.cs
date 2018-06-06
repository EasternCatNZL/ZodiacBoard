using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

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
}
