using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NewTileMakerEditor : ScriptableWizard {


    //[MenuItem("Professor Cat's Lab/New Tile Wizard")]
    public static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<NewTileMakerEditor>("Create new tile", "Add", "Close");
    }
}
