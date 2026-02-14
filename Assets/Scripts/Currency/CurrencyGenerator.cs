using UnityEngine;

public class CurrencyGenerator : MonoBehaviour
{
    public Currency currencyType;   // Which currency to generate
    public int amountPerTick = 1;       // How much to add each tick
    public float tickInterval = 1f;     // Seconds between ticks

    private void Start()
    {
        StartCoroutine(Generate());
    }

    private System.Collections.IEnumerator Generate()
    {
        // Wait for CurrencyManager to be ready (Addressables safe)
        while (CurrencyManager.Instance == null || !CurrencyManager.Instance.IsLoaded)
            yield return null;

        // Loop forever
        while (true)
        {
            CurrencyManager.Instance.Add(currencyType, amountPerTick);
            yield return new WaitForSeconds(tickInterval);
        }
    }
}