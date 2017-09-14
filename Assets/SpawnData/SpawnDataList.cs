using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu] // Create scriptable object from menu
public class SpawnDataList : ScriptableObject
{
	public List<SpawnData> spawnDatas;
}
