using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/DialogieScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public List<String> dialogueList;
}
