using UnityEngine;

[System.Serializable]
public class Bonus
{
    // Numerical effects
    public int surplus;
    public float growth;
    public int replenish_time;
    public float exp_gain;
    public float trade_bonus;
    public int tax_bonus;
    public int fleets_supported;
    public int ship_cost;
    public int victory_points;
    public int stability_bonus;

    // Text fields
    public string name;
    public string benefit1;
    public string benefit2;
}
