using UnityEngine;
using UnityEngine.UI;

public class CurrencyButton : MonoBehaviour
{
    public Currency currencyType;
    public int amountToAdd = 1;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(AddCurrency);
    }

    void AddCurrency()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.Add(currencyType, amountToAdd);
            Debug.Log($"Added {amountToAdd} {currencyType.name}");
        }
    }
}