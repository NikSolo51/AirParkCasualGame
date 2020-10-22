using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBarrier : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AddPeoplesToLinkList>())
        {
            Destroy(this);
        }
    }
}
