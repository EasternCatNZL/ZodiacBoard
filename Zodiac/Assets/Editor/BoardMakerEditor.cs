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

    private Rect upperPanel;
    private Rect lowerPanel;

    //non static label text
    private string tileXText = "0";
    private string tileYText = "0";

    //non static number values

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
    private float spaceFromTop = 10.0f;

    //colour key
    private Color noneColour = new Color(255, 0, 0);
    private Color basicColour = new Color(0, 0, 255);

    ////buttons
    //private Button loadMapButton;

    //object and scripts refs
    private MapHandler map; //the map object that is selected
    private TileBehavior[,] board; //the current board object
    private List<TileBehavior> boardDetails; //list containing current board details
    private TileBehavior tile; //for handling tile behavior
    //private GameObject mapObject;

    [MenuItem("Professor Cat's Lab/Board Maker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BoardMakerEditor));
    }

    //works like start() or awake()
    void OnEnable()
    {

    }

    //updates whenever something happens with window <- value changed, button pressed, interactable clicked
    private void OnGUI()
    {
        DrawFilePanel();
        DrawDrawingPanel();
        DrawInfoPanel();
    }

    private void DrawFilePanel()
    {
        //define area
        filePanel = new Rect(0, 0, position.width * sidePanelSizeRatio, position.height);
        GUILayout.BeginArea(filePanel);
        GUILayout.Label("File");
        //object field for map handler
        map = (MapHandler)EditorGUILayout.ObjectField(map, typeof(MapHandler), false);
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
            Rect boardTile = new Rect(mapBaseX + boardDetails[i].information.xPos * tileWidth, mapBaseZ + boardDetails[i].information.zPos * tileHeight, tileWidth, tileHeight);
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, ColourTexture(boardDetails[i].properties.type));
            GUI.DrawTexture(boardTile, tex);
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

    private void DrawInfoPanel()
    {
        //define area
        infoPanel = new Rect(position.width * (sidePanelSizeRatio + centerPanelSizeRatio), 0, position.width * sidePanelSizeRatio, position.height);
        GUILayout.BeginArea(infoPanel);
        GUILayout.Label("Info");
        //labels for which tile is currently being viewed
        GUILayout.BeginHorizontal();
        GUILayout.Label("X: " + tileXText);
        GUILayout.Label("Y: " + tileYText);
        GUILayout.EndHorizontal();
        //create fields needed to display tilebehavior
        tileInformation.xPos = EditorGUILayout.IntField("X pos: ", tileInformation.xPos);
        tileInformation.zPos = EditorGUILayout.IntField("Z pos: ", tileInformation.zPos);
        tileType = (TileBehavior.TileType)EditorGUILayout.EnumPopup("Tile type", tileType);
        //end area
        GUILayout.EndArea();
    }
}
