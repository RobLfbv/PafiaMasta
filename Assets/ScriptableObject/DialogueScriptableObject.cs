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
    Lasagne,
    A,
}

public enum ActionChoice
{
    Nothing,
    RunMiniGame,
    SearchMiniGame,
    GunMiniGame,
    FactoryMiniGame,
    WinRiddleGame,
    DialogueYetteOmbre,
    DialogueYetteRaVito,
    CheckYetteDialogueInfoZily,
    CheckYetteDialogueInfoRaVito,
    CheckYetteDialogueInfoFarfolle,
    FinishGameRaVito,
    FinishGameYette
}

public enum AlibiUnlock
{
    YetteAlibi1, YetteAlibi2, YetteAlibi3, YetteAlibi4, YetteAlibi5, FarfolleAlibi1, FarfolleAlibi2, FarfolleAlibi3, GhettiAlibi1, GhettiAlibi2, ZilyAlibi1, Ra_VitoAlibi1
}

[System.Serializable]
public class DialogueLine
{
    public int idDialogue;
    public Character charTalking;
    public string text;
    public string textEn;
    public Emotions emotion;
    public List<Choice> choices;
    public int idGoto;
    public bool isChoiceDialogue;
}

[System.Serializable]
public class Choice
{
    public String textChoice;
    public String textChoiceEn;
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
[System.Serializable]
public class UnlockCarnet
{
    public AlibiUnlock alibiToUnlock;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/DialogieScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public List<DialogueLine> dialogueList;
    public Character talker1;
    public Character talker2;
    public ActionChoice dialogueAction;
    public UnlockDialogue[] unlockDialogue;
    public UnlockCarnet[] unlockCarnets;
}


