using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    // Time in seconds to show log message for.
    [SerializeField]
    private float logSeconds;

    // An instance of the Log class.
    private static Log log;

    // The Log GameObject.
    private static GameObject logObject;

    // The text component of the Log Object.
    private static Text textField;

    private void Start()
    {
        log = this;
        logObject = GameObject.Find("Log");
        textField = logObject.GetComponentInChildren<Text>();
        logObject.SetActive(false);
    }

    public static void Print(object obj)
    {
        textField.text = obj.ToString();
        logObject.SetActive(true);
        log.StartCoroutine(nameof(WaitAndDisableLog), log.logSeconds);
    }

    public static void Printf(string message, params object[] parameters)
    {
        textField.text = string.Format(message, parameters);
        logObject.SetActive(true);
        log.StartCoroutine(nameof(WaitAndDisableLog), log.logSeconds);
    }

    // Waits for seconds, then disables the log window.
    private IEnumerator WaitAndDisableLog(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        logObject.SetActive(false);
    }
}
