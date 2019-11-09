using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Key")]
public class KeyScript : ScriptableObject
{
    public int level;
    public Sprite emptyKey;
    public Sprite fullKey;
    public bool unlocked;
}
