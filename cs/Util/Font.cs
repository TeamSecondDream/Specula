using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Specula.Util;

public class Font {
    public readonly Texture2D Texture;
    public readonly ushort LineHeight;
    public readonly ushort CharSpacing;
    
    private readonly string _charset;
    private readonly ushort[] _widthMap;
    private readonly ushort _gridWidth;

    public Font(Texture2D texture, string charset, ushort[] widthMap, ushort gridWidth, ushort lineHeight, ushort charSpacing) {
        Texture = texture;
        _charset = charset;
        _widthMap = widthMap;
        _gridWidth = gridWidth;
        LineHeight = lineHeight;
        CharSpacing = charSpacing;
    }

    public int GetWidth(char c) {
        int index = _charset.IndexOf(c);
        return _widthMap[index == -1 ? 0 : index];
    }

    public Rectangle GetRectangle(char c) {
        int index = _charset.IndexOf(c);
        if (index == -1) index = _charset.IndexOf(' ');
        return new Rectangle(index * _gridWidth, 0, _gridWidth, LineHeight);
    }
    
    public int GetWidth(string str) {
        int length = 0;
        if (str.Contains('\n')) {
            string[] lines = str.Split("\n");
            int longestId = -1;
            for (int i = 0; i < lines.Length; i++) {
                int l = lines[i].Length;
                if (l <= length) continue;
                length = l;
                longestId = i;
            }

            str = lines[longestId];
            length = 0;
        }

        for (int i = 0; i < str.Length; i++) {
            length += GetWidth(str[i]);
            if (i < str.Length - 1) {
                length += CharSpacing;
            }
        }

        return length;
    }

    public bool ContainsChar(char c) => _charset.Contains(c);
}