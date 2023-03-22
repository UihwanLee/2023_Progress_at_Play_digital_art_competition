using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    전체적인 씬을 담당하는 스크립트
*/

public class ScenenManager : MonoBehaviour
{
    private bool isLoad;
    private bool isSkip;

    [Header("UI")]
    [SerializeField]
    private GameObject loadingUI;

    [SerializeField]
    private CameraController cameraController;

    // Scripts
    [SerializeField]
    private StageManager stageManager;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private InterectManager interectManager;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        isLoad = false;
        isSkip = false;

        resetAlpha(loadingUI);

        this.player = GameObject.Find("Player").GetComponent<PlayerController>();
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

    // 로딩 씬 구현
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

    // STAGE1 Field1 애니메이션
    public void Anim_STAGE1_Field1()
    {
        player.SetPlayerWalk(false);
        player.SetAnim(true);
        StartCoroutine(Anim_STAGE1_Field1_Coroutine());
    }

    IEnumerator Anim_STAGE1_Field1_Coroutine()
    {
        uiManager.SetPurposeUI(false);
        uiManager.SetSkipUI(true);

        // 카메라 이동
        Vector3 startPos = new Vector3(player.transform.position.x + 15.0f, player.transform.position.y + 1.0f, player.transform.position.z);
        Vector3 endPos = new Vector3(player.transform.position.x + 1.0f, player.transform.position.y + 1.0f, player.transform.position.z);

        if(!isSkip) cameraController.MoveCamera(startPos);

        if (!isSkip) yield return new WaitForSeconds(3f);

        stageManager.SetEffect("SNOW_EFFECT", true);

        float duration = 5f;
        float time = 0f;

        while (time < duration)
        {
            if (isSkip) break;
            cameraController.MoveCamera(Vector3.Lerp(startPos, endPos, (time / duration)));
            time += Time.deltaTime;
            yield return null;
        }

        // UI 설정 및 ChatGPT 설정

        uiManager.NextPlayerMessage();
        uiManager.NextChatAI();

        if (!isSkip) yield return new WaitForSeconds(5f);

        uiManager.SetPurposeUI(true);
        uiManager.NextPurpose();

        cameraController.MoveCamera(player.transform.position);

        isSkip = false;
        uiManager.SetSkipUI(false);

        player.SetAnim(false);

    }

    // STAGE1 Field1 애니메이션
    public void Anim_STAGE1_Field2()
    {
        player.SetAnim(true);
        StartCoroutine(Anim_STAGE1_Field2_Coroutine());
    }

