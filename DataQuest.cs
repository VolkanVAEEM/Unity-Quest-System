using UnityEngine;
using System.Collections.Generic;
public class DataQuest : MonoBehaviour
{
    public dataItem dataItem;
    public List<Quest> questsOfStructures;
    public List<Quest> questsOfSoldiers;
    public List<Quest> produceOfResource;
    public List<Quest> questOfLevels;
    public List<Quest> questsMH;
    public List<Quest> constantQuests;
    private int structureIndex = 2;
    private int questIDParameter = 1;

    int[] countOfTroops =
    {
        10,
        50,
        200,
        1000,
        5000,
        10000,
        30000,
        50000
    };
    int[] countOfResources =
    {
        200,
        1000,
        5000,
        10000,
        50000,
        200000,
        500000,
        1000000
    };
    public Quest AddQuest(int id)
    {
        Quest newQuest = null;
        for (int i = 0; i < constantQuests.Count; i++)
        {
            if (constantQuests[i].questID == id)
            {
                newQuest = constantQuests[i];
            }
        }
        return newQuest;
    }

    public void StructureUpgradeQuest(string nameOfStructure, int structureStartLevel)
    {
        for (int i = structureStartLevel; i < 8; i++)
        {
            questsOfStructures.Add(new Quest(questIDParameter,
                                   i,
                                   "Upgrade the " + nameOfStructure + " to Level " + (i + 1).ToString(),
                                   "Improve your city by upgrading the " + nameOfStructure + " to Level " + (i + 1).ToString(),
                                   0 + ((i) * 1),
                                   500f * ((i + 1) * 1 * structureIndex),
                                   100 * ((i + 1) * 1 * structureIndex),
                                   0,
                                   new List<QuestRewardItem>()
                                   {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(3),
                                       amount = 1
                                   }
                                   },
                                   false,
                                   false,
                                   false,
                                   QuestType.Main,
                                   new List<QuestTarget>()
                                   {
                                        new QuestTarget
                                        {
                                            goalType = QuestGoalType.UpgradeBuilding,
                                            targetName = nameOfStructure,
                                            displayName = nameOfStructure,
                                            requiredAmount = (i + 1),
                                            currentAmount = 0
                                        }
                                   },
                                   QuestRank.F,
                                   false,
                                   QuestNPC.None,
                                   0,
                                   (i + 1),
                                   0));
            questIDParameter++;
        }
    }

    public void TrainSoldierQuest(string nameOfSoldierType)
    {
        for (int i = 0; i < 8; i++)
        {
            questsOfSoldiers.Add(new Quest(questIDParameter,
                                   i,
                                   "Train " + (countOfTroops[i]).ToString() + " " + nameOfSoldierType,
                                   "Our army is preparing for future battles. Train the required number of troops to strengthen our military forces",
                                   0 + ((i) * 1),
                                   500f * ((i + 1) * 1 * structureIndex),
                                   100 * ((i + 1) * 1 * structureIndex),
                                   0,
                                   new List<QuestRewardItem>()
                                   {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(4),
                                       amount = 1
                                   }
                                   },
                                   false,
                                   false,
                                   false,
                                   QuestType.Main,
                                   new List<QuestTarget>()
                                   {
                                        new QuestTarget
                                        {
                                            goalType = QuestGoalType.Train,
                                            targetName = nameOfSoldierType,
                                            displayName = nameOfSoldierType,
                                            requiredAmount = (countOfTroops[i]),
                                            currentAmount = 0
                                        }
                                   },
                                   QuestRank.F,
                                   false,
                                   QuestNPC.None,
                                   0,
                                   (countOfTroops[i]),
                                   0));
            questIDParameter++;
        }
    }
    public void ResourceProduceQuest(string resourceName)
    {
        for (int i = 0; i < 8; i++)
        {
            produceOfResource.Add(new Quest(questIDParameter,
                                   i,
                                   "Produce " + countOfResources[i].ToString() + " units of " + resourceName,
                                   "Produce the required amount of " + resourceName + " to support your city's growth and development.",
                                   0 + ((i) * 1),
                                   500f * ((i + 1) * 1 * structureIndex),
                                   100 * ((i + 1) * 1 * structureIndex),
                                   0,
                                   new List<QuestRewardItem>()
                                   {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(6),
                                       amount = 1
                                   }
                                   },
                                   false,
                                   false,
                                   false,
                                   QuestType.Main,
                                   new List<QuestTarget>()
                                   {
                                        new QuestTarget
                                        {
                                            goalType = QuestGoalType.Produce,
                                            targetName = "Collect" + resourceName,
                                            displayName = "Collect" + resourceName,
                                            requiredAmount = countOfResources[i],
                                            currentAmount = 0
                                        }
                                   },
                                   QuestRank.F,
                                   false,
                                   QuestNPC.None,
                                   0,
                                   countOfResources[i],
                                   0));
            questIDParameter++;
        }
    }
    private void Awake()
    {//---------------------------------------------------------------------------
        questsOfStructures.Add(new Quest(0,
                               0,
                               "",
                               "",
                               0,
                               0f,
                               0,
                               0,
                               new List<QuestRewardItem>(),
                               false,
                               false,
                               false,
                               QuestType.None,
                               new List<QuestTarget>(),
                               QuestRank.None,
                               false,
                               QuestNPC.None,
                               0,
                               0,
                               0));

        StructureUpgradeQuest("Palace", 0);
        StructureUpgradeQuest("Castle", 1);


        questsOfSoldiers.Add(new Quest(0,
                               0,
                               "",
                               "",
                               0,
                               0f,
                               0,
                               0,
                               new List<QuestRewardItem>(),
                               false,
                               false,
                               false,
                               QuestType.None,
                               new List<QuestTarget>(),
                               QuestRank.None,
                               false,
                               QuestNPC.None,
                               0,
                               0,
                               0));

        TrainSoldierQuest("Infantry");
        TrainSoldierQuest("Archer");


        produceOfResource.Add(new Quest(0,
                               0,
                               "",
                               "",
                               0,
                               0f,
                               0,
                               0,
                               new List<QuestRewardItem>(),
                               false,
                               false,
                               false,
                               QuestType.None,
                               new List<QuestTarget>(),
                               QuestRank.None,
                               false,
                               QuestNPC.None,
                               0,
                               0,
                               0));

        ResourceProduceQuest("Grain");
        ResourceProduceQuest("Iron");

        questOfLevels.Add(new Quest(0,
                               0,
                               "",
                               "",
                               0,
                               0f,
                               0,
                               0,
                               new List<QuestRewardItem>(),
                               false,
                               false,
                               false,
                               QuestType.None,
                               new List<QuestTarget>(),
                               QuestRank.None,
                               false,
                               QuestNPC.None,
                               0,
                               0,
                               0));

        questOfLevels.Add(new Quest(questIDParameter,
                               0,
                               "Reach Level 77",
                               "Continue your journey and reach level 77 to prove your growing strength.",
                               1,
                               10000f,
                               1000,
                               0,
                               new List<QuestRewardItem>()
                               {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(901),
                                       amount = 1
                                   }
                               },
                               false,
                               false,
                               false,
                               QuestType.Side,
                               new List<QuestTarget>()
                               {
                                  new QuestTarget
                                  {
                                      goalType = QuestGoalType.ReachLevel,
                                      targetName = "ReachLevel77",
                                      displayName = "Reach Level 77",
                                      requiredAmount = 77,
                                      currentAmount = 0
                                  }
                               },
                               QuestRank.C,
                               true,
                               QuestNPC.None,
                               0,
                               1,
                               0));
        questIDParameter++;
        //---------------------------------------------------------------------------
        constantQuests.Add(new Quest(0,
                               0,
                               "",
                               "",
                               0,
                               0f,
                               0,
                               0,
                               new List<QuestRewardItem>(),
                               false,
                               false,
                               false,
                               QuestType.None,
                               new List<QuestTarget>(),
                               QuestRank.None,
                               false,
                               QuestNPC.None,
                               0,
                               0,
                               0));

        constantQuests.Add(new Quest(1,
                               0,
                               "Shadows Among the Trees",
                               "The souls of those who committed great sins in life can never find peace after death. Cursed to be reborn " +
                               "in the bodies of wild beasts, they are condemned to endure endless suffering. As you draw near, you can " +
                               "sense the dark energy surrounding them, allowing you to distinguish these cursed souls from ordinary " +
                               "creatures. Defeat these cursed beasts and free their souls from their eternal punishment.",
                               1,
                               2000f,
                               200,
                               0,
                               new List<QuestRewardItem>()
                               {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(3),
                                       amount = 1
                                   }
                               },
                               false,
                               false,
                               false,
                               QuestType.Main,
                               new List<QuestTarget>()
                               {
                                  new QuestTarget
                                  {
                                      goalType = QuestGoalType.Kill,
                                      targetName = "BlackLeopard",
                                      displayName = "Black Leopards",
                                      requiredAmount = 10,
                                      currentAmount = 0
                                  }
                               },
                               QuestRank.F,
                               false,
                               QuestNPC.HighPriest,
                               2,
                               1,
                               0));

        constantQuests.Add(new Quest(4,
                               0,
                               "Go to the House of Stars",
                               "Travel to the House of Stars and search for the missing child.",
                               1,
                               12500f,
                               1050,
                               0,
                               new List<QuestRewardItem>()
                               {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(1541),
                                       amount = 1
                                   }
                               },
                               false,
                               false,
                               false,
                               QuestType.Main,
                               new List<QuestTarget>()
                               {
                                  new QuestTarget
                                  {
                                      goalType = QuestGoalType.Travel,
                                      targetName = "HouseOfStars",
                                      displayName = "Go to the House of Stars.",
                                      requiredAmount = 1,
                                      currentAmount = 0
                                  },
                                  new QuestTarget
                                  {
                                      goalType = QuestGoalType.Talk,
                                      targetName = "MissingChild",
                                      displayName = "Find the Missing Child.",
                                      requiredAmount = 1,
                                      currentAmount = 0
                                  }
                               },
                               QuestRank.D,
                               false,
                               QuestNPC.Armorer,
                               4,
                               2,
                               0));

        //------------------------------------------------------------------
        questsMH.Add(new Quest(0, 
                               0, 
                               "", 
                               "", 
                               0, 
                               0f, 
                               0, 
                               0, 
                               new List<QuestRewardItem>(), 
                               false, 
                               false, 
                               false, 
                               QuestType.None, 
                               new List<QuestTarget>(), 
                               QuestRank.None,
                               true,
                               QuestNPC.None,
                               0,
                               1,
                               0));

        questsMH.Add(new Quest(1,
                               0,
                               "Food for the People - 2",
                               "The city's food stores are running low, and the people are beginning to worry. Venture into the " +
                               "wilderness and hunt deer to provide fresh meat for the families before shortages become a crisis.",
                               1,
                               3000f,
                               400,
                               0,
                               new List<QuestRewardItem>()
                               {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(101),
                                       amount = 1
                                   },
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(6),
                                       amount = 4
                                   }
                               },
                               false,
                               false,
                               false,
                               QuestType.Side,
                               new List<QuestTarget>()
                               {
                                  new QuestTarget
                                  {
                                      goalType = QuestGoalType.Kill,
                                      targetName = "GazelleOrange",
                                      displayName = "Orange Gazelles",
                                      requiredAmount = 20,
                                      currentAmount = 0
                                  }
                               },
                               QuestRank.E,
                               true,
                               QuestNPC.None,
                               0,
                               1,
                               0));

        questsMH.Add(new Quest(4,
                               0,
                               "Collect 20 Black Leopard Fangs",
                               "Black leopards roam the dark forests. Hunt them and collect their sharp fangs as proof.",
                               1,
                               15000f,
                               1600,
                               0,
                               new List<QuestRewardItem>()
                               {
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(223),
                                       amount = 1
                                   },
                                   new QuestRewardItem
                                   {
                                       item = dataItem.AddItemForQuest(423),
                                       amount = 1
                                   }
                               },
                               false,
                               false,
                               false,
                               QuestType.Side,
                               new List<QuestTarget>()
                               {
                                  new QuestTarget
                                  {
                                      goalType = QuestGoalType.Collect,
                                      targetName = "BlackLeopardFang",
                                      displayName = "Black Leopard Fangs",
                                      requiredAmount = 20,
                                      currentAmount = 0
                                  }
                               },
                               QuestRank.B,
                               true,
                               QuestNPC.None,
                               0,
                               1,
                               0));
    }
}
