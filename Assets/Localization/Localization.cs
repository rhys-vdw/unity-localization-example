using UnityEngine;
using System.Collections.Generic;
using MS.LitCSV;

public enum LanguageCode {
    En = 0,
    De = 1,
    It = 2,
}

public class Localization : MonoBehaviour {
    [SerializeField] TextAsset _translationsFile = null;
    Dictionary<string, string[]> _translations = new Dictionary<string, string[]>();
    /*
    {
        Hello => [ 'Hello', 'Tag', 'Buonjuorno!' ],
        Goodbye => [ 'Goodbye', 'Tag', 'Beliissiomo endo' ]
    }
    */

    public static Localization Instance { get; private set; }

    public LanguageCode Language;

    void Awake() {
        if (Instance != null) throw new System.InvalidOperationException("Already exists");
        Instance = this;

        var reader = CSVReader.ReadText(_translationsFile.text);
        foreach (var line in reader.ToArray()) {
            // Take the first element (the English) and use it as the key.
            // The entire row, including English, is the value.
            _translations.Add(line[0], line);
        }
    }

    public string Translate(string englishString) {
        var languageIndex = (int) Language;
        if (
            _translations.TryGetValue(englishString, out var row) &&
            row.Length > languageIndex &&
            !string.IsNullOrWhiteSpace(row[languageIndex])
        ) {
            return row[languageIndex];
        } else {
            Debug.LogError($"No translation for '{englishString}' found for {Language}");
            return englishString;
        }
    }
}