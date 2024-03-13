using UnityEngine;

public class TextObject : MonoBehaviour
{
    [TextArea(3, 5)] public string[] text;
    [SerializeField] private bool typerwriter;

    public void Interact()
    {
        UIText.Instance.StartDisplayingText(text, typerwriter);
    }
}

