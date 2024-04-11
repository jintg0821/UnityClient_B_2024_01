using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExItem
{
    public bool IsCollected;
}

public class ExCollectedSystem : MonoBehaviour
{
    public List<ExItem> collectedItem = new List<ExItem>();     //컬렉팅 할 리스트

    // Start is called before the first frame update
    void Start()
    {
        collectedItem.Add(new ExItem());
        collectedItem.Add(new ExItem());
        collectedItem[0].IsCollected = true;
        collectedItem[1].IsCollected = false;
        CheckAllItemsCollected();
    }

    void CheckAllItemsCollected()
    {
        if(collectedItem.All(item => item.IsCollected ))
        {
            Debug.Log("All items collected");
        }
        else
        {
            Debug.Log("Not all items collected!");
        }
    }
}
