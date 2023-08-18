namespace Specula.Util; 

public interface IUpdatable {
    void PreUpdate();
    void Update();
    void PostUpdate();
}