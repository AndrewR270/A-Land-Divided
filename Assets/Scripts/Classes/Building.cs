using UnityEngine;

public abstract class Building
{
	public virtual string Name;
	public virtual string Description;
	public abstract BuildingText Text { get; }

	private int TIER = 0;
	public int Tier => TIER;

	private const int MIN_TIER = 0;
	private const int MAX_TIER = 6;

	public void setTier(int tier) { TIER = Mathf.Clamp(tier, MIN_TIER, MAX_TIER); }
	public bool hasNextTier() { return TIER < MAX_TIER; }

	private static readonly int[] UpgradeCosts = { 200, 400, 900, 1600, 2500, 3600, 0 };
	private static readonly int[] UpgradeTimes = { 2, 4, 6, 8, 10, 12, 0 };

	public int UpgradeCost => UpgradeCosts[TIER];
	public int UpgradeTime => UpgradeTimes[TIER];
}

public struct FarmsStats
{
	public int Surplus;
	public float Growth;

	public FarmsStats(int surplus, float growth)
	{
		Surplus = surplus;
		Growth = growth;
	}
}

public class Farms : Building
{
	public static readonly FarmsStats[] stats = {
		new FarmTierData(0, 0.0f),
		new FarmTierData(1, 0.6f),
		new FarmTierData(2, 0.5f),
		new FarmTierData(3, 0.4f),
		new FarmTierData(4, 0.4f),
		new FarmTierData(6, 0.3f),
		new FarmTierData(8, 0.3f),
		new FarmTierData(0, 0.0f)
	};

	public int Surplus => stats[TIER].Surplus;
	public float Growth => stats[TIER].Growth;

	public int NextSurplus => stats[TIER+1].Surplus;
	public float NextGrowth => stats[TIER+1].Growth;

	public override BuildingText Text => BuildingDescriptions.Farm;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public void getBenefits(int tier) 
	{
		$"<b>+{stats[tier].Surplus}</b>" + Text.Benefit1 + $"\n<b>+{stats[tier].Growth}</b>" + Text.Benefit2;
	}
}

public struct BarracksStats
{
	public int ReplenishTime;
	public float ExpGain;

	public BarracksStats(int replenishTime, float expGain)
	{
		ReplenishTime = replenishTime;
		ExpGain = expGain;
	}
}

public class Barracks : Building
{
	public static readonly BarracksStats[] stats = {
		new BarracksTierData(10, 0.00f),
		new BarracksTierData(9, 0.05f),
		new BarracksTierData(8, 0.10f),
		new BarracksTierData(7, 0.15f),
		new BarracksTierData(6, 0.20f),
		new BarracksTierData(5, 0.25f),
		new BarracksTierData(4, 0.30f),
		new BarracksTierData(10, 0.00f)
	};

	public int ReplenishTime => stats[TIER].ReplenishTime;
	public float ExpGain => stats[TIER].ExpGain;

	public int NextReplenishTime => stats[TIER+1].ReplenishTime;
	public float NextExpGain => stats[TIER+1].ExpGain;

	public override BuildingText Text => BuildingDescriptions.Barracks;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public void getBenefits(int tier) 
	{
		$"<b>{stats[tier].ReplenishTime}</b>" + Text.Benefit1 + $"\n<b>+{stats[tier].ExpGain * 100}</b>" + Text.Benefit2;
	}
}

public struct MarketsStats
{
	public float TradeBonus;
	public int TaxBonus;

	public MarketsStats(float tradeBonus, int taxBonus)
	{
		TradeBonus = tradeBonus;
		TaxBonus = taxBonus;
	}
}

public class Market : Building
{
	public static readonly MarketsStats[] stats = {
		new MarketTierData(0.00f, 0),
		new MarketTierData(0.02f, 1),
		new MarketTierData(0.04f, 1),
		new MarketTierData(0.06f, 2),
		new MarketTierData(0.08f, 2),
		new MarketTierData(0.10f, 3),
		new MarketTierData(0.12f, 3),
		new MarketTierData(0.00f, 0)
	};

	public int TradeBonus => stats[TIER].TradeBonus;
	public float TaxBonus => stats[TIER].TaxBonus;
	
	public int NextTradeBonus => stats[TIER+1].TradeBonus;
	public float NextTaxBonus => stats[TIER+1].TaxBonus;

	public override BuildingText Text => BuildingDescriptions.Markets;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public void getBenefits(int tier) 
	{
		$"<b>{stats[tier].TradeBonus * 100}</b>" + Text.Benefit1 + $"\n<b>+{stats[tier].TaxBonus}</b>" + Text.Benefit2;
	}
}

public struct PortStats
{
	public float FleetsSupported;
	public int ShipCost;

	public PortStats(float fleetsSupported, int shipCost)
	{
		FleetsSupported = fleetsSupported;
		ShipCost = shipCost;
	}
}

public class Port : Building
{
	public static readonly PortStats[] stats = {
		new PortTierData(0, 0),
		new PortTierData(1, 200),
		new PortTierData(2, 180),
		new PortTierData(3, 160),
		new PortTierData(4, 140),
		new PortTierData(5, 120),
		new PortTierData(6, 100),
		new PortTierData(0, 0)
	};

	public int FleetsSupported => stats[TIER].FleetsSupported;
	public float ShipCost => stats[TIER].ShipCost;
		
	public int NextFleetsSupported => stats[TIER+1].FleetsSupported;
	public float NextShipCost => stats[TIER+1].ShipCost;

	public override BuildingText Text => BuildingDescriptions.Port;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public void getBenefits(int tier) 
	{
		$"<b>{stats[tier].FleetsSupported}</b>" + Text.Benefit1 + $"\n<b>{stats[tier].ShipCost}</b>" + Text.Benefit2;
	}
}