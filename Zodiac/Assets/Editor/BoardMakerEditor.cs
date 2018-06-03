using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEditor;

public class BoardMakerEditor : EditorWindow {

    //section references
    private Rect filePanel;
    private Rect drawingPanel;
    private Rect infoPanel;
    private Rect newPanel;

    private Rect upperPanel;
    private Rect lowerPanel;

    //non static label text
    private string newMapNameText = "";
    private string tileXText = "0";
    private string tileZText = "0";

    //new tile handling vars
    TileBehavior newTile;
    private int newTileX = 0;
    private int newTileZ = 0;
    TileBehavior.TileType newTileType = TileBehavior.TileType.BASIC;

    //non static enum values
    TileBehavior.TileType tileType;
    TileBehavior.TileInformation tileInformation;

    //size handling vals
    private float sidePanelSizeRatio = 0.2f;
    private float centerPanelSizeRatio = 0.6f;

    private float tileWidth = 5.0f;
    private float tileHeight = 5.0f;

    private float buttonHeight = 20.0f;

    private float numberFieldHeight = 15.0f;

    //spacing values
    private int spaceFromTop = 20;

    //colour key
    private Color noneColour = new Color(255, 0, 0);
    private Color basicColour = new Color(0, 0, 255);

    ////buttons
    //private Button loadMapButton;

    //object and scripts refs
    private MapHandler map; //the map object that is selected
    private TileBehavior[,] board; //the current board object
    private List<TileBehavior> boardDetails; //list containing current board details
    private TileBehavior tile/* = new TileBehavior()*/; //for handling tile behavior
    //private GameObject mapObject;

    //Event handler
    Event thingshappenin;

