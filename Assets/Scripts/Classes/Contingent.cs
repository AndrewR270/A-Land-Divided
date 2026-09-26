using UnityEngine;

public class Contingent
{
    public string ID { get; private set; }

    public string OriginID;
    public string LocationID;

    public int Experience;

    public bool IsLevied;
    public bool IsExile;
    public bool IsAlive = true;

    public string CustomName;

    public string DisplayName =>
        string.IsNullOrEmpty(CustomName)
            ? ScenarioManager.Instance.ScenarioText.unit_default
            : CustomName;

    public Faction Owner;

    public Contingent(string id)
    {
        ID = id;
    }
}
