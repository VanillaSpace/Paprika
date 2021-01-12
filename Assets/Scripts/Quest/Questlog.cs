using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questlog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private Quest selected;

    [SerializeField]
    private Text questDescription;

    private List<QuestScript> questScripts = new List<QuestScript>();

    private static Questlog instance;
    public static Questlog MyInstance 
    
    {
        get 
        {
           if  (instance == null)
            {
                instance = FindObjectOfType<Questlog>();
            }
                return instance;
        } 
    }

    [SerializeField]
    private CanvasGroup canvasGroup;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OpenClose();
        }
    }
    public void accpetQuests(Quest quest)
    {
        foreach (CollectObjective o in quest.MyCollectObjectives)
        {
            inventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
        }

        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        questScripts.Add(qs);

        go.GetComponent<Text>().text = quest.MyTitle;
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSelect();
            }

            string objectives = string.Empty;

            selected = quest;

            string title = quest.MyTitle;

            foreach (Objective obj in quest.MyCollectObjectives)
            {
                objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            questDescription.text = string.Format("<color=#910000><b>{0}</b></color>\n<size=18>{1}</size>\n\n<color=#910000>Objectives</color>\n<size=18>{2}</size>", title, quest.MyDescription, objectives);
        }
        
    }

    public void CheckCompletion()
    {
        foreach (QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha == 1)
        {
            Close();
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
