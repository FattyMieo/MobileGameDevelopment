using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnDataList))]
public class SpawnDataListEditor : Editor
{
	SerializedProperty spawnDataProp;

	public override void OnInspectorGUI() //Override because there've been something drawn on the UI already
	{
		//serializedObject = CustomEditor we referred to
		serializedObject.Update();

		EditorGUILayout.LabelField("Spawn Data", EditorStyles.boldLabel);

		//Display Size of the LIST
		spawnDataProp = serializedObject.FindProperty("spawnDatas");
		EditorGUILayout.PropertyField(spawnDataProp.FindPropertyRelative("Array.size"));

		// Grid
		int win = Screen.width - 20 - 30 - 30 - 30 - 30;
		int w1 = (int)(win * 0.2f);
		int w2 = (int)(win * 0.5f);
		int w3 = (int)(win * 0.3f);

		// Draw labels above
		EditorGUILayout.BeginHorizontal();
//		EditorGUILayout.LabelField("Level", GUILayout.Width(40));
//		EditorGUILayout.LabelField("Enemy", GUILayout.Width(200));
//		EditorGUILayout.LabelField("Time To Spawn", GUILayout.Width(120));
		EditorGUILayout.LabelField("Level", GUILayout.Width(w1));
		EditorGUILayout.LabelField("Enemy", GUILayout.Width(w2));
		EditorGUILayout.LabelField("Time To Spawn", GUILayout.Width(w3));
		EditorGUILayout.EndHorizontal();

		for(int i = 0; i < spawnDataProp.arraySize; i++)
		{
			EditorGUILayout.BeginHorizontal();
//			EditorGUILayout.PropertyField(spawnDataProp.GetArrayElementAtIndex(i).FindPropertyRelative("level"), GUIContent.none, GUILayout.Width(40));
//			EditorGUILayout.PropertyField(spawnDataProp.GetArrayElementAtIndex(i).FindPropertyRelative("enemy"), GUIContent.none, GUILayout.Width(200));
//			EditorGUILayout.PropertyField(spawnDataProp.GetArrayElementAtIndex(i).FindPropertyRelative("timeUntilSpawn"), GUIContent.none, GUILayout.Width(120));
			EditorGUILayout.PropertyField(spawnDataProp.GetArrayElementAtIndex(i).FindPropertyRelative("level"), GUIContent.none, GUILayout.Width(w1));
			EditorGUILayout.PropertyField(spawnDataProp.GetArrayElementAtIndex(i).FindPropertyRelative("enemy"), GUIContent.none, GUILayout.Width(w2));
			EditorGUILayout.PropertyField(spawnDataProp.GetArrayElementAtIndex(i).FindPropertyRelative("timeUntilSpawn"), GUIContent.none, GUILayout.Width(w3));

			if(GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.MaxWidth(30))) AddEntry(i);
			if(GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.MaxWidth(30))) DeleteEntry(i);
			if(GUILayout.Button("▲", EditorStyles.miniButtonLeft, GUILayout.MaxWidth(30))) ShiftEntryUp(i);
			if(GUILayout.Button("▼", EditorStyles.miniButtonRight, GUILayout.MaxWidth(30))) ShiftEntryDown(i);
				
			EditorGUILayout.EndHorizontal();
		}

		serializedObject.ApplyModifiedProperties();
	}

	void AddEntry(int index)
	{
		spawnDataProp.InsertArrayElementAtIndex(index);
	}

	void DeleteEntry(int index)
	{
		bool choice = EditorUtility.DisplayDialog
		(
			"Warning",
			"Are you sure you want to delete?",
			"Yes",
			"No"
		);
		
		if(choice) spawnDataProp.DeleteArrayElementAtIndex(index);
	}

	void ShiftEntryUp(int index)
	{
		spawnDataProp.MoveArrayElement(index, index - 1);
	}

	void ShiftEntryDown(int index)
	{
		spawnDataProp.MoveArrayElement(index, index + 1);
	}
}
