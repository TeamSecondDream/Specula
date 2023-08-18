using System;
using Microsoft.Xna.Framework.Graphics;
using Specula.Scenes;

namespace Specula.Graphics;

public abstract class Renderer {
    public Camera Camera = new();
    public bool Visible = true;
    public virtual void Update(Scene scene) { }
    public virtual void BeforeRender(Scene scene) { }
    public virtual void Render(Scene scene) { }
    public virtual void AfterRender(Scene scene) { }
}

public class DefaultRenderer : Renderer {
    public BlendState BlendState;
    public SamplerState SamplerState;
    public Effect Effect;

    public DefaultRenderer() {
        BlendState = BlendState.AlphaBlend;
        SamplerState = SamplerState.LinearClamp;
    }
    
    public override void Render(Scene scene) {

        Drawing.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState, DepthStencilState.None, RasterizerState.CullNone, Effect, Camera.Matrix * Engine.ScreenMatrix);
        
        scene.Entities.Draw();

        Drawing.SpriteBatch.End();
    }
}