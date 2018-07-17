using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoCodeHL {
    namespace Utilities {

        public class TextSize
        {
            private Hashtable _charWidthMap; //map character -> width

            private TextMesh _textMesh;
            private Renderer _renderer;
            private string _originalText;

            public TextSize(TextMesh textMesh, MeshRenderer renderer)
            {
                _textMesh = textMesh;
                _originalText = _textMesh.text;
                _renderer = renderer;
                _charWidthMap = new Hashtable();
                calculateSpaceCharWidth();
            }

            private void calculateSpaceCharWidth()
            {
                _textMesh.text = "a";
                float aw = _renderer.bounds.size.x;
                _textMesh.text = "a a";
                float cw = _renderer.bounds.size.x - 2 * aw;

                _charWidthMap.Add(' ', cw);
                _charWidthMap.Add('a', aw);

                _textMesh.text = _originalText;
            }

            public float GetTextWidth(string text)
            {
                char[] charList = text.ToCharArray();
                float calculatedWidth = 0;
                foreach(char c in charList){
                    if (_charWidthMap.ContainsKey(c))
                    {
                        calculatedWidth += (float)_charWidthMap[c];
                    }
                    else
                    {
                        _textMesh.text = "" + c;
                        float charWidth = _renderer.bounds.size.x;
                        _charWidthMap.Add(c, charWidth);
                        calculatedWidth += charWidth;
                    }
                }

                _textMesh.text = _originalText;
                return calculatedWidth;
            }

            public float width { get { return GetTextWidth(_textMesh.text); } }
            public float height { get { return _renderer.bounds.size.y; } }

            public string GetClippedText(string text, float maxWidth) {
                string clippedTextEnding = "...";
                string clippedText = "";
                float clippedTextEndingWidth = GetTextWidth(clippedTextEnding);
                char[] charList = text.ToCharArray();
                float calculatedWidth = 0;
                foreach (char c in charList)
                {
                    if (_charWidthMap.ContainsKey(c))
                    {
                        calculatedWidth += (float)_charWidthMap[c];
                    }
                    else
                    {
                        _textMesh.text = "" + c;
                        float charWidth = _renderer.bounds.size.x;
                        _charWidthMap.Add(c, charWidth);
                        calculatedWidth += charWidth;
                    }

                    if ((calculatedWidth + clippedTextEndingWidth)  < maxWidth) {
                        clippedText += c;
                    } else {
                        return clippedText + clippedTextEnding;
                    }
                }

                return text;
            }
        }
    }
}
