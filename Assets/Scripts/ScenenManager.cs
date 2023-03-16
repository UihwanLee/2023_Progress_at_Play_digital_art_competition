using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    전체적인 씬을 담당하는 스크립트
*/

public class ScenenManager : MonoBehaviour
{
    private bool isLoad;

    [SerializeField]
    private StageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        isLoad = false;
    }

    public void LoadStage()
    {
        StartCoroutine(LoadingCoroutine());
    }

    // 로딩 씬 구현
    IEnumerator LoadingCoroutine()
    {
        isLoad = true;
        yield return new WaitForSeconds(0.5f);
        stageManager.SpawnPlayerStage(stageManager.GetCurStageName());
        yield return new WaitForSeconds(0.5f);

        isLoad = false;
    }

    public bool isLoading() { return isLoad; }
}
