using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDatas", menuName = "ScriptableObjects/MapDatas", order = 1)]
public class MapDatas : ScriptableObject
{
    public List<MapData> mapDataList;

    [System.Serializable]
    public class MapData
    {
        public string mapName;
        public List<GameObject> mapChunkList;
    }
}
