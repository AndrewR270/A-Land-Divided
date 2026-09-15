using UnityEngine;

public class Contingent
{
    public string ID { get; private set; }

    public string OriginID;
    public string LocationID;

    public int Experience;
    public bool IsAlive = true;
    public bool IsExile;

    public string CustomName;

    public string DisplayName =>
        string.IsNullOrEmpty(CustomName)
            ? ScenarioManager.Instance.ScenarioText.unit_default
            : CustomName;

    public Faction Owner;

    public Contingent(string id) { ID = id; }
}
