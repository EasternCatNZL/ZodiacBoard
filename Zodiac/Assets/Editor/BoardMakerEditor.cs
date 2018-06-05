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
    private string newMapNameText = "New Map";
    private string tileXText = "0";
    private string tileZText = "0";

    //tags
    private string mapTag = "Map";

    //new tile handling vars
    TileBehavior newTile;
    private int newTileX = 0;
    private int newTileZ = 0;
    TileBehavior.TileType newTileType = TileBehavior.TileType.BASIC;
    TileBehavior.TileType fillTileType = TileBehavior.TileType.BASIC;

    //grid fill handling vars
    private int fromX = 0;
    private int fromZ = 0;
    private int toX = 0;
    private int toZ = 0;

    //non static enum values
    TileBehavior.TileType tileType;
    TileBehavior.TileInformation tileInformation;

    //size handling vals
    private float sidePanelSizeRatio = 0.2f;
    private float centerPanelSizeRatio = 0.6f;

    private int tileWidth = 20;
    private int tileHeight = 20;

    private int gapBetweenTiles = 5;

    private float buttonHeight = 20.0f;

    private float numberFieldHeight = 15.0f;

    //spacing values
    private int spaceFromTop = 20;

    //colour key
    private Color noneColour = new Color(255, 0, 0);
    private Color basicColour = new Color(0, 0, 255);
    private Color backColour = new Color(255, 255, 255);

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

    //scroll pos
    private Vector2 scrollPos = Vector2.zero;

    [MenuItem("Professor Cat's Lab/Board Maker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BoardMakerEditor));
    }

    //works like start() or awake()
    void OnEnable()
    {
        tile = new TileBehavior();
        newMapNameText = "New Map";
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
        GetMapDetails();
    }

    //get map if it exists
    private void GetMapDetails()
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
            map = newMap.AddComponent<MapHandler>();
            //set tag
            newMap.tag = mapTag;
        }
    }

    //handles drawing the map as a ui
    private void DrawDrawingPanel()
    {
        //define area
        drawingPanel = new Rect(position.width * sidePanelSizeRatio, 0, position.width * centerPanelSizeRatio, position.height);
        //define color of drawing panel
        Texture2D tex = new Texture2D(1, 1);
        Color[] color = new Color[1];
        color[0] = backColour;
        //tex.SetPixel(0, 0, ColourTexture(boardDetails[i].properties.type));
        tex.SetPixels(color);
        tex.Apply();
        GUI.DrawTexture(drawingPanel, tex);
        GUILayout.BeginArea(drawingPanel);
        //begin a scroll view inside rect
        scrollPos = GUILayout.BeginScrollView(scrollPos,GUILayout.Width(position.width * centerPanelSizeRatio),GUILayout.Height(position.height));
        GUILayout.Label("Map");
        //draw board inside drawing area if infomation exists
        if(boardDetails != null && boardDetails.Count > 0)
        {
            DrawMap();
        }
        //end scroll view
        GUILayout.EndScrollView();
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
            Rect boardTile = new Rect(/*mapBaseX + */(boardDetails[i].information.xPos * tileWidth) + (gapBetweenTiles * boardDetails[i].information.xPos), (mapBaseZ + boardDetails[i].information.zPos * tileHeight) + (gapBetweenTiles * boardDetails[i].information.zPos), tileWidth, tileHeight);
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
        GameObject dummyTile = new GameObject();
        TileBehavior createdTile = dummyTile.AddComponent<TileBehavior>();

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
        //if map exists
        if (map)
        {
            //after change in ui, check if new tile is valid 
            if (!map.CheckForSameInBoard(createdTile, 0))
            {
                tile.information.xPos = createdTile.information.xPos;
                tile.information.zPos = createdTile.information.zPos;
            }
        }
        tile.properties.type = (TileBehavior.TileType)EditorGUILayout.EnumPopup("Tile type", tile.properties.type);
        //button to remove tile
        if(GUILayout.Button("Remove this tile", GUILayout.Height(buttonHeight)))
        {
            RemoveTileButtonLogic();
        }
        //end area
        GUILayout.EndArea();
        //once done destroy the dummy
        DestroyImmediate(dummyTile);
    }

    //logic for remove tile logic
    private void RemoveTileButtonLogic()
    {
        tile.RemoveSelf();
        DestroyImmediate(tile.gameObject);
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
            NewMapButtonLogic();
        }
        //handle new tile stuff
        GUILayout.Label("New tile");
        newTileX = EditorGUILayout.IntField("X pos: ", newTileX);
        newTileZ = EditorGUILayout.IntField("Z pos: ", newTileZ);
        newTileType = (TileBehavior.TileType)EditorGUILayout.EnumPopup("Tile type", newTileType);
        //button to add new tile to current map
        if (GUILayout.Button("Add New Tile", GUILayout.Height(buttonHeight)))
        {
            NewTileButtonLogic();
        }
        //components for grid logic
        GUILayout.Label("Fill new grid");
        //two cols
        GUILayout.BeginHorizontal();
        //from col
        GUILayout.BeginVertical();
        GUILayout.Label("From:");
        fromX = EditorGUILayout.IntField(fromX);
        fromZ = EditorGUILayout.IntField(fromZ);
        GUILayout.EndVertical();
        //to col
        GUILayout.BeginVertical();
        GUILayout.Label("To:");
        toX = EditorGUILayout.IntField(toX);
        toZ = EditorGUILayout.IntField(toZ);
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        fillTileType = (TileBehavior.TileType)EditorGUILayout.EnumPopup("Tile type", fillTileType);
        //button to fill a grid
        if (GUILayout.Button("Fill grid", GUILayout.Height(buttonHeight)))
        {
            GridFillButtonLogic();
        }
        //end area
        GUILayout.EndArea();
    }

    //logic to make new map
    private void NewMapButtonLogic()
    {
        GameObject newMap = new GameObject("New Map");
        newMap.tag = mapTag;
        map = newMap.AddComponent<MapHandler>();
    }

    //logic to add new tile to current map
    private void NewTileButtonLogic()
    {
        //check that map exists
        if (map)
        {

            //create a gameobject to hold the tile
            GameObject newTileObject = new GameObject("Tile");
            //attach the tile to the object
            TileBehavior createdTile = newTileObject.AddComponent<TileBehavior>();
            //assign values to new tile
            createdTile.information.xPos = newTileX;
            createdTile.information.zPos = newTileZ;
            createdTile.properties.type = newTileType;
            //check that this tile does not already exist
            if (!map.CheckForSameInBoard(createdTile, 0))
            {
                //attach this new object to the maps object as child
                newTileObject.transform.parent = map.gameObject.transform;

                //add this new tile to the map
                map.AddTileToBoard(createdTile);

                //draw the map again
                GetMapDetails();
            }
            //if it does, destroy the object after comparison
            else
            {
                DestroyImmediate(newTileObject);
            }
        }
    }

    //logic to fill a grid area of the map
    private void GridFillButtonLogic()
    {
        //check that map exists
        if (map)
        {
            //check that values selected work
            if(toX > fromX && toZ > fromZ)
            {
                //for all x
                for(int i = fromX; i <= toX; i++)
                {
                    //for all z
                    for(int j = fromZ; j <= toZ; j++)
                    {
                        //create a gameobject to hold the tile
                        GameObject newTileObject = new GameObject("Tile");
                        //attach the tile to the object
                        TileBehavior createdTile = newTileObject.AddComponent<TileBehavior>();
                        //assign values to new tile
                        createdTile.information.xPos = i;
                        createdTile.information.zPos = j;
                        createdTile.properties.type = fillTileType;
                        //check that this tile does not already exist
                        if (!map.CheckForSameInBoard(createdTile, 0))
                        {
                            //attach this new object to the maps object as child
                            newTileObject.transform.parent = map.gameObject.transform;

                            //add this new tile to the map
                            map.AddTileToBoard(createdTile);

                            //draw the map again
                            GetMapDetails();
                        }
                        //if it does, destroy the object after comparison
                        else
                        {
                            DestroyImmediate(newTileObject);
                        }
                    }
                }
            }
        }
    }
}
