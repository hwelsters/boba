using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public const float lettersPerSecond = 10f;
    public const float inverseLettersPerSecond = 1f / lettersPerSecond;

    public GameObject dialogGameObject;
    public Text dialogText;

    public void ShowDialog(in string dialog)
    {
        dialogGameObject.SetActive(true);
        StartCoroutine(TypeDialog(dialog));
    }

    //REFACTOR
    public IEnumerator TypeDialog(string dialog)
    {
        string displayedDialog = "";

        foreach(char c in dialog)
        {
            displayedDialog += c;
            dialogText.text = displayedDialog;

            // WTF is this bullshit
            if (Input.GetKeyDown(KeyCode.Q))
            {
                dialogText.text = dialog;
                yield return new WaitForSeconds(0.1f);
                break;
            }
            
            yield return new WaitForSeconds(inverseLettersPerSecond);
        }

        while (!Input.GetKeyDown(KeyCode.Q)) yield return null;

        dialogGameObject.SetActive(false);
    }
}
