using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QueueItem : MonoBehaviour
{
    public bool occupied;
    public int level;
    public TextMeshProUGUI text;

    public void Set(int Level)
    {
        this.occupied = true;
        this.level = Level;
        this.text.text = "LVL " + level;
    }

    public void Unset()
    {
        this.occupied = false;
        this.text.text = "";
        this.level = 0;
    }

    public void CopyFrom(QueueItem elementAt)
    {
        this.occupied = elementAt.occupied;
        this.level = elementAt.level;
        this.text.text = elementAt.text.text;
    }
}
