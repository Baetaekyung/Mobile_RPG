using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoSaveManager : MonoSingleton<AutoSaveManager>
{
    private const string AUTO_SAVE_ROUTINE_KEY = "AutoSave";

    private List<ISavable> saveDatas = new();

    [SerializeField] private float autoSaveInterval = 30f;
    [SerializeField] private bool useAutoSave = true;

    private CoroutinePool pool;

    private void Start()
    {
        pool = CoroutinePool.Inst;

        pool.RegisterRoutine(AUTO_SAVE_ROUTINE_KEY, AutoSaveRoutine);

        StartAutoSave();
    }

    /// <summary>
    /// ���� �� �ڵ����� ȣ���ϰ� ���߿� �������� useAutoSave��� ���� �״� �����ϰ� �����
    /// </summary>
    private void StartAutoSave()
    {
        pool.StartRegisterRoutine(AUTO_SAVE_ROUTINE_KEY);
    }

    public void RegisterAutoSave(ISavable savable)
    {
        if (saveDatas.Contains(savable) == true)
        {
            Debug.LogWarning($"�̹� ��ϵ� ���� ������, {savable.SaveKey}");
            return;
        }

        saveDatas.Add(savable);
    }

    public void UnRegisterAutoSave(ISavable savable)
    {
        if(saveDatas.Contains(savable) == false)
        {
            Debug.LogWarning($"�ڵ� ���忡 ��ϵ��� ���� ���� �����͸� �����Ϸ���, {savable.SaveKey}");
            return;
        }

        saveDatas.Remove(savable);
    }

    private IEnumerator AutoSaveRoutine()
    {
        while(useAutoSave)
        {
            foreach (ISavable savable in saveDatas)
            {
                savable.SaveData();
            }

            yield return YieldCache.GetWaitSec(autoSaveInterval);
        }
    }
}
