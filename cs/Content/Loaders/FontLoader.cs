using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Specula.Util;

namespace Specula.Content.Loaders;

[ResourceLoader]
public class FontLoader : ResourceLoader {
    public override bool ShouldLoadFile(string resourcePath) {
        return resourcePath.StartsWith("assets/fonts/") && resourcePath.EndsWith(".json");
    }

    public override void Load(string resourcePath, string absolutePath) {
        Resources.Fonts.Add(resourcePath, ReadFromFile(absolutePath));
    }

    public static Font ReadFromFile(string path) {
        string json = Resources.GetTextFileStream(path).ReadToEnd();
        ProtoFont font = JsonConvert.DeserializeObject<ProtoFont>(json);

        if (!Resources.Load(font.texture)) throw new Exception($"Texture not found: {font.texture}");
        string charset = font.charset;

        ushort[] widthMap = new ushort[font.charset.Length + 1];
        widthMap[font.charset.Length] = font.whitespace_width;
        ushort defaultWidth = font.char_widths["__default"];
        for (int i = 0; i < charset.Length; i++) {
            char curChar = charset[i];
            widthMap[i] = font.char_widths.GetValueOrDefault(curChar.ToString(), defaultWidth);
        }

        return new Font(Resources.Textures[font.texture], charset + " ", widthMap, font.grid_width, font.line_height, font.char_separation);
    }

    
    private class ProtoFont {
        // ReSharper disable class InconsistentNaming
        public string charset { get; set; }
        public string texture { get; set; }
        public Dictionary<string, ushort> char_widths { get; set; }
        public ushort char_separation { get; set; }
        public ushort whitespace_width { get; set; }
        public ushort line_height { get; set; }
        public ushort grid_width { get; set; }
    }
}