using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "ScenarioAsset", menuName = "ALD/Scenario")]
public class ScenarioData : ScriptableObject
{
    [Header("Identity")]
    public string scenarioID;

    [Header("Map Layers")]
    public Texture2D mapBorders;
    public Texture2D baseMapImage;
    public Texture2D colorMap;
    public Texture2D cityLayerImage;
    public Texture2D labelLayerImage;

    [Header("Background Prefab")]
    public GameObject backgroundPrefab;

    [Header("Metadata")]
    public Color[] factionColors;
    public TextAsset regionListJSON;

    public List<Faction> Factions;
    public List<Province> Provinces;
    public List<SeaProvince> SeaProvinces;

    public int DifficultyMultiplier;
}
