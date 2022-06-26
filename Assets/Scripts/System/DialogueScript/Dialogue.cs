using UnityEngine;

#region Sentence Data Class

[System.Serializable]
public class Sentence
{
    #region Parameter Field

    public int id;
    public int type;
    public int chara;
    public string dialogue;

    public bool backGroundImage;
    public bool sprite;
    public int ImageID;

    #endregion

    #region Construction

    public Sentence() 
    {
        type = 0;
        type = 0;
        chara = 0;
        dialogue = "";

        backGroundImage = false;
        sprite = false;
        ImageID = 0;
    }

    public Sentence(int type, int id, int chara, string dialogue) 
    {
        this.type = type;
        this.id = id;
        this.chara = chara;
        this.dialogue = dialogue;

        backGroundImage = false;
        sprite = false;
        ImageID = 0;
    }

    public Sentence(int type, int id, int chara, string dialogue, bool backGroundImage, bool sprite, int ImageID)
    {
        this.type = type;
        this.id = id;
        this.chara = chara;
        this.dialogue = dialogue;

        this.backGroundImage = backGroundImage;
        this.sprite = sprite;
        this.ImageID = ImageID;
    }

    #endregion
}

#endregion

#region Character Data Class

[System.Serializable]
public class Chara
{
    #region Parameter Field

    public string name;
    public int posiX;

    #endregion

    #region Construction

    public Chara() 
    {
        name = "";
        posiX = 0;
    }

    public Chara(string name, int posiX)
    {
        this.name = name;
        this.posiX = posiX;
    }

    #endregion
}

#endregion

[System.Serializable]
public class DialogueData
{
    #region Parameter Field

    public Chara[] charas;

    public Sentence[] sentences;

    #endregion

    #region Construction

    public DialogueData() { }

    public DialogueData(DialogueData dialogue) 
    {
        charas = dialogue.charas;
        sentences = dialogue.sentences;
    }

    public DialogueData(Chara[] charas, Sentence[] sentences) 
    { 
        this.charas = charas;
        this.sentences = sentences;
    }

    #endregion
}
