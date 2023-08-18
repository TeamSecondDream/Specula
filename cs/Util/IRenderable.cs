namespace Specula.Util; 

public interface IRenderable {
    void PreDraw();
    void Draw();
    void PostDraw();
}