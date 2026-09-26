using System.Collections.Generic;
using UnityEngine;
using static SoldierTroop;

public class QuestProgressManager: MonoBehaviour
{
    public NPCClick npcClick;
    public CharacterMovement characterMovement;
    public static QuestProgressManager Instance;
    public QuestManager questManager;
    public DataDialogue dataDialogue;
    public DataQuest dataQuest;

    private void Awake()
    {
        Instance = this;
    }
    public bool NPCSituation()
    {
        return npcClick.clickedNPCTF = true;
    }
    public void RewardGoldRubyEx(int i)
    {
        if (questManager.acceptQuestList[i].rewardGold != 0)
            characterMovement.characterGold += questManager.acceptQuestList[i].rewardGold;
        if (questManager.acceptQuestList[i].rewardExp != 0)
            characterMovement.characterExp += questManager.acceptQuestList[i].rewardExp;
        if (questManager.acceptQuestList[i].rewardRuby != 0)
            GameResources.Instance.ruby += questManager.acceptQuestList[i].rewardRuby;
    }
    public void RewardItems(int i)
    {
        for (int j = 0; j < questManager.acceptQuestList[i].rewardItems.Count; j++)
        {
            if (questManager.acceptQuestList[i].rewardItems[j] != null &&
                questManager.acceptQuestList[i].rewardItems[j].item != null)
                characterMovement.inventory.AddItem(questManager.acceptQuestList[i].rewardItems[j].item.itemId,
                questManager.acceptQuestList[i].rewardItems[j].amount);
        }
    }
    public void RewardGoldRubyExStructure(int i, List<Quest> questListName)
    {
        if (questListName[i].rewardGold != 0)
            characterMovement.characterGold += questListName[i].rewardGold;
        if (questListName[i].rewardExp != 0)
            characterMovement.characterExp += questListName[i].rewardExp;
        if (questListName[i].rewardRuby != 0)
            GameResources.Instance.ruby += questListName[i].rewardRuby;
    }
    public void RewardItemsStructure(int i, List<Quest> questListName)
    {
        for (int j = 0; j < questListName[i].rewardItems.Count; j++)
        {
            if (questListName[i].rewardItems[j] != null &&
                questListName[i].rewardItems[j].item != null)
                characterMovement.inventory.AddItem(questListName[i].rewardItems[j].item.itemId,
                questListName[i].rewardItems[j].amount);
        }
    }
    public void ClaimConstantQuest(int i)
    {
        RewardGoldRubyEx(i);
        characterMovement.ExpointAndLevelUpdateFunc();
        RewardItems(i);
    }
    public void CompletedTravelAndTalkQuest(int talkedIndex = 0)
    {
        if (questManager.acceptQuestList.Count == 0) return;
        for (int i = questManager.acceptQuestList.Count - 1; i >= 0; i--)
        {
            for (int k = 0; k < questManager.acceptQuestList[i].questTargets.Count; k++)
            {
                if (questManager.acceptQuestList[i]._requiredTargetValue == questManager.acceptQuestList[i]._currentTargetValue &&
                    questManager.acceptQuestList[i]._currentTargetValue != 0 &&
                    questManager.acceptQuestList[i]._delivered)
                {
                    RewardGoldRubyEx(i);
                    questManager.acceptQuestValue--;

                    questManager.acceptQuestList[i].questCompleted = true;

                    RewardItems(i);

                    questManager.acceptQuestList.Remove(questManager.acceptQuestList[i]);
                }
                else if (questManager.acceptQuestList[i]._requiredTargetValue == questManager.acceptQuestList[i]._currentTargetValue &&
                         questManager.acceptQuestList[i]._currentTargetValue != 0 &&
                        !questManager.acceptQuestList[i]._delivered)    
                {
                    questManager.acceptQuestList[i].questCompleted = true;
                }
                if (npcClick.typeOfNearNPC.ToString() == questManager.acceptQuestList[i].questTargets[k].targetName &&
                    questManager.acceptQuestList[i].questTargets[k].currentAmount == 0 &&
                    questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Travel &&
                    questManager.acceptQuestList[i]._delivered)
                {
                    if (questManager.acceptQuestList[i]._requiredTargetValue == 1)
                    {
                        RewardGoldRubyEx(i);
                        questManager.acceptQuestValue--;

                        questManager.acceptQuestList[i].questCompleted = true;

                        RewardItems(i);

                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        questManager.acceptQuestList.Remove(questManager.acceptQuestList[i]);
                    }
                    else
                    {
                        string talkedNPC = npcClick.typeOfNearNPC.ToString();

                        if (questManager.acceptQuestList[i].questTargets[k].targetName != talkedNPC)
                            continue;

                        questManager.acceptQuestList[i]._currentTargetValue++;
                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        return;
                    }
                }
                else if (npcClick.typeOfNearNPC.ToString() == questManager.acceptQuestList[i].questTargets[k].targetName &&
                    questManager.acceptQuestList[i].questTargets[k].currentAmount == 0 &&
                    questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Travel &&
                    !questManager.acceptQuestList[i]._delivered)
                {
                    if (questManager.acceptQuestList[i]._requiredTargetValue == 1)
                    {
                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        questManager.acceptQuestList[i].questCompleted = true;
                    }
                    else
                    {
                        string talkedNPC = npcClick.typeOfNearNPC.ToString();

                        if (questManager.acceptQuestList[i].questTargets[k].targetName != talkedNPC)
                            continue;

                        questManager.acceptQuestList[i]._currentTargetValue++;
                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        return;
                    }
                }
                if (dataDialogue.dialogues[talkedIndex].talked &&
                    questManager.acceptQuestList[i].questTargets[k].currentAmount == 0 &&
                    questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Talk &&
                    talkedIndex == npcClick.dialogueScript.dialugueIndex &&
                    questManager.acceptQuestList[i]._delivered)
                {
                    if (questManager.acceptQuestList[i]._requiredTargetValue == 1)
                    {
                        RewardGoldRubyEx(i);
                        questManager.acceptQuestValue--;

                        questManager.acceptQuestList[i].questCompleted = true;

                        RewardItems(i);

                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        questManager.acceptQuestList.Remove(questManager.acceptQuestList[i]);
                    }
                    else
                    {
                        string talkedNPC = npcClick.typeOfNearNPC.ToString();

                        if (questManager.acceptQuestList[i].questTargets[k].targetName != talkedNPC)
                            continue;

                        questManager.acceptQuestList[i]._currentTargetValue++;
                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        return;
                    }
                }
                else if (dataDialogue.dialogues[talkedIndex].talked &&
                    questManager.acceptQuestList[i].questTargets[k].currentAmount == 0 &&
                    questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Talk &&
                    talkedIndex == npcClick.dialogueScript.dialugueIndex &&
                    !questManager.acceptQuestList[i]._delivered)
                {
                    if (questManager.acceptQuestList[i]._requiredTargetValue == 1)
                    {
                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        questManager.acceptQuestList[i].questCompleted = true;
                        return;
                    }
                    else
                    {
                        string talkedNPC = npcClick.typeOfNearNPC.ToString();

                        if (questManager.acceptQuestList[i].questTargets[k].targetName != talkedNPC)
                            continue;

                        questManager.acceptQuestList[i]._currentTargetValue++;
                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 1;
                        return;
                    }
                }
            }
        }
    }
    public void CopletedQuestM(Animal animalV, GameObject attackedMob)
    {
        if (questManager.acceptQuestList.Count == 0 &&  characterMovement.clickedMob2 != attackedMob.name) return;
        for (int i = questManager.acceptQuestList.Count - 1; i >= 0; i--)
        {
            for (int k = 0; k < questManager.acceptQuestList[i].questTargets.Count; k++)
            {
                if (questManager.acceptQuestList[i].questTargets[k].targetName == animalV.animalName &&
                    questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Kill)
                {
                    if (questManager.acceptQuestList[i].questTargets[k].currentAmount <
                       (questManager.acceptQuestList[i].questTargets[k].requiredAmount - 1) &&
                        questManager.acceptQuestList[i]._delivered)
                    {
                        questManager.acceptQuestList[i].questTargets[k].currentAmount++;
                    }
                    else if (questManager.acceptQuestList[i].questTargets[k].currentAmount <
                            (questManager.acceptQuestList[i].questTargets[k].requiredAmount) &&
                             !questManager.acceptQuestList[i]._delivered)
                    {
                        questManager.acceptQuestList[i].questTargets[k].currentAmount++;
                    }
                    else if (questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Kill &&
                    questManager.acceptQuestList[i]._delivered)
                    {
                        RewardGoldRubyEx(i);
                        questManager.acceptQuestValue--;

                        questManager.acceptQuestList[i].questCompleted = true;

                        RewardItems(i);

                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 0;
                        questManager.acceptQuestList.Remove(questManager.acceptQuestList[i]);
                    }
                    if (questManager.acceptQuestList[i].questTargets[k].currentAmount >=
                       (questManager.acceptQuestList[i].questTargets[k].requiredAmount) &&
                       !questManager.acceptQuestList[i]._delivered)
                    {
                        questManager.acceptQuestList[i].questCompleted = true;
                    }
                }
                else if(questManager.acceptQuestList[i].questTargets[k].targetName == animalV.animalName &&
                        questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Kill &&
                        !questManager.acceptQuestList[i]._delivered)
                {
                    questManager.acceptQuestList[i].questCompleted = true;
                }
                if ((questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + "Fang") ||
                     questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + "Scale") ||
                     questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + "Skin") ||
                     questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + " Spoil"))
                    && questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Collect)
                {
                    if (questManager.acceptQuestList[i].questTargets[k].currentAmount <
                       (questManager.acceptQuestList[i].questTargets[k].requiredAmount - 1) &&
                        questManager.acceptQuestList[i]._delivered)
                    {
                        if (Random.Range(0, 3) == 1)
                            questManager.acceptQuestList[i].questTargets[k].currentAmount++;
                    }
                    else if (questManager.acceptQuestList[i].questTargets[k].currentAmount <
                            (questManager.acceptQuestList[i].questTargets[k].requiredAmount) &&
                            !questManager.acceptQuestList[i]._delivered)
                    {
                        if (Random.Range(0, 3) == 1)
                            questManager.acceptQuestList[i].questTargets[k].currentAmount++;
                    }
                    else if (questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Collect &&
                    questManager.acceptQuestList[i]._delivered)
                    {
                        RewardGoldRubyEx(i);
                        questManager.acceptQuestValue--;

                        questManager.acceptQuestList[i].questCompleted = true;

                        RewardItems(i);

                        questManager.acceptQuestList[i].questTargets[k].currentAmount = 0;
                        questManager.acceptQuestList.Remove(questManager.acceptQuestList[i]);
                    }
                    if (questManager.acceptQuestList[i].questTargets[k].currentAmount >=
                       (questManager.acceptQuestList[i].questTargets[k].requiredAmount) &&
                       !questManager.acceptQuestList[i]._delivered)
                    {
                        questManager.acceptQuestList[i].questCompleted = true;
                    }
                }
                else if ((questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + "Fang") ||
                          questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + "Scale") ||
                          questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + "Skin") ||
                          questManager.acceptQuestList[i].questTargets[k].targetName == (animalV.animalName + " Spoil"))
                          && questManager.acceptQuestList[i].questTargets[k].goalType == QuestGoalType.Collect &&
                          !questManager.acceptQuestList[i]._delivered)
                {
                    questManager.acceptQuestList[i].questCompleted = true;
                }
            }
        }
    }
    public void CompletedQuestStructure(string structureName, int currentStructureLevel)
    {
        if (dataQuest.questsOfStructures.Count == 0) return;

        for (int i = 0; i < dataQuest.questsOfStructures.Count; i++)
        {
            for (int k = 0; k < dataQuest.questsOfStructures[i].questTargets.Count; k++)
            {
                QuestTarget target = dataQuest.questsOfStructures[i].questTargets[k];

                if (structureName.Contains(target.targetName) &&
                    target.requiredAmount == currentStructureLevel &&
                    target.goalType == QuestGoalType.UpgradeBuilding &&
                    !dataQuest.questsOfStructures[i].questCompleted)
                {
                    //RewardGoldRubyExStructure(i, dataQuest.questsOfStructures);
                    dataQuest.questsOfStructures[i].questCompleted = true;
                    //RewardItemsStructure(i, dataQuest.questsOfStructures);

                    return;
                }
            }
        }
    }
    public void CompletedQuestSoldier(TroopType troopType, int currentSoldierCount)
    {
        if (dataQuest.questsOfSoldiers.Count == 0) return;

        for (int i = 0; i < dataQuest.questsOfSoldiers.Count; i++)
        {
            for (int k = 0; k < dataQuest.questsOfSoldiers[i].questTargets.Count; k++)
            {
                QuestTarget target = dataQuest.questsOfSoldiers[i].questTargets[k];

                if (troopType.ToString() == target.targetName &&
                    target.requiredAmount <= currentSoldierCount &&
                    target.goalType == QuestGoalType.Train &&
                    !dataQuest.questsOfSoldiers[i].questCompleted)
                {
                    //RewardGoldRubyExStructure(i, dataQuest.questsOfSoldiers);
                    dataQuest.questsOfSoldiers[i].questCompleted = true;
                    //RewardItemsStructure(i, dataQuest.questsOfSoldiers);

                    return;
                }
            }
        }
    }
    public void CompletedQuestResource(GameResources.ResourceType resourceType, int currentProduceCount)
    {
        if (dataQuest.produceOfResource.Count == 0) return;

        for (int i = 0; i < dataQuest.produceOfResource.Count; i++)
        {
            for (int k = 0; k < dataQuest.produceOfResource[i].questTargets.Count; k++)
            {
                QuestTarget target = dataQuest.produceOfResource[i].questTargets[k];

                if (resourceType.ToString() == target.targetName &&
                    target.requiredAmount <= currentProduceCount &&
                    target.goalType == QuestGoalType.Produce &&
                    !dataQuest.produceOfResource[i].questCompleted)
                {
                    //RewardGoldRubyExStructure(i, dataQuest.produceOfResource);
                    dataQuest.produceOfResource[i].questCompleted = true;
                    //RewardItemsStructure(i, dataQuest.produceOfResource);

                    return;
                }
            }
        }
    }
    public void CopletedQuestLevel()
    {
        if (dataQuest.questOfLevels.Count == 0) return;
        for (int i = dataQuest.questOfLevels.Count - 1; i >= 0; i--)
        {
            for (int k = 0; k < dataQuest.questOfLevels[i].questTargets.Count; k++)
            {
                if (dataQuest.questOfLevels[i].questTargets[k].requiredAmount == characterMovement.characterLevel &&
                     dataQuest.questOfLevels[i].questTargets[k].goalType == QuestGoalType.ReachLevel)
                {
                    dataQuest.questOfLevels[i].questCompleted = true;

                    return;
                }
            }
        }
    }
}
