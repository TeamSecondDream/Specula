using Specula.Graphics;
using Specula.Scenes;

namespace Specula.Util.SpecializedLists; 

public class RendererList : DeferredList<Renderer>, IRenderable {

    private Scene _scene;
    
    internal RendererList(Scene scene) {
        _scene = scene;
    }
    
    public void Update() {
        foreach (Renderer renderer in Contents) {
            renderer.Update(_scene);
        }
    }

    public void PreDraw() {
        for (int i = 0; i < Count; i++) {
            if (!Contents[i].Visible)
                continue;
            Drawing.Renderer = Contents[i];
            Contents[i].BeforeRender(_scene);
        }
    }

    public void Draw() {
        for (int i = 0; i < Count; i++) {
            if (!Contents[i].Visible)
                continue;
            Drawing.Renderer = Contents[i];
            Contents[i].Render(_scene);
        }
    }

    public void PostDraw() {
        for (int i = 0; i < Count; i++) {
            if (!Contents[i].Visible)
                continue;
            Drawing.Renderer = Contents[i];
            Contents[i].AfterRender(_scene);
        }
    }
}

