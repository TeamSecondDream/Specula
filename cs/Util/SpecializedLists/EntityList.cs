namespace Specula.Util.SpecializedLists; 

public class EntityList : DeferredList<AbstractEntity> {
    public void Update() {
        if (Count == 0) return;
        UpdateAll(Contents);
    }
    
    public void Draw() {
        if (Count == 0) return;
        DrawAll(Contents);
    }
}

