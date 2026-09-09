using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/*

  A singleton class that manages the currently active scenario in the game.
  It handles loading scenario data and applying scenario specific assets.

*/

public class ScenarioManager : MonoBehaviour
{
  public static ScenarioManager Instance { get; private set; }

  public ScenarioData[] scenarioData;
  private Dictionary<string, ScenarioData> scenarios;
  public ScenarioData activeScenario;

  public string ScenarioID => activeScenario.scenarioID;
  public Texture2D ColorMap => activeScenario.colorMap;

  // Visual elements for the scenario map

  public RawImage mapBorders;
  public RawImage baseMap;
  public RawImage highlight;
  public RawImage cityLayer;
  public RawImage labelLayer;

  public Transform background;
  private GameObject activeBackground;

  void Awake()
  {
    Instance = this;
    scenarios = new Dictionary<string, ScenarioData>();
    foreach (var s in scenarioData) { scenarios[s.scenarioID] = s; }
  }

  public void LoadScenario(string id)
  {
    if (!scenarios.TryGetValue(id, out activeScenario)) { Debug.LogError($"Scenario not found."); return; }

    ColorMap.Load(ScenarioID);

    // Apply visual elements for the scenario map

    mapBorders.texture = activeScenario.mapBorders;
    baseMap.texture = activeScenario.baseMap;
    cityLayer.texture = activeScenario.cityLayer;
    labelLayer.texture = activeScenario.labelLayer;

    if (activeBackground != null) { Destroy(activeBackground); }
    activeBackground = Instantiate(activeScenario.background, background);
  }

  public Province GetProvinceByID(string id)
  {
    foreach (var p in activeScenario.Provinces)
      if (p.ProvinceID == id)
        return p;

    foreach (var s in activeScenario.SeaProvinces)
      if (s.ProvinceID == id)
        return s;

    Debug.LogError("Province ID not found: " + id);
    return null;
  }
}