using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BoardMakerEditor : EditorWindow {

    //section references
    private Rect filePanel;
    private Rect drawingPanel;
    private Rect infoPanel;

    private Rect upperPanel;
    private Rect lowerPanel;

    //size handling vals
    private float sidePanelSizeRatio = 0.2f;
    private float centerPanelSizeRatio = 0.6f;

    //object and scripts refs
    private MapHandler map;
    private GameObject mapObject;

    [MenuItem("Professor Cat's Lab/Board Maker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BoardMakerEditor));
    }

    private void OnGUI()
    {
        DrawFilePanel();
        DrawDrawingPanel();
        DrawInfoPanel();
    }

    private void DrawFilePanel()
    {
        filePanel = new Rect(0, 0, position.width * sidePanelSizeRatio, position.height);

        GUILayout.BeginArea(filePanel);
        GUILayout.Label("File");
        map = (MapHandler)EditorGUILayout.ObjectField(map, typeof(MapHandler), false);
        GUILayout.EndArea();
    }

    private void DrawDrawingPanel()
    {
        drawingPanel = new Rect(position.width * sidePanelSizeRatio, 0, position.width * centerPanelSizeRatio, position.height);

        GUILayout.BeginArea(drawingPanel);
        GUILayout.Label("Map");
        GUILayout.EndArea();
    }

    private void DrawInfoPanel()
    {
        infoPanel = new Rect(position.width * (sidePanelSizeRatio + centerPanelSizeRatio), 0, position.width * sidePanelSizeRatio, position.height);

        GUILayout.BeginArea(infoPanel);
        GUILayout.Label("Info");
        GUILayout.EndArea();
    }
}
