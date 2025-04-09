namespace net.logo.model;

public class Parameter : IExpression
{
    private string _name;
    public string Name => _name;
    
    public Parameter(string name)
    {
        _name = name;
    }
}