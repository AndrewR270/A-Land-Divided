using UnityEngine;

public class Contingent
{
    public string Name;             // Hoplites, Fyrd, etc.
    public Province Origin;         // where it was raised
    public Faction Owner;

    public int Experience;          // 0–6
    public int Attack;
    public int Defense;

    public Province CurrentLocation; // must be owned by faction
    public bool IsExile;             // if origin province is lost
}
