using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace UtilityScripts
{
    // Nice idea, maybe need to figure it out, though.
    public static class LabelUtilities
    {
        public static TextMeshPro GetTextLabel(
            TextMeshPro label,
            float? fontSize = 45,
            TextAlignmentOptions? alignment = TextAlignmentOptions.Center,
            Color? color = null,
            string text = ""
        )
        {
            label.fontSize = fontSize ?? label.fontSize;
            label.alignment = alignment ?? label.alignment;
            label.color = color ?? label.color;
            label.text = text ?? label.text;

            return label;
        }
    }
}