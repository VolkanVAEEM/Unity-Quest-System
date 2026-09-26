using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestManager : MonoBehaviour
{
    public DataQuest dataQuest;
    [SerializeField] private CityDevelopment watchtowerCityDevelopment;

    [SerializeField] private GameObject exampleLine;
    [SerializeField] private GameObject worldUIGO;
    [SerializeField] private GameObject watchtowerPanel;
    [SerializeField] private Transform watchtowerContent;

    public List<GameObject> questGOList = new List<GameObject>();
    [SerializeField] private Sprite[] rankSprites;
    public List<int> randomNumbers;

    public List<Quest> acceptQuestList;
    public int acceptQuestValue;

    private WaitForSeconds oneSecondWait = new WaitForSeconds(1f);
    private int counterCoroutine1 = 0;
    //-----------------------------------

    private void FillQuestList(int i, List<GameObject> questList, int value)
    {
        if (dataQuest.questsMH[value].questName == null) return;
        if (value == 0) return;
        if (acceptQuestList.Contains(dataQuest.questsMH[value])) return;

        for (int l = 0; l < watchtowerCityDevelopment.structure.structureLevel; l++)
        {
            if(questList[l] == null)
            {
                questList[l] = Instantiate(exampleLine, watchtowerContent);
                break;
            }
        }

        questList[i].SetActive(true);
        QuestOnePanel questPanel = questList[i].GetComponent<QuestOnePanel>();
        questPanel.oneQuest = dataQuest.questsMH[value];
        questPanel.questID = value;
        questPanel.questName.text = dataQuest.questsMH[value].questName.ToString();

        for (int j = 0; j < dataQuest.questsMH[value].questTargets.Count; j++)
        {
            switch (dataQuest.questsMH[value].questTargets[j].goalType)
            {
                case QuestGoalType.Kill:
                    questPanel.questInformation.text = "Defeat " +
                    dataQuest.questsMH[value].questTargets[j].requiredAmount + " " +
                    dataQuest.questsMH[value].questTargets[j].displayName;
                    break;
                case QuestGoalType.Collect:
                    questPanel.questInformation.text = "Collect " +
                    dataQuest.questsMH[value].questTargets[j].requiredAmount + " " +
                    dataQuest.questsMH[value].questTargets[j].displayName;
                    break;
                case QuestGoalType.Capture:
                    questPanel.questInformation.text = "Capture " +
                    dataQuest.questsMH[value].questTargets[j].requiredAmount + " " +
                    dataQuest.questsMH[value].questTargets[j].displayName;
                    break;
                case QuestGoalType.ReachLevel:
                case QuestGoalType.Travel:
                case QuestGoalType.Talk:
                    questPanel.questInformation.text =
                    dataQuest.questsMH[value].questTargets[j].displayName;
                    break;
            }
        }

        questPanel.questImage.sprite = dataQuest.questsMH[value].questIcon;
        questPanel.questRankImage.sprite = rankSprites[(int)dataQuest.questsMH[value].questRank];
    }
    public void ShowQuestList()
    {
        for (int i = 0; i < watchtowerCityDevelopment.structure.structureLevel; i++)
        {
            if (randomNumbers[i] != 0)
                continue;

            bool questFound = false;

            int randomSpecialNumber = Random.Range(1, dataQuest.questsMH.Count);
            int safetyCounter = 0;

            while (!questFound)
            {
                safetyCounter++; 
                
                if (safetyCounter >= dataQuest.questsMH.Count)
                {
                    break;
                }

                if (randomNumbers.Contains(randomSpecialNumber) ||
                    acceptQuestList.Contains(dataQuest.questsMH[randomSpecialNumber]))
                {
                    randomSpecialNumber++;

                    if (randomSpecialNumber >= dataQuest.questsMH.Count)
                    {
                        randomSpecialNumber = 1;
                    }
                }
                else
                {
                    randomNumbers[i] = randomSpecialNumber;
                    questFound = true;
                }
            }

            if (questFound)
            {
                FillQuestList(i, questGOList, randomNumbers[i]);
            }
        }
    }
    private IEnumerator ResetQuestList()
    {
        while (true)
        {
            yield return oneSecondWait;
            counterCoroutine1++;

            if (counterCoroutine1 >= 100)
            {
                counterCoroutine1 = 0;

                for (int i = 0; i < questGOList.Count; i++)
                {
                    Destroy(questGOList[i]);
                    questGOList[i] = null;
                    randomNumbers[i] = 0;
                }

                ShowQuestList();
            }
        }
    }
    public void OpenWatchtowerPanel()
    {
        worldUIGO.SetActive(false);
        watchtowerPanel.SetActive(true);

        ShowQuestList();
    }
    private void Start()
    {
        StartCoroutine(ResetQuestList());
    }
}