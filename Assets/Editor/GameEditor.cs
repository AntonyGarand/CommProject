using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Game))]
public class MapEditor : Editor {

    public override void OnInspectorGUI()
    {
       Game game = target as Game;

		if (DrawDefaultInspector ()) {
			game.placeObjects();
		}

		if (GUILayout.Button("Build layout")) {
			game.placeObjects();
		}
    }
}
