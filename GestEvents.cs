public delegate void PoseRecognized();

public class GestEvents
{
    public event PoseRecognized PoseRecognized;

    public virtual void OnPoseRecognized(){
        PoseRecognized?.Invoke();
    }

}
