namespace NetLogo.services;

public class LogoCompilationResult
{
    public bool Success { get; set; }
    
    public string Draw {get; set;}
    
    public IEnumerable<string> Errors {get; set;}
    
    public static implicit operator LogoCompilationResult(string draw)
    {
        return new LogoCompilationResult()
        {
            Draw = draw,
            Success = true
        };
    }

    public static implicit operator LogoCompilationResult(List<string> errors)
    {
        return new LogoCompilationResult()
        {
            Success = false,
            Errors = errors
        };
    }
    
}