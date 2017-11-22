using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
	public UnitSelectionController unitSelect;
	public Text selectText;
	public Image selectButton;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnGUI()
	{
		switch(unitSelect.selectionMode)
		{
			case SelectionMode.ClusterMode:
				selectText.text = "Cluster";
				selectButton.color = Color.red * Color.white;
				break;
			case SelectionMode.FormationMode:
				selectText.text = "Formation";
				selectButton.color = Color.green * Color.white;
				break;
		}
	}
}
