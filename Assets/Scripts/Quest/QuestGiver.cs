using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    [SerializeField]
    private Quest[] quests;

    [SerializeField]
    private Sprite question, talkUI, Exclamation;

    [SerializeField]
    private SpriteRenderer statusRenderer;


    public Quest[] MyQuests { get => quests; }

    private void Start()
    {
        foreach (Quest quest in quests)
        {
            quest.MyQuestGiver = this;
        }
    }

    public void UpdateQuestStatus()
    {
        int count = 0;

        foreach (Quest quest in quests)
        {
            if (quest != null)
            {
                if (quest.IsComplete && Questlog.MyInstance.HasQuest(quest))
                {
                    statusRenderer.sprite = question;
                    break;
                }
                else if (!Questlog.MyInstance.HasQuest(quest))
                {
                    //if the quest giver has a quest the player doesnt have
                    statusRenderer.sprite = Exclamation;
                    break;
                }
                else if (!quest.IsComplete && Questlog.MyInstance.HasQuest(quest))
                {
                    //the player is on the quest but hasn't completed yet
                    statusRenderer.sprite = talkUI;
                }
            }
            else
            {
                count++;

                if (count == quests.Length)
                {
                    statusRenderer.enabled = false;
                }
            }
        }
    }
}
