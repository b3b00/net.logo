namespace net.logo.model;

public class PenInstruction : IInstruction
{
    private bool _penDown;

    public bool PenDown => _penDown;
    
    public bool PenUp => !_penDown;

    public PenInstruction(bool penDown)
    {
        _penDown = penDown;
    }
}