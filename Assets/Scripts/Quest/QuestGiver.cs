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
    private Sprite miniquestion, minitalkUI, miniExclamation;

    [SerializeField]
    private SpriteRenderer statusRenderer;


    public Quest[] MyQuests { get => quests; }

    [SerializeField]
    private SpriteRenderer minimapRender;

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
                    //Completed quest that can be handed in
                    statusRenderer.sprite = question;
                    minimapRender.sprite = miniquestion;
                    break;
                }
                else if (!Questlog.MyInstance.HasQuest(quest))
                {
                    //Quest-giver has a quest for the player
                    statusRenderer.sprite = Exclamation;
                    minimapRender.sprite = miniExclamation;
                    break;
                }
                else if (!quest.IsComplete && Questlog.MyInstance.HasQuest(quest))
                {
                    //All quests are in progress
                    statusRenderer.sprite = talkUI;
                    minimapRender.sprite = minitalkUI;
                }
            }
            else //No more quests
            {
                count++;
                if (count == quests.Length)
                {
                    statusRenderer.enabled = false;
                    minimapRender.enabled = false;
                }
            }
        }
    }
}
