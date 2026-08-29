using UnityEngine;

public class Army
{
    public Faction Owner;
    public Province Location;

    public List<Contingent> Contingents = new List<Contingent>();

    public bool HasMovedThisTurn;
}
