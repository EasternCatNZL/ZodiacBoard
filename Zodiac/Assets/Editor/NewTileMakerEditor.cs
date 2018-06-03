using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NewTileMakerEditor : ScriptableWizard {

    //editor ref
    private static BoardMakerEditor boardEditor;

    //[MenuItem("Professor Cat's Lab/New Tile Wizard")]
    //public static void CreateWizard()
    //{
    //    ScriptableWizard.DisplayWizard<NewTileMakerEditor>("Create new tile", "Add", "Close");
    //}

    public static void CreateWizard(BoardMakerEditor board)
    {
        //set board
        boardEditor = board;
        ScriptableWizard.DisplayWizard<NewTileMakerEditor>("Create new tile", "Add", "Close");
    }
}
