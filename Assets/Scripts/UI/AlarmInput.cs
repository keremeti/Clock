using TMPro;
using UnityEngine;

public class AlarmInput : MonoBehaviour
{
    private TMP_InputField input;
    [SerializeField] private int max;
    private int number = 0;
    public int Number { get => number; }

    void Start()
    {
        input = GetComponent<TMP_InputField>();
        input.onValueChanged.AddListener(ValidateInput);
        input.onEndEdit.AddListener(FormatInput);
        input.text = "00";
    }

    // форматировать введенное
    private void FormatInput(string _string)
    {
        input.text = _string.PadLeft(2, '0');
    }

    // проверить введенные данные, поправить, если некорректные
    private void ValidateInput(string _string)
    {
        number = int.Parse(_string);
        if (number > max)
        {
            input.text = max.ToString();
        }
    }
}
