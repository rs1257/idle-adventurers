using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    public Currency currencyType;        // ScriptableObject reference
    public TextMeshProUGUI text;             // UI text element

    private bool ready = false;

    private void Start()
    {
        StartCoroutine(WaitForManager());
    }

    private System.Collections.IEnumerator WaitForManager()
    {
        // Wait until CurrencyManager.Instance exists
        while (CurrencyManager.Instance == null)
            yield return null;

        // Wait until currencies are loaded
        while (!CurrencyManager.Instance.IsLoaded)
            yield return null;

        ready = true;
    }

    private void Update()
    {
        if (!ready) return;

        int amount = CurrencyManager.Instance.GetAmount(currencyType);
        text.text = amount.ToString();
    }
}
