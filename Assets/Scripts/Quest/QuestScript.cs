using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    public bool markedCompelte = false;

    public void Select()
    {
        GetComponent<Text>().color = Color.blue;
        Questlog.MyInstance.ShowDescription(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<Text>().color = Color.white;

    }

    public void IsComplete()
    {
        if (MyQuest.IsComplete && !markedCompelte)
        {
            markedCompelte = true;
            GetComponent<Text>().text += " (DONE!)";
            MessageFeedManager.MyInstance.WriteMessage(string.Format("{0}: Complete!", MyQuest.MyTitle));
        }
       else if(!MyQuest.IsComplete)
        {
            markedCompelte = false;
            GetComponent<Text>().text = MyQuest.MyTitle;
        }

    }
}
