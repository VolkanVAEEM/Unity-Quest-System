using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestTarget
{
    public QuestGoalType goalType;
    public string targetName;
    public string displayName;
    public int requiredAmount;
    public int currentAmount;
}

[System.Serializable]
public class QuestRewardItem
{
    public Item item;
    public int amount;
}

[System.Serializable]
public class Quest
{
    public int questID;
    public int requiredQuestID;

    public string questName;
    public string questInfo;

    public int questRequiredLevel;

    public float rewardExp;
    public int rewardGold;
    public int rewardRuby;
    public List<QuestRewardItem> rewardItems = new List<QuestRewardItem>();

    public bool questCompleted;
    public bool questAccepted;
    public bool questListed;

    public QuestType questType;

    public List<QuestTarget> questTargets = new List<QuestTarget>();

    public Sprite questIcon;
    public Sprite questCollectIcon;
    public QuestRank questRank;

    public bool _delivered;

    public QuestNPC _questNPC;

    public int _dialogueIndex;
    public int _requiredTargetValue;
    public int _currentTargetValue;

    public Quest(int id, int requiredID, string name, string info, int requiredLevel, float exp, int gold, int ruby, 
                 List<QuestRewardItem> items, bool completed, bool accepted, bool listed, QuestType type,
                 List<QuestTarget> targets, QuestRank rank, bool delivered, QuestNPC questNPC, int dialogueIndex
,                int requiredTargetValue, int currentTargetValue)
    {
        questID = id;
        requiredQuestID = requiredID;
        questName = name;
        questInfo = info;
        questRequiredLevel = requiredLevel;
        rewardExp = exp;
        rewardGold = gold;
        rewardRuby = ruby;
        rewardItems = items;
        questCompleted = completed;
        questAccepted = accepted;
        questListed = listed;
        questType = type;
        questTargets = targets;
        questRank = rank;
        _delivered = delivered;
        _questNPC = questNPC;
        _dialogueIndex = dialogueIndex;
        _requiredTargetValue = requiredTargetValue;
        _currentTargetValue = currentTargetValue;

        questIcon = Resources.Load<Sprite>((6000 + id).ToString());
        questCollectIcon = Resources.Load<Sprite>((3913 + id).ToString());
     }

    public Quest()
    {

    }
}

public enum QuestType
{
    Main,
    Side,
    Daily,
    Weekly,
    Tutorial,
    None
}

public enum QuestGoalType
{
    Kill,
    Collect,
    Talk,
    Travel,
    ReachLevel,
    Build,
    Upgrade,
    Train,
    Capture,
    UpgradeBuilding,
    Produce,
    None
}
public enum QuestRank
{
    F,
    E,
    D,
    C,
    B,
    A,
    S,
    None
}
public enum QuestNPC
{
    Armorer,
    WeaponSmith,
    StableMaster,
    Jeweler,
    HighPriest,
    Magister,
    TheAuctioneer,
    WarehouseMan,
    MissingChild,
    HouseOfStars,
    None
}