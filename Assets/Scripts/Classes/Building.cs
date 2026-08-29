using UnityEngine;

public abstract class Building
{
	public abstract string Name { get; }
	public abstract string Description { get; }
	public abstract BuildingText Text { get; }

	protected int TIER = 0;
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

public struct FarmsTierStats
{
	public int Surplus;
	public float Growth;

	public FarmsTierStats(int surplus, float growth)
	{
		Surplus = surplus;
		Growth = growth;
	}
}

public class Farms : Building
{
	public static readonly FarmsTierStats[] stats = {
		new FarmsTierStats(0, 0.0f),
		new FarmsTierStats(1, 0.6f),
		new FarmsTierStats(2, 0.5f),
		new FarmsTierStats(3, 0.4f),
		new FarmsTierStats(4, 0.4f),
		new FarmsTierStats(6, 0.3f),
		new FarmsTierStats(8, 0.3f),
		new FarmsTierStats(0, 0.0f)
	};

	public int Surplus => stats[TIER].Surplus;
	public float Growth => stats[TIER].Growth;

	public int NextSurplus => stats[TIER+1].Surplus;
	public float NextGrowth => stats[TIER+1].Growth;

	public override BuildingText Text => BuildingDescriptions.Farms;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public string getBenefits(int tier) 
	{
		return $"<b>+{stats[tier].Surplus}</b>" + Text.Benefit1 + $"\n<b>+{stats[tier].Growth}</b>" + Text.Benefit2;
	}
}

public struct BarracksTierStats
{
	public int ReplenishTime;
	public float ExpGain;

	public BarracksTierStats(int replenishTime, float expGain)
	{
		ReplenishTime = replenishTime;
		ExpGain = expGain;
	}
}

public class Barracks : Building
{
	public static readonly BarracksTierStats[] stats = {
		new BarracksTierStats(10, 0.00f),
		new BarracksTierStats(9, 0.05f),
		new BarracksTierStats(8, 0.10f),
		new BarracksTierStats(7, 0.15f),
		new BarracksTierStats(6, 0.20f),
		new BarracksTierStats(5, 0.25f),
		new BarracksTierStats(4, 0.30f),
		new BarracksTierStats(10, 0.00f)
	};

	public int ReplenishTime => stats[TIER].ReplenishTime;
	public float ExpGain => stats[TIER].ExpGain;

	public int NextReplenishTime => stats[TIER+1].ReplenishTime;
	public float NextExpGain => stats[TIER+1].ExpGain;

	public override BuildingText Text => BuildingDescriptions.Barracks;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public string getBenefits(int tier) 
	{
		return $"<b>{stats[tier].ReplenishTime}</b>" + Text.Benefit1 + $"\n<b>+{stats[tier].ExpGain * 100}</b>" + Text.Benefit2;
	}
}

public struct MarketsTierStats
{
	public float TradeBonus;
	public int TaxBonus;

	public MarketsTierStats(float tradeBonus, int taxBonus)
	{
		TradeBonus = tradeBonus;
		TaxBonus = taxBonus;
	}
}

public class Markets : Building
{
	public static readonly MarketsTierStats[] stats = {
		new MarketsTierStats(0.00f, 0),
		new MarketsTierStats(0.02f, 1),
		new MarketsTierStats(0.04f, 1),
		new MarketsTierStats(0.06f, 2),
		new MarketsTierStats(0.08f, 2),
		new MarketsTierStats(0.10f, 3),
		new MarketsTierStats(0.12f, 3),
		new MarketsTierStats(0.00f, 0)
	};

	public float TradeBonus => stats[TIER].TradeBonus;
	public int TaxBonus => stats[TIER].TaxBonus;
	
	public float NextTradeBonus => stats[TIER+1].TradeBonus;
	public int NextTaxBonus => stats[TIER+1].TaxBonus;

	public override BuildingText Text => BuildingDescriptions.Markets;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public string getBenefits(int tier) 
	{
		return $"<b>{stats[tier].TradeBonus * 100}</b>" + Text.Benefit1 + $"\n<b>+{stats[tier].TaxBonus}</b>" + Text.Benefit2;
	}
}

public struct PortTierStats
{
	public int FleetsSupported;
	public int ShipCost;

	public PortTierStats(int fleetsSupported, int shipCost)
	{
		FleetsSupported = fleetsSupported;
		ShipCost = shipCost;
	}
}

public class Port : Building
{
	public static readonly PortTierStats[] stats = {
		new PortTierStats(0, 0),
		new PortTierStats(1, 200),
		new PortTierStats(2, 180),
		new PortTierStats(3, 160),
		new PortTierStats(4, 140),
		new PortTierStats(5, 120),
		new PortTierStats(6, 100),
		new PortTierStats(0, 0)
	};

	public int FleetsSupported => stats[TIER].FleetsSupported;
	public int ShipCost => stats[TIER].ShipCost;
		
	public int NextFleetsSupported => stats[TIER+1].FleetsSupported;
	public int NextShipCost => stats[TIER+1].ShipCost;

	public override BuildingText Text => BuildingDescriptions.Port;

	public override string Name => Text.Name;
	public override string Description => Text.Description;

	public string getBenefits(int tier) 
	{
		return $"<b>{stats[tier].FleetsSupported}</b>" + Text.Benefit1 + $"\n<b>{stats[tier].ShipCost}</b>" + Text.Benefit2;
	}
}