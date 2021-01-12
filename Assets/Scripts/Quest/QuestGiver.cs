using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    // Debugging Only
    [SerializeField]
    private Questlog tmpLog;

    private void Awake()
    {
        //accept a quest;
        tmpLog.accpetQuests(quests[0]);
      
    }
}
