using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Database/StatDatabase")]
public class StatDatabase : BaseDB
{
    [SerializeField] private List<StatSO> database;

    private StatSO _lastAddedData = null;

    public List<StatSO> GetDatabase => database;

    public void AddData(StatSO data)
    {
        database.Add(data);
        _lastAddedData = data;
    }

    public void RemoveData(StatSO data)
    {
        database.Remove(data);
    }

    public void RemoveData(int index)
    {
        database.RemoveAt(index);
    }

    public void RemoveLastData()
    {
        if (_lastAddedData != null)
        {
            RemoveData(_lastAddedData);
            _lastAddedData = null;
        }
    }

    public void SortByName()
    {
        database.Sort((a, b) 
            => a.StatType.ToString()
            .CompareTo(b.StatType.ToString()));
    }
}
