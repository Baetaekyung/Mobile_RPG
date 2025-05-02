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
    /// 시작 시 자동으로 호출하고 나중에 설정에서 useAutoSave기능 껐다 켰다 가능하게 만들기
    /// </summary>
    private void StartAutoSave()
    {
        pool.StartRegisterRoutine(AUTO_SAVE_ROUTINE_KEY);
    }

    public void RegisterAutoSave(ISavable savable)
    {
        if (saveDatas.Contains(savable) == true)
        {
            Debug.LogWarning($"이미 등록된 저장 데이터, {savable.SaveKey}");
            return;
        }

        saveDatas.Add(savable);
    }

    public void UnRegisterAutoSave(ISavable savable)
    {
        if(saveDatas.Contains(savable) == false)
        {
            Debug.LogWarning($"자동 저장에 등록되지 않은 저장 데이터를 제거하려함, {savable.SaveKey}");
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
