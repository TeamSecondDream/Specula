using Microsoft.Xna.Framework.Audio;

namespace Specula.Content.Loaders; 

[ResourceLoader]
public class SoundEffectLoader : ResourceLoader {
    public override bool ShouldLoadFile(string resourcePath) {
        return resourcePath.StartsWith("sounds/") && resourcePath.EndsWith(".wav");
    }

    public override void Load(string resourcePath, string absolutePath) {
        Resources.Sounds.Add(resourcePath, ReadFromFile(absolutePath));
    }
    
    public static SoundEffect ReadFromFile(string path) => SoundEffect.FromFile(path);
}