    [MenuItem("Professor Cat's Lab/Board Maker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BoardMakerEditor));
    }

    //works like start() or awake()
    void OnEnable()
    {
        tile = new TileBehavior();
    }

    //updates whenever something happens with window <- value changed, button pressed, interactable clicked
    private void OnGUI()
    {
        //set up event
        thingshappenin = Event.current;

        //draw panels
        DrawFilePanel();
        DrawDrawingPanel();
        DrawInfoPanel();
        DrawNewPanel();
    }

    //draws panel used to handle loading and saving map
    private void DrawFilePanel()
    {
        //define area
        filePanel = new Rect(0, 0, position.width * sidePanelSizeRatio, position.height);
        GUILayout.BeginArea(filePanel);
        GUILayout.Label("File");
        //object field for map handler
        map = (MapHandler)EditorGUILayout.ObjectField(map, typeof(MapHandler), true);
        //begin horizontal area
        GUILayout.BeginHorizontal();
        //button to load map from map handler object
        if(GUILayout.Button("Load map", GUILayout.Height(buttonHeight)))
        {
            LoadMapButtonLogic();
        }
        //button to save what has changed in the editor back into object
        if(GUILayout.Button("Save map", GUILayout.Height(buttonHeight)))
        {
            SaveMapButtonLogic();
        }
        GUILayout.EndHorizontal();
        //map name
        newMapNameText = EditorGUILayout.DelayedTextField("New map name", newMapNameText);
        //button to deploy map to scene
        if(GUILayout.Button("Deploy map", GUILayout.Height(buttonHeight)))
        {
            DeployMapButtonLogic();
        }
        //end area
        GUILayout.EndArea();
    }

    //logic for load map button
    private void LoadMapButtonLogic()
    {
        //check that map exists
        if (map)
        {
            board = map.GetBoard();
            boardDetails = map.GetBoardDetails();
            //draw the board to drawing panel
        }
    }

    //logic for save map button
    private void SaveMapButtonLogic()
    {

    }

    //logic for map deploy button
    private void DeployMapButtonLogic()
    {
        //check that map exists
        if (map)
        {
            //create a gameobject and attach map to it
            GameObject newMap = new GameObject(newMapNameText);
            //attach the map to it
            newMap.AddComponent<MapHandler>();
            
        }
    }

    //handles drawing the map as a ui
    private void DrawDrawingPanel()
    {
        //define area
        drawingPanel = new Rect(position.width * sidePanelSizeRatio, 0, position.width * centerPanelSizeRatio, position.height);
        GUILayout.BeginArea(drawingPanel);
        GUILayout.Label("Map");
        //draw board inside drawing area if infomation exists
        if(boardDetails != null && boardDetails.Count > 0)
        {
            DrawMap();
        }
        //end area
        GUILayout.EndArea();
    }

    //map drawing function
    private void DrawMap()
    {
        //get map start pos
        float mapBaseX = position.width * sidePanelSizeRatio;
        float mapBaseZ = spaceFromTop;

        //for all contents in details list
        for(int i = 0; i < boardDetails.Count; i++)
        {
            //get a rect
            Rect boardTile = new Rect(/*mapBaseX + */boardDetails[i].information.xPos * tileWidth, mapBaseZ + boardDetails[i].information.zPos * tileHeight, tileWidth, tileHeight);
            Texture2D tex = new Texture2D(1, 1);
            Color[] color = new Color[1];
            color[0] = ColourTexture(boardDetails[i].properties.type);
            //tex.SetPixel(0, 0, ColourTexture(boardDetails[i].properties.type));
            tex.SetPixels(color);
            tex.Apply();
            GUI.DrawTexture(boardTile, tex);

            //set event for if clicked on
            if(thingshappenin.type == EventType.MouseDown && boardTile.Contains(thingshappenin.mousePosition))
            {
                //pass this tile's information over to info panel
                tileXText = boardDetails[i].information.xPos.ToString();
                tileZText = boardDetails[i].information.zPos.ToString();
                tileType = boardDetails[i].properties.type;

                //set current tile ref to this tile
                tile = boardDetails[i];

                //do event
                thingshappenin.Use();
            }
        }
    }

    //get a color to represent tile
    private Color ColourTexture(TileBehavior.TileType type)
    {
        Color colour;
        switch (type)
        {
            case TileBehavior.TileType.NONE:
                colour = noneColour;
                break;
            case TileBehavior.TileType.BASIC:
                colour = basicColour;
                break;
            default:
                colour = noneColour;
                break;
        }
        return colour;
    }

    //handles drawing components used to examine and alter tiles
    private void DrawInfoPanel()
    {
        //set up vars to change tile values if valid
        TileBehavior createdTile = new TileBehavior();

        //define area
        infoPanel = new Rect(position.width * (sidePanelSizeRatio + centerPanelSizeRatio), 0, position.width * sidePanelSizeRatio, position.height * 0.5f);
        GUILayout.BeginArea(infoPanel);
        GUILayout.Label("Info");
        //labels for which tile is currently being viewed
        GUILayout.BeginHorizontal();
        GUILayout.Label("X: " + tileXText);
        GUILayout.Label("Z: " + tileZText);
        GUILayout.EndHorizontal();
        //create fields needed to display tilebehavior
        createdTile.information.xPos = EditorGUILayout.DelayedIntField("X pos: ", tile.information.xPos);
        createdTile.information.zPos = EditorGUILayout.DelayedIntField("Z pos: ", tile.information.zPos);
        //after change in ui, check if new tile is valid 
        if (!tile.CheckIfSame(createdTile))
        {
            tile.information.xPos = createdTile.information.xPos;
            tile.information.zPos = createdTile.information.zPos;
        }
        createdTile.properties.type = (TileBehavior.TileType)EditorGUILayout.EnumPopup("Tile type", tile.properties.type);
        //end area
        GUILayout.EndArea();
    }

    private void DrawNewPanel()
    {
        //define area
        newPanel = new Rect(position.width * (sidePanelSizeRatio + centerPanelSizeRatio), position.height * 0.5f, position.width * sidePanelSizeRatio, position.height * 0.5f);
        //new area
        GUILayout.BeginArea(newPanel);
        //button for new map
        if(GUILayout.Button("Make new map", GUILayout.Height(buttonHeight)))
        {

        }
        //handle new tile stuff
        GUILayout.Label("New tile");
        newTileX = EditorGUILayout.DelayedIntField("X pos: ", newTileX);
        newTileZ = EditorGUILayout.DelayedIntField("Z pos: ", newTileZ);
        newTileType = (TileBehavior.TileType)EditorGUILayout.EnumPopup("Tile type", newTileType);
        //button to add new tile to current map
        if (GUILayout.Button("Add New Tile", GUILayout.Height(buttonHeight)))
        {
            NewTileButtonLogic();
        }
        //end area
        GUILayout.EndArea();
    }

    //logic to make new map
    private void NewMapButtonLogic()
    {
        map = new MapHandler();
    }

    //logic to add new tile to current map
    private void NewTileButtonLogic()
    {
        //check that map exists
        if (map)
        {
            //make a new tile
            TileBehavior createdTile = new TileBehavior();
            //assign values to new tile
            createdTile.information.xPos = newTileX;
            createdTile.information.zPos = newTileZ;
            createdTile.properties.type = newTileType;
            //check that this tile does not already exist
            if (!map.CheckForSameInBoard(createdTile, 0))
            {
                //add this new tile to the map
                map.AddTileToBoard(createdTile);
            }
        }
    }
}
