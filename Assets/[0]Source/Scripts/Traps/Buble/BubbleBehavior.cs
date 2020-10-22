using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior 
{
    public void peopleSetParentBubble(Transform people , Transform bubble){ people.SetParent(bubble);}
    
    public void peopleSetPositionBubble(Transform people) { people.transform.localPosition = Vector3.zero;}
    
}
