using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Emotions
{
    Neutral,
    Angry,
    Sad,
    Happy
}
public enum Character
{
    Player,
    Test,
    Yette,
    Farfolle,
    Ghetti,
    RaVito
}

public enum ActionChoice
{
    Nothing,
    RunMiniGame,
    SearchMiniGame,
    RiddleMiniGame,
    GunMiniGame
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

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/DialogieScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public List<DialogueLine> dialogueList;
    public Character talker1;
    public Character talker2;



}


