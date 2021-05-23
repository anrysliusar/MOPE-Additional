using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public static CanvasController instance { get; private set; }
    private const float ERROR_MESSAGE_TIME = 2f;
    private const string INPUT_ERROR_MESSAGE = "Неправильне введення";

    public GameObject info;
    public GameObject fermatMethodGO;

    public GameObject error;
    public Image errorImage;
    public Text errorText;
    private Coroutine errorCoroutine;

    public InputField inputN;
    public Text result;

    private void ShowErrorMessage(string message) {
        if (errorCoroutine != null) {
            StopCoroutine(errorCoroutine);
        }

        errorCoroutine = StartCoroutine(_ShowErrorMessage(message));
    }

    private IEnumerator _ShowErrorMessage(string message) {
        error.SetActive(true);
        errorText.text = message;
        var time = 0f;
        while (time < ERROR_MESSAGE_TIME) {
            errorImage.color = Color.Lerp(new Color(180f, 180f, 180f), Color.clear, time / ERROR_MESSAGE_TIME);
            time += Time.deltaTime;
            yield return null;
        }

        error.SetActive(false);
        errorCoroutine = null;
    }

    public void RaiseAndShowError(string message) {
        ShowErrorMessage(message);
    }

    private void Awake() {
        instance = this;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    private void Start() {
        SetDefault();
    }

    public void SetDefault() {
        info.SetActive(true);
        fermatMethodGO.SetActive(false);
    }

    private void OnClick(GameObject other) {
        info.SetActive(false);
        other.SetActive(true);
    }

    public void OnFermatMethod() {
        OnClick(fermatMethodGO);
    }

    private bool CheckInputs(out long n) {
        if (!long.TryParse(inputN.text, NumberStyles.Float, CultureInfo.InvariantCulture, out n)) {
            n = 0;
            return false;
        }

        return true;
    }


    public void OnFindMultipliers() {
        if (!CheckInputs(out var n)) {
            RaiseAndShowError(INPUT_ERROR_MESSAGE);
            return;
        }

        long[] multipliers;
        try {
            multipliers = FindMultipliers(n);
        }
        catch (Exception e) {
            RaiseAndShowError(e.Message);
            return;
        }

        ShowResult(multipliers, n);
    }

    private void ShowResult(long[] multipliers, long n) {
        var resultText = $"n = {n} = {string.Join(" * ", multipliers)}";
        result.text = resultText;
    }

    private long[] FindMultipliers(long n) {
        return FermatMethod.Factorize(n);
    }
}