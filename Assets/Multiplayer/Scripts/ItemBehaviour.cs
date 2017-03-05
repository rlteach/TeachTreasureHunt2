using UnityEngine;
using System.Collections;

public abstract class ItemBehaviour : MonoBehaviour {

    public enum ItemType {
        None = 0
       , Jump
    }

    public abstract ItemType Type { get; }

}
