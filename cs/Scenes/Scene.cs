using System;
using Specula.Graphics;
using Specula.Util;
using Specula.Util.SpecializedLists;

namespace Specula.Scenes;

public abstract class Scene : IUpdatable, IRenderable {
    public readonly EntityList Entities;
    public readonly RendererList Renderers;
    public event Action OnEndOfFrame;

    public bool Focused;
    public bool Paused;
    public double TimeSinceLoaded;
    public double RawTimeSinceLoaded;

    protected Scene(Renderer primaryRenderer) {
        Entities = new EntityList();
        Engine.PrimaryRenderer = primaryRenderer;
        Renderers = new RendererList(this);
        Renderers.Add(primaryRenderer);
    }

    public abstract void Start();
    public abstract void End();

    public virtual void PreUpdate() {
        if (!Paused)
            TimeSinceLoaded += Engine.DeltaTime;
        RawTimeSinceLoaded += Engine.RawDeltaTime;

        foreach (var entity in Entities.ToAdd) {
            entity.SetScene(this);
        }
        
        Entities.UpdateChanges();
        Renderers.UpdateChanges();
    }

    public virtual void Update() {
        if (Paused) return;
        Entities.Update();
        Renderers.Update();
    }

    public virtual void PostUpdate() {
        if (OnEndOfFrame != null) {
            OnEndOfFrame.Invoke();
            OnEndOfFrame = null;
        }
    }


    public virtual void PreDraw() {
        Renderers.PreDraw();
    }

    public virtual void Draw() {
        Renderers.Draw();
    }

    public virtual void PostDraw() {
        Renderers.PostDraw();
    }

    public virtual void OnGraphicsReset() { }
    public virtual void OnGraphicsCreate() { }
    
    public bool OnInterval(float interval) {
        return (int)((TimeSinceLoaded - Engine.DeltaTime) / interval) < (int)(TimeSinceLoaded / interval);
    }

    public bool OnInterval(float interval, float offset) {
        return Math.Floor((TimeSinceLoaded - offset - Engine.DeltaTime) / interval) <
               Math.Floor((TimeSinceLoaded - offset) / interval);
    }

    public bool OnRawInterval(float interval) {
        return (int)((RawTimeSinceLoaded - Engine.RawDeltaTime) / interval) < (int)(RawTimeSinceLoaded / interval);
    }

    public bool OnRawInterval(float interval, float offset) {
        return Math.Floor((RawTimeSinceLoaded - offset - Engine.RawDeltaTime) / interval) <
               Math.Floor((RawTimeSinceLoaded - offset) / interval);
    }
}