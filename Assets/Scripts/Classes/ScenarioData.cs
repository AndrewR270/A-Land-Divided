using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioAsset", menuName = "ALD/Scenario")]
public class ScenarioAsset : ScriptableObject
{
    [Header("Identity")]
    public string scenarioName;       // "Archaic Greece"
    public int scenarioID;            // 0, 1, 2, etc.
    public string victoryPointName;

    [Header("Map Layers")]
    public Texture2D baseMapImage;
    public Texture2D highlightLayerImage;
    public Texture2D cityLayerImage;
    public Texture2D labelLayerImage;

    [Header("Background Prefab")]
    public GameObject backgroundPrefab;

    [Header("Metadata")]
    public Color[] factionColors;
    public TextAsset regionListJSON;

    public List<Faction> Factions;
    public List<Province> Provinces;
    public List<SeaRegion> SeaRegions;

    public int DifficultyMultiplier; // 1x, 1.5x, 2x
}
