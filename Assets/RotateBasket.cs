using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class RotateBasket : MonoBehaviour
{
    #region 변수들
    private GameObject basket;
    private GameObject ttb;
    private GameObject Oezjo;
    private GameObject lpiece;
    private GameObject rpiece;
    private GameObject ltarget;
    private GameObject rtarget;
    private GameObject talk;
    private RectTransform basketRectTransform;
    private RectTransform cookieTransform;
    private Vector2 initCcale;
    private RotateBasket smac;
    private GameObject Ocore;
    private GameObject retry;
    private Button selfB;
    private Button coreB;
    private Button baseB;
    private int _ = 0;
    private int _1 = 0;
    private TextMeshProUGUI text0;
    
    TMP_InputField inputField;
    Vector2 lv;
    Vector2 rv;
    float newHeight;
    #endregion

    private void Awake(){
        basket = GameObject.FindWithTag("Player");
        inputField = FindObjectOfType<TMP_InputField>();
        smac = FindObjectOfType<RotateBasket>();
        Ocore = smac.gameObject;
        talk = inputField.gameObject;
        ttb = GameObject.FindWithTag("Respawn");
    }

    private void Start()
        {
            retry = GameObject.FindWithTag("Finish");
            Oezjo = talk.transform.parent.gameObject;
            Oezjo.SetActive(false);
            Transform ot = Oezjo.GetComponent<Transform>();

            lpiece = ot.GetChild(1).gameObject;
            rpiece = ot.GetChild(2).gameObject;
            ltarget = ot.GetChild(3).gameObject;
            rtarget = ot.GetChild(4).gameObject;

        text0 = ttb.GetComponent<TextMeshProUGUI>();

        baseB = basket.GetComponent<Button>();
        ttb.SetActive(false);
        initCcale = new Vector2(15.26292f, 15.4337f);
        cookieTransform = Oezjo.GetComponent<RectTransform>();
        cookieTransform.localScale = new Vector2(initCcale.x - 2, initCcale.y - 2);
        selfB = gameObject.GetComponent<Button>();
        selfB.interactable = false;
        coreB = smac.GetComponent<Button>();
        coreB.interactable = false;
        talk.SetActive(false);
        retry.SetActive(false);
        lv = ltarget.GetComponent<RectTransform>().position;
        rv = rtarget.GetComponent<RectTransform>().position;
    }

    public void mix()
    {
        coreB.interactable = true;
        if(basket.TryGetComponent(out basketRectTransform)){
        StartCoroutine(RotateBasketCoroutine());
        }
    }

    private System.Collections.IEnumerator RotateBasketCoroutine()
    {
        int repeatCount = 0;
        var wait0 = new WaitForSeconds(0.325f);
        var wait15 = new WaitForSeconds(0.15f);
        yield return wait0;
        while (repeatCount < 3)
        {
            yield return RotateToAngle(0, -15);
            yield return RotateToAngle(-15, 15);
            yield return RotateToAngle(15, 0);
            repeatCount++;
            yield return wait15;
        }
         if(_1 < 1){
            yield return wait0;
            ttb.SetActive(true);
        }
        selfB.interactable = true;
    }

    private System.Collections.IEnumerator RotateToAngle(float startAngle, float endAngle)
    {
        float timeElapsed = 0f;
        while (timeElapsed < 0.12f)
        {
            float t = timeElapsed / 0.12f;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            basketRectTransform.rotation = Quaternion.Euler(0, 0, angle);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        basketRectTransform.rotation = Quaternion.Euler(0, 0, endAngle);
    }

    public void touch(){
        StartCoroutine(touched());
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(basket, pointerEventData, ExecuteEvents.pointerClickHandler);
        ttb.SetActive(false);
        selfB.interactable = false;
    }

    private System.Collections.IEnumerator touched()
    {
        var wait85 = new WaitForSeconds(0.85f);
        basket.SetActive(false);
        Oezjo.SetActive(true);
        StartCoroutine(AnimateScaleBack());
        yield return wait85;
        StartCoroutine(MoveToAp(lv, 0.5f));
        StartCoroutine(MoveToBp(rv, 0.5f));
        talk.SetActive(true);
        StartCoroutine(AnimateWidth());
        yield return wait85;
        StartCoroutine(AnimateHeight());
        yield return wait85;
        yield return wait85;
        yield return wait85;
        yield return wait85;
        retry.SetActive(true);
    }

    private System.Collections.IEnumerator AnimateScaleBack()
    {
        _1++;
        Vector2 targetScale = initCcale;
        Vector2 startScale = cookieTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < 0.39f)
        {
            cookieTransform.localScale = Vector2.Lerp(startScale, targetScale, elapsedTime / 0.39f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set to the initial scale
        cookieTransform.localScale = targetScale;
    }

    

    private System.Collections.IEnumerator AnimateWidth()
    {
        float current = talk.GetComponent<RectTransform>().sizeDelta.x;
        while (Mathf.Abs(160f - 20f) > 0.01f)
        {
            current = Mathf.MoveTowards(current, 160f, 280f * Time.deltaTime);
            talk.GetComponent<RectTransform>().sizeDelta = new Vector2(current, talk.GetComponent<RectTransform>().sizeDelta.y);
            yield return null;
        }

        // Ensure the final height is set to the target height
        talk.GetComponent<RectTransform>().sizeDelta = new Vector2(160f, talk.GetComponent<RectTransform>().sizeDelta.y);
    }

    private System.Collections.IEnumerator AnimateHeight()
    {
        _++;
        if(_ == 1){
        float elapsedTime = 0f;
        while (elapsedTime < 1)
        {
            newHeight = Mathf.Lerp(30f, 120f, elapsedTime / 1);
            talk.GetComponent<RectTransform>().sizeDelta = new Vector2(talk.GetComponent<RectTransform>().sizeDelta.x, newHeight);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final height is set to the target height
        talk.GetComponent<RectTransform>().sizeDelta = new Vector2(talk.GetComponent<RectTransform>().sizeDelta.x, 120f);
        }
    }

    private System.Collections.IEnumerator MoveToAp(Vector2 targetPos, float duration)
    {
        Vector2 startPos = lpiece.transform.position;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            lpiece.transform.position = Vector3.Lerp(startPos, targetPos, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        lpiece.transform.position = targetPos;
    }

    private System.Collections.IEnumerator MoveToBp(Vector2 targetPos1, float duration1)
    {
        Vector2 startPos1 = rpiece.transform.position;
        float timeElapsed1 = 0f;
        while (timeElapsed1 < duration1)
        {
            float t1 = timeElapsed1 / duration1;
            rpiece.transform.position = Vector3.Lerp(startPos1, targetPos1, t1);
            timeElapsed1 += Time.deltaTime;
            yield return null;
        }
        rpiece.transform.position = targetPos1;
    }

    public void reet(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    
}
