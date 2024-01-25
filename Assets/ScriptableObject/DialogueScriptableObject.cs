using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Emotions
{
    Neutral,
    Angry,
    Sad,
    Happy,
    Questioned,
    Shocked,
    Ashamed,
    Absent
}
public enum Character
{
    Gnocchitalian_la_Détective,
    Test,
    Yette,
    Farfolle,
    Ghetti,
    Ra_Vito,
    Zily,
    Maili_Mailo,

}

public enum ActionChoice
{
    Nothing,
    RunMiniGame,
    SearchMiniGame,
    RiddleMiniGame,
    GunMiniGame,
    FactoryMiniGame
}

[System.Serializable]
public class DialogueLine
{
    public int idDialogue;
    public Character charTalking;
    public string text;
    public Emotions emotion;
    public List<Choice> choices;
    public int idGoto;
    public bool isChoiceDialogue;
}

[System.Serializable]
public class Choice
{
    public String textChoice;
    public int idNext;
    public ActionChoice method;
}
/*
0 = LaunchGameDialogue
1 = NotUnlock
2 = WinDialogue
3 = LoseDialogue
4 = FinishGameDialogue
5 = InfoDialogue
*/
[System.Serializable]
public class UnlockDialogue
{
    public Character characterToUnlock;
    public int dialogueIdUnlock;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/DialogieScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public List<DialogueLine> dialogueList;
    public Character talker1;
    public Character talker2;
    public ActionChoice dialogueAction;

    public UnlockDialogue[] unlockDialogue;
}


