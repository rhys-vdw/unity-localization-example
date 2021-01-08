using UnityEngine;
using System.Collections;

public class DreamText : MonoBehaviour {
    public TextMesh Text = null;

    public string[] Texts;

    void Start() {
        StartCoroutine(ShowAllText());
    }

    IEnumerator ShowAllText()
    {
        while (true) {
            foreach (var text in Texts)
            {
                Text.text = Localization.Instance.Translate(text);
                Text.color = Color.Lerp(Color.red, Color.blue, Random.value);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}