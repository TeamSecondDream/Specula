using Microsoft.Xna.Framework.Graphics;

namespace Specula.Content.Loaders; 

[ResourceLoader]
public class Texture2DLoader : ResourceLoader {
    public override bool ShouldLoadFile(string resourcePath) {
        return resourcePath.EndsWith(".png");
    }
    
    public override void Load(string resourcePath, string absolutePath) {
        Resources.Textures.Add(resourcePath, ReadFromFile(absolutePath));
    }

    public static Texture2D ReadFromFile(string path) =>
        Texture2D.FromFile(Engine.Graphics.GraphicsDevice, path);
}