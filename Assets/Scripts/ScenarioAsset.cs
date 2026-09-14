using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "ScenarioAsset", menuName = "ALD/Scenario")]
public class ScenarioAsset : ScriptableObject
{
    [Header("Identity")]
    public string scenarioID;

    [Header("Map Layers")]
    public Texture2D MapBorders;
    public Texture2D BaseMap;
    public Texture2D ColorMap;
    public Texture2D CityLayer;
    public Texture2D LabelLayer;

    [Header("Background Prefab")]
    public GameObject Background;
}
