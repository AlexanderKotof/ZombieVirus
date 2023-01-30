using TMPro;
using UnityEngine;

namespace ScreenSystem.Components
{
    public class TextComponent : WindowComponent
    {
        public TMP_Text text;

        private void OnValidate()
        {
            text = GetComponent<TMP_Text>();
        }

        public void SetText(string value)
        {
            text.text = value;
        }

        public void SetColor(Color color)
        {
            text.color = color;
        }
    }
}