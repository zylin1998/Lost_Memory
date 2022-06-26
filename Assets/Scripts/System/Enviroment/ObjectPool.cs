using System.Collections.Generic;
using UnityEngine;

#region Staff Class

[System.Serializable]
public class Staff 
{
    [SerializeField] private string staffName;

    public Component staffObject;

    public string StaffName => staffName;

    public Staff() 
    {
        staffObject = null;
        staffName = string.Empty;
    }

    public Staff(Component staffObject, string staffName)
    {
        this.staffObject = staffObject;
        this.staffName = staffName;
    }
}

#endregion

#region Staff Group Class

[System.Serializable]
public class StaffGroup
{
    [SerializeField] private string groupName;

    [SerializeField] private List<Staff> staffList;

    #region Reachable Properties

    public string GroupName => groupName;

    public int Count => staffList.Count;

    public Component this[string staffName]
    {
        get
        {
            Staff staff = null;

            foreach(Staff s in staffList) 
            {
                if (s.StaffName != staffName) { continue; }

                staff = s;

                break;
            }

            return staff == null ? null : staff.staffObject;
        }
    }

    #endregion

    public StaffGroup() 
    {
        groupName = string.Empty;

        staffList = new List<Staff>();
    }

    public StaffGroup(string name)
    {
        groupName = name;

        staffList = new List<Staff>();
    }

    public bool Add(Staff staff) 
    {
        if (staffList.Count <= 0) { staffList.Add(staff); return true; }

        if (this[staff.StaffName] != null) { return false; }

        staffList.Add(staff); 
        
        return true;
    }
}

#endregion

public static class ObjectPool
{
    private static List<StaffGroup> pool = new List<StaffGroup>();

    #region Reachable Properties

    public static int Count => pool.Count;

    public static List<StaffGroup> Pool => pool;

    #endregion

    #region Public Function

    public static bool Add(Component component, string staffName, string groupName)
    {
        return Add(new Staff(component, staffName), groupName);
    }

    public static bool Add(Staff staff, string groupName)
    {
        StaffGroup group = CheckGroup(groupName);

        return group == null ? false : group.Add(staff);
    }

    public static Component GetStaff(string staffName, string groupName) 
    {
        StaffGroup group = GetGroup(groupName);

        return group == null ? null : group[staffName];
    }

    public static void Reset() { pool = new List<StaffGroup>();  }

    #endregion

    #region Private Function

    private static StaffGroup CheckGroup(string groupName) 
    {
        if (GetGroup(groupName) == null) { pool.Add(new StaffGroup(groupName)); }

        return GetGroup(groupName);
    }

    private static StaffGroup GetGroup(string groupName) 
    {
        if (pool.Count <= 0) { return null; }

        foreach (StaffGroup group in pool)
        {
            if (group.GroupName == groupName) { return group; }
        }

        return null;
    }

    #endregion
}
