using TMPro;
using UnityEngine;

public class SourceDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    [SerializeField] private SourceManager sourceManager;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        // заполнение списка существующими источниками
        foreach (var dateTimeSource in sourceManager.dateTimeSources)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = dateTimeSource.Title });
        }
        dropdown.captionText.text = dropdown.options[dropdown.value].text;

        SelectedItem();
        dropdown.onValueChanged.AddListener(delegate { SelectedItem(); });
    }

    private void SelectedItem()
    {
        int index = dropdown.value;
        EventManager.SendDropdownItemSelected(dropdown.options[index].text);
    }
}
