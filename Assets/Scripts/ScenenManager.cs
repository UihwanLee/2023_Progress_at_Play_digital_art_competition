using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ��ü���� ���� ����ϴ� ��ũ��Ʈ
*/

public class ScenenManager : MonoBehaviour
{
    private bool isLoad;

    [Header("UI")]
    [SerializeField]
    private GameObject loadingUI;

    [SerializeField]
    private StageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        isLoad = false;

        resetAlpha(loadingUI);
    }

    private void resetAlpha(GameObject canvas)
    {
        canvas.SetActive(true);
        canvas.GetComponent<CanvasGroup>().alpha = 0.0f;
        canvas.SetActive(false);
    }


    public void LoadStage()
    {
        StartCoroutine(LoadingCoroutine());
    }

    // �ε� �� ����
    IEnumerator LoadingCoroutine()
    {
        isLoad = true;
        float time = Random.Range(2.0f, 3.0f);

        loadingUI.SetActive(true);

        // Fade
        CanvasGroup cg = loadingUI.GetComponent<CanvasGroup>();
        StartCoroutine(FadeCourtine(cg, cg.alpha, 1, 0.5f));
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeCourtine(cg, cg.alpha, 0, 0.5f));
        isLoad = false;

        yield return new WaitForSeconds(1.5f);
        loadingUI.SetActive(false);
    }

    IEnumerator FadeCourtine(CanvasGroup _cg, float _start, float _end, float _duration)
    {
        float counter = 0.0f;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            _cg.alpha = Mathf.Lerp(_start, _end, counter / _duration);

            yield return null;
        }

        stageManager.SpawnPlayerStage(stageManager.GetCurStageName());
    }

    public bool isLoading() { return isLoad; }
}
