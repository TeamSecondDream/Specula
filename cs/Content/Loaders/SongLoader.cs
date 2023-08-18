using System;
using Microsoft.Xna.Framework.Media;

namespace Specula.Content.Loaders; 

[ResourceLoader]
public class SongLoader : ResourceLoader {
    public override bool ShouldLoadFile(string resourcePath) {
        return resourcePath.StartsWith("songs/") && resourcePath.EndsWith(".wav");
    }

    public override void Load(string resourcePath, string absolutePath) {
        Resources.Songs.Add(resourcePath, ReadFromFile(absolutePath));
    }
    
    public static Song ReadFromFile(string path) => Song.FromUri(path, new Uri(path));

}