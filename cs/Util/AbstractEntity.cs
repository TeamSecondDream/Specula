using System;
using Specula.Scenes;

namespace Specula.Util; 

public abstract class AbstractEntity : IUpdatable, IRenderable, IComparable<AbstractEntity>, IComparable, IDisposable {
    public int ExecutionPriority { get; set; }
    protected Scene Scene { get; private set; }
    
    public abstract void Update();

    public virtual void PreUpdate() { }
    public virtual void PostUpdate() { }
    
    public abstract void Draw();
    public virtual void PreDraw() { }
    public virtual void PostDraw() { }
    
    public int CompareTo(AbstractEntity other) {
        return ExecutionPriority.CompareTo(other.ExecutionPriority);
    }

    public int CompareTo(object obj) {
        if (obj is AbstractEntity other) return CompareTo(other);
        return -1;
    }

    public void SetScene(Scene scene) {
        Scene = scene;
    }
    
    public virtual void Dispose() {
        GC.SuppressFinalize(this);
    }
}