using UnityEngine;
using System.Collections.Generic;

public class SeaFactionPresence
{
    public string FactionID;

    public int Fleets;
    public int Merchants;

    public bool ArmyAtSea;

    public int ShipsInQueue;
    public int MerchantsInQueue;

    public SeaFactionPresence(string factionID)
    {
        FactionID = factionID;
    }
}

public class SeaProvince
{
    public string ID;
    public string Name;
    public Color RegionColor;

    // One entry per faction present in this sea region
    public List<SeaFactionPresence> FactionPresence = new List<SeaFactionPresence>();

    public SeaProvince(string id)
    {
        ID = id;
    }
}
