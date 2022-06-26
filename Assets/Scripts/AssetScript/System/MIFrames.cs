using UnityEngine;

#region Memory Image Data Class

[System.Serializable]
public class MemoryImage
{
    #region Parameter Field

    public int page;
    public int id;
    public bool used;
    public Sprite sprite;

    #endregion
}

#endregion

#region Data Asset Class

[CreateAssetMenu(fileName = "Memory Images", menuName = "System Data/Memory Images", order = 1)]
public class MIFrames : ScriptableObject
{
    #region Parameter Field

    public MemoryImage[] frames;

    #endregion

    #region Reachable Properties

    public MemoryImage this[int id] => frames[id];

    #endregion

    #region Public Function

    public void SetCGFramesData(MIFramesData data) 
    {
        Clear();

        for (int i = 0; i < frames.Length; i++) 
        { 
            frames[i].used = data.used[i]; 
        }
    }

    public void Clear() 
    {
        foreach (MemoryImage frame in frames)
        {
            frame.used = false;
        }
    }

    public MIFramesData GetCGFramesData() { return new MIFramesData(this); }

    #endregion
}

#endregion

#region Data Store Class

[System.Serializable]
public class MIFramesData
{
    #region Parameter Field

    public bool[] used;

    #endregion

    #region Construction

    public MIFramesData() 
    {
        used = new bool[13];
    }

    public MIFramesData(MIFrames data) 
    {
        used = new bool[data.frames.Length];

        SetUsedImages(data);
    }

    #endregion

    #region Public Function

    public void SetUsedImage(int num) 
    { 
        used[num] = true; 
    }
    
    public void SetUsedImages(MIFrames data) 
    { 
        for(int i = 0; i < data.frames.Length; i++) { used[i] = data.frames[i].used; }
    }

    #endregion
}

#endregion