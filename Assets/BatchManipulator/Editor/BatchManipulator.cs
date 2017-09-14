using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BatchManipulator : EditorWindow
{
	//Editor Windows - Batch Manipulator
	//Custom Property Draw / Override Inspector

	string objectName = "";
	bool selectFoldout = true;
	bool posFoldout = true;

	Vector3 positionChange;

	[MenuItem("SuperTools/Batch Manipulator")]
	static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(BatchManipulator), false, "Batch Manipulator");
	}

	void OnGUI()
	{
		// Selection Tools
		selectFoldout = EditorGUILayout.Foldout(selectFoldout, "Selection Tool");
		if(selectFoldout)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.BeginHorizontal();
			objectName = EditorGUILayout.TextField("Object Name", objectName);
			if(GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.MaxWidth(50))) SelectChildObjects();
			EditorGUILayout.EndHorizontal();
			EditorGUI.indentLevel--;
		}

		// Positioning Tools
		posFoldout = EditorGUILayout.Foldout(posFoldout, "Positioning Tool");
		if(posFoldout)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.BeginHorizontal();
			positionChange = EditorGUILayout.Vector3Field("Positon Change", positionChange);
			if(GUILayout.Button("Move", EditorStyles.miniButton, GUILayout.MaxWidth(50))) MoveObjects();
			EditorGUILayout.EndHorizontal();
			EditorGUI.indentLevel--;
		}
	}

	void SelectChildObjects()
	{
		List<GameObject> objs = new List<GameObject>();

		// Get all child transforms
		Transform[] transforms = Selection.activeTransform.GetComponentsInChildren<Transform>();

		//Filter out the names and add the relevant transforms into a list
		foreach(Transform t in transforms)
		{
			if(t.name.Contains(objectName))
				objs.Add(t.gameObject);
		}
//		for(int i = 0; i < transforms.Length; i++)
//		{
//			if(transforms[i].name.Contains(objectName))
//				objs.Add(transforms[i].gameObject);
//		}

		//Converts the list into an array
		GameObject[] goArray = new GameObject[objs.Count];

		for(int i = 0; i < objs.Count; i++)
		{
			goArray[i] = objs[i];
		}

		//Pass thr array to active selected objects
		Selection.objects = goArray;
	}

	void MoveObjects()
	{
		Debug.Log("Moving Objects by " + positionChange);

		Transform[] trans = Selection.GetTransforms(SelectionMode.Unfiltered);

		for(int i = 0; i < trans.Length; i++)
		{
			trans[i].position += positionChange;
		}
	}
}
