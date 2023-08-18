namespace Specula.Content; 

public abstract class ResourceLoader {
    public abstract bool ShouldLoadFile(string resourcePath);
    public abstract void Load(string resourcePath, string absolutePath);
}