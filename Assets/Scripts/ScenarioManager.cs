using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

/*

  A singleton class that manages the currently active scenario in the game.
  It handles loading scenario data and applying scenario specific assets.

*/

public class ScenarioManager : MonoBehaviour
{
  public static ScenarioManager Instance { get; private set; }

  // Scenario Asset

  public ScenarioAsset[] scenarioData;
  private Dictionary<string, ScenarioAsset> scenarios;
  public ScenarioAsset activeScenario;
  public static event System.Action<ScenarioAsset> ScenarioLoaded;

  // Scenario Information

  public string ScenarioID => activeScenario.scenarioID;

  public ScenarioText ScenarioText { get; set; }

  // Asset filepaths

  public string ScenarioPath => Path.Combine(Application.dataPath, "Scenarios", ScenarioID);
  public string DataPath => Path.Combine(ScenarioPath, "Data");
  public string TextPath => Path.Combine(ScenarioPath, "Text", SettingsManager.Language);

  // Visual elements for the scenario map

  public RawImage MapBorders;
  public RawImage BaseMap;
  public RawImage Highlight;
  public RawImage CityLayer;
  public RawImage LabelLayer;

  public Transform Background;
  private GameObject ActiveBackground;

  /*

    Scenario Instance, Data Loading, and Lookup methods.
    These essentially declare a static accessible instance for the scenario,
    as well as unpack stored JSON data.

  */

  void Awake()
  {
    Instance = this;
    scenarios = new Dictionary<string, ScenarioAsset>();
    foreach (var s in scenarioData) { scenarios[s.scenarioID] = s; }
  }

  public void LoadScenario(string id)
  {
    if (!scenarios.TryGetValue(id, out activeScenario)) { Debug.LogError($"Scenario not found."); return; }

    // Load Scenario JSON data
    
    Adjacency.Load();
    FactionColors.Load();
    ProvinceColors.Load();
    Bonuses.Load();

    ScenarioTextLoader.Load();
    FactionText.Load();
    ProvinceText.Load();
    BonusText.Load();
    BuildingText.Load();

    // Apply visual elements for the scenario map

    MapBorders.texture = activeScenario.MapBorders;
    BaseMap.texture = activeScenario.BaseMap;
    CityLayer.texture = activeScenario.CityLayer;
    LabelLayer.texture = activeScenario.LabelLayer;

    MapBorders.SetNativeSize();
    BaseMap.SetNativeSize();
    Highlight.SetNativeSize();
    CityLayer.SetNativeSize();
    LabelLayer.SetNativeSize();

    if (ActiveBackground != null) { Destroy(ActiveBackground); }
    ActiveBackground = Instantiate(activeScenario.Background, Background);

    ScenarioLoaded?.Invoke(activeScenario);
  }
}