    IEnumerator Anim_STAGE1_Field2_Coroutine()
    {
        uiManager.SetSkipUI(true);
        uiManager.SetPurposeUI(false);
        stageManager.SetEffect("SNOW_EFFECT", false);

        //if (field1 == null || field2 == null)
        //{
        //    //// Field2 페이드 인
        //    //// Fade
        //    //CanvasGroup cg = field2.GetComponent<CanvasGroup>();
        //    //StartCoroutine(FadeCourtine(cg, cg.alpha, 1, 0.5f));
        //    ////yield return new WaitForSeconds(time);
        //    //StartCoroutine(FadeCourtine(cg, cg.alpha, 0, 0.5f));
        //}
        stageManager.GetStageField("STAGE_FIELD2").SetActive(true);
        // 위로 이동
        stageManager.GetStage1BG().transform.position = 
            new Vector3(stageManager.GetStage1BG().transform.position.x, stageManager.GetStage1BG().transform.position.y + 11f, stageManager.GetStage1BG().transform.position.z);
        stageManager.SetStage1BG(1);

        if(!isSkip) yield return new WaitForSeconds(2f);

        player.DetachRope();

        if (!isSkip) yield return new WaitForSeconds(2f);

        stageManager.GetStageField("STAGE_FIELD1").SetActive(false);

        float duration = 4f;
        float time = 0f;

        // 카메라 이동
        Vector3 startPos = new Vector3(player.transform.position.x + 1.0f, player.transform.position.y + 1.0f, player.transform.position.z);
        Vector3 endPos = new Vector3(player.transform.position.x - 15.0f, player.transform.position.y + 1.0f, player.transform.position.z);

        while (time < duration)
        {
            if (isSkip) break;
            cameraController.MoveCamera(Vector3.Lerp(startPos, endPos, (time / duration)));
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;

        startPos = cameraController.transform.position;
        endPos = new Vector3(player.transform.position.x + 1.0f, player.transform.position.y + 1.0f, player.transform.position.z);

        while (time < duration)
        {
            if (isSkip) break;
            cameraController.MoveCamera(Vector3.Lerp(startPos, endPos, (time / duration)));
            time += Time.deltaTime;
            yield return null;
        }

        // UI 설정 및 ChatGPT 설정

        StartCoroutine(PlayerMessageWait(3f));
        //uiManager.NextPlayerMessage();
        uiManager.NextChatAI();

        if (!isSkip) yield return new WaitForSeconds(5f);

        uiManager.SetPurposeUI(true);
        uiManager.NextPurpose();

        cameraController.MoveCamera(player.transform.position);

        isSkip = false;
        uiManager.SetSkipUI(false);

        player.SetPlayerSpeed(5f);

        player.SetAnim(false);
    }

    public void Anim_STAGE1_Field3(Interect _obj)
    {
        player.SetAnim(true);
        StartCoroutine(Anim_STAGE1_Field3_Coroutine(_obj));
    }

    IEnumerator Anim_STAGE1_Field3_Coroutine(Interect _obj)
    {
        uiManager.SetSkipUI(true);
        uiManager.SetPurposeUI(false);

        // 새싹 키우기

        float duration = 4f;
        float time = 0f;

        // pos -20 7.5
        // scale 2 4

        // 물체 이동
        Vector3 startPos = new Vector3(_obj.transform.position.x, _obj.transform.position.y, _obj.transform.position.z);
        Vector3 endPos = new Vector3(_obj.transform.position.x, _obj.transform.position.y + 8f, player.transform.position.z);

        Vector3 startScale = _obj.transform.localScale;
        Vector3 endScale = new Vector3(_obj.transform.localScale.x + 1.5f, _obj.transform.localScale.y + 3.5f, _obj.transform.localScale.z);

        uiManager.NextPlayerMessage();
        while (time < duration)
        {
            _obj.transform.position = (Vector3.Lerp(startPos, endPos, (time / duration)));
            _obj.transform.localScale = (Vector3.Lerp(startScale, endScale, (time / duration)));
            time += Time.deltaTime;

            yield return null;
        }

        if (!isSkip) yield return new WaitForSeconds(4f);

        // 캐릭터 반응
        uiManager.NextPlayerMessage();

        time = 0;

        // 캐릭터 && 카메라 이동
        startPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        endPos = new Vector3(player.transform.position.x - 1f , player.transform.position.y + 15.0f, player.transform.position.z);

        player.GetComponent<Animator>().SetBool("walk", false);
        player.GetComponent<Animator>().SetBool("climb", true);

        Vector3 moveCameraPos;

        while (time < duration)
        {
            if (isSkip) break;
            player.transform.position = (Vector3.Lerp(startPos, endPos, (time / duration)));
            moveCameraPos = new Vector3(player.transform.position.x + 1.0f, player.transform.position.y + 1.0f, player.transform.position.z);
            cameraController.MoveCamera(moveCameraPos);
            time += Time.deltaTime;
            yield return null;
        }

        if (isSkip) player.transform.position = (new Vector3(player.transform.position.x, 32.0f, player.transform.position.z));

        stageManager.SetStage1BG(2);
        stageManager.GetStage1BG().transform.position =
            new Vector3(stageManager.GetStage1BG().transform.position.x, stageManager.GetStage1BG().transform.position.y + 10f, stageManager.GetStage1BG().transform.position.z);

        player.GetComponent<Animator>().SetBool("climb", false);

        stageManager.GetStageField("STAGE_FIELD3").SetActive(true);

        cameraController.MoveCamera(player.transform.position);
        player.SetAnim(false);

        // UI 설정 및 ChatGPT 설정
        StartCoroutine(PlayerMessageWait(3f));
        uiManager.NextChatAI();

        uiManager.SetPurposeUI(true);
        uiManager.NextPurpose();

        isSkip = false;
        uiManager.SetSkipUI(false);


        player.SetPlayerSpeed(5f);

        player.SetAnim(false);

        // Stage1 클리어
        stageManager.SetStageClear();
    }

    public void Anim_STAGE2_Field1()
    {
        player.SetAnim(true);
        StartCoroutine(Anim_STAGE2_Field1_Coroutine());
    }

    IEnumerator Anim_STAGE2_Field1_Coroutine()
    {
        uiManager.SetSkipUI(true);
        uiManager.SetPurposeUI(false);

        yield return new WaitForSeconds(1f);
        // BusManager 
        uiManager.ObjTalking(interectManager.GetCurInterctObj().index, 0);

        if (!isSkip) yield return new WaitForSeconds(4f);

        uiManager.NextPlayerMessage();

        if (!isSkip) yield return new WaitForSeconds(4f);

        uiManager.ObjTalking(interectManager.GetCurInterctObj().index, 1);

        if (!isSkip) yield return new WaitForSeconds(4f);

        uiManager.NextPlayerMessage();

        if (!isSkip) yield return new WaitForSeconds(4f);

        uiManager.ObjTalking(interectManager.GetCurInterctObj().index, 2);

        if (!isSkip) yield return new WaitForSeconds(7f);


        // chatAI
        StartCoroutine(PlayerMessageWait(3f));
        //uiManager.NextPlayerMessage();
        uiManager.NextChatAI();


        uiManager.SetPurposeUI(true);
        uiManager.NextPurpose();

        isSkip = false;
        uiManager.SetSkipUI(false);

        // Money System UI 활성화!
        uiManager.SetMoneySystemUI(true);

        player.SetAnim(false);
    }

    IEnumerator PlayerMessageWait(float _time)
    {
        yield return new WaitForSeconds(_time);
        uiManager.NextPlayerMessage();
    }


    public bool isLoading() { return isLoad; }
    public void SetSkip(bool _active) { isSkip = _active; }
}
