using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class RotateBasket : MonoBehaviour
{
    public GameObject basket;
    private RectTransform basketRectTransform;
    private Vector3 initCcale;
    private RectTransform cookieTransform;
    public GameObject ttb;
    public GameObject Oezjo;
    public GameObject lpiece;
    public GameObject rpiece;
    public GameObject ltarget;
    public GameObject rtarget;
    public GameObject talk;
    Vector2 lv;
    Vector2 rv;

    private void Start(){
        ttb.SetActive(false);
        cookieTransform = Oezjo.GetComponent<RectTransform>();
        initCcale = cookieTransform.localScale;
        Oezjo.SetActive(false);
        cookieTransform.localScale = new Vector3(initCcale.x - 2, initCcale.y - 2, initCcale.z);
        gameObject.GetComponent<Button>().interactable = false;
        talk.SetActive(false);
        
    }

    public void mix()
    {
        basketRectTransform = basket.GetComponent<RectTransform>();
        StartCoroutine(RotateBasketCoroutine());
    }

    private System.Collections.IEnumerator AnimateScaleBack()
    {
        Vector3 targetScale = initCcale;
        Vector3 startScale = cookieTransform.localScale;
        float duration = 0.39f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cookieTransform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set to the initial scale
        cookieTransform.localScale = targetScale;
    }

    private System.Collections.IEnumerator RotateBasketCoroutine()
    {
        int repeatCount = 0;
        while (repeatCount < 3)
        {
            yield return RotateToAngle(0, -15, 0.15f);
            yield return RotateToAngle(-15, 15, 0.15f);
            yield return RotateToAngle(15, 0, 0.15f);
            repeatCount++;
        }
        ttb.SetActive(true);
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void touch(){
        StartCoroutine(touched());
        ttb.SetActive(false);
    }

    private System.Collections.IEnumerator touched()
    {
        basket.SetActive(false);
        Oezjo.SetActive(true);
        StartCoroutine(AnimateScaleBack());
        lv = ltarget.GetComponent<RectTransform>().position;
        rv = rtarget.GetComponent<RectTransform>().position;
        yield return new WaitForSeconds(0.85f);
        StartCoroutine(MoveToAp(lv, 0.5f));
        StartCoroutine(MoveToBp(rv, 0.5f));
        talk.SetActive(true);
        StartCoroutine(AnimateWidth());
        yield return new WaitForSeconds(0.85f);
        StartCoroutine(AnimateHeight());
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
        float elapsedTime = 0f;

        while (elapsedTime < 1)
        {
            float newHeight = Mathf.Lerp(30f, 120f, elapsedTime / 1);
            talk.GetComponent<RectTransform>().sizeDelta = new Vector2(talk.GetComponent<RectTransform>().sizeDelta.x, newHeight);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final height is set to the target height
        talk.GetComponent<RectTransform>().sizeDelta = new Vector2(talk.GetComponent<RectTransform>().sizeDelta.x, 120f);
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

    private System.Collections.IEnumerator RotateToAngle(float startAngle, float endAngle, float duration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            basketRectTransform.rotation = Quaternion.Euler(0, 0, angle);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        basketRectTransform.rotation = Quaternion.Euler(0, 0, endAngle);
    }
}
