using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StructureQuestPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueUpPanelText;

    [SerializeField] private DataQuest dataQuest;

    [SerializeField] private GameObject exampleLine;
    [SerializeField] private GameObject exampleLineCompleted;
    public GameObject constantPanel;
    [SerializeField] private Transform constantContent;

    public List<GameObject> questGOList = new List<GameObject>();
    public List<GameObject> questGOListCompleted = new List<GameObject>();
    [SerializeField] private Sprite[] rankSprites;

    //-----------------------------------

    private void FillQuestList(List<GameObject> questList, int value, GameObject line, List<Quest> oneQuest)
    {
        GameObject dummyGO = Instantiate(line, constantContent);
        questList.Add(dummyGO);
        int valueX = questList.IndexOf(dummyGO);

        questList[valueX].SetActive(true);
        QuestOnePanel questPanel = questList[valueX].GetComponent<QuestOnePanel>();
        questPanel.oneQuest = oneQuest[value];
        questPanel.questID = value;
        questPanel.questName.text = oneQuest[value].questName.ToString();

        dialogueUpPanelText.text = questPanel.oneQuest.questType.ToString();
        for (int j = 0; j < oneQuest[value].questTargets.Count; j++)
        {
            questPanel.questInformation.text = oneQuest[value].questInfo;
        }
        if(questPanel.questImage != null)
           questPanel.questImage.sprite = oneQuest[value].questIcon;
        if (questPanel.questRankImage != null)
            questPanel.questRankImage.sprite = rankSprites[(int)oneQuest[value].questRank];
    }
    private void WriteQuestListNameCompleted(List<Quest> quesitListName)
    {
        for (int i = 0; i < quesitListName.Count; i++)
        {
            if (quesitListName[i].questCompleted && !quesitListName[i].questAccepted)
            {
                FillQuestList(questGOListCompleted, i, exampleLineCompleted, quesitListName);
            }
        }
    }
    private void WriteQuestListName(List<Quest> quesitListName)
    {
        for (int i = 0; i < quesitListName.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                int index = j + 1 + (i * 8);

                if (index >= quesitListName.Count)
                    break;
                if (!quesitListName[index].questCompleted && !quesitListName[index].questAccepted)
                {
                    FillQuestList(questGOList, index, exampleLine, quesitListName);
                    break;
                }
            }
        }
    }
    public void ShowQuestList()
    {
        WriteQuestListNameCompleted(dataQuest.questsOfStructures);
        WriteQuestListNameCompleted(dataQuest.questsOfSoldiers);
        WriteQuestListNameCompleted(dataQuest.produceOfResource);
        WriteQuestListNameCompleted(dataQuest.questOfLevels);
        WriteQuestListName(dataQuest.questsOfStructures);
        WriteQuestListName(dataQuest.questsOfSoldiers);
        WriteQuestListName(dataQuest.produceOfResource);
        WriteQuestListName(dataQuest.questOfLevels);
    }
    public void SubResetFunc(List<GameObject> _questGOList)
    {
        for (int i = _questGOList.Count - 1; i >= 0 ; i--)
        {
            if (_questGOList[i] != null && _questGOList[i].activeSelf)
            {
                Destroy(_questGOList[i]);
                _questGOList.Remove(_questGOList[i]);
            }
        }
    }
    public void SubResetFuncWithBreak(List<GameObject> _questGOList)
    {
        for (int i = _questGOList.Count - 1; i >= 0; i--)
        {
            if (_questGOList[i] != null && _questGOList[i].activeSelf)
            {
                Destroy(_questGOList[i]);
                _questGOList.Remove(_questGOList[i]);
                break;
            }
        }
    }
    public void ResetConstantQuestList()
    {
        SubResetFunc(questGOList);
        SubResetFunc(questGOListCompleted);
    }
    public void OpenWatchtowerPanel()
    {
        ResetConstantQuestList();
        constantPanel.SetActive(true);

        ShowQuestList();
    }
}
