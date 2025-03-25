namespace EasyButtons.Example
{
    using UnityEngine;

    public class ButtonsExample : MonoBehaviour
    {
        public int a;
        public int b;

        public int c;

        [Button]
        public void SayMyName()
        {
            Debug.Log(name);
        }

        

        // Example use of the Expanded option.
        [Button("Expanded Button Example", Expanded = true)]
        public void TestExpandedButton(string message)
        {
            Debug.Log(message);
        }

        [Button("Special Name Editor Only", Mode = ButtonMode.DisabledInPlayMode)]
        public void TestButtonNameEditorOnly()
        {
            Debug.Log("Hello from special name button for editor only");
        }
    }
}