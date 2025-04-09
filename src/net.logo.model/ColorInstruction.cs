namespace net.logo.model;

public class ColorInstruction : IInstruction
{
    private string _color;

    public string Color => _color;

    public ColorInstruction(string color)
    {
        _color = color;
    }
}