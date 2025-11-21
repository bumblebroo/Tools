using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent onTimerFinished;

    [SerializeField]
    private float duration;

    [SerializeField]
    private TextMeshPro text;
    private bool displayText;

    private void Start() {
        displayText = text != null;
    }

    public void StartTimer() {
        StopAllCoroutines();
        StartCoroutine()
    }

    private IEnumerator Timer() {
        if (displayText) {
            text.gameObject.SetActive(true);
        }

        float time = duration;
        while (time > 0) {
            time -= Time.deltaTime;

            if (displayText) {
                text.text = Mathf.Ceil(time);
            }

            yield return new WaitForEndOfFrame();
        }
        onTimerFinished?.Invoke();
    }
}
