using SharpFileSystem.FileSystems;

namespace NetLogo.services;

public class LogoContext : ILogoContext
{
    private string _sampleName;
    
    public string SampleName
    {
        get => _sampleName;
        set
        {
            _sampleName = value;
        }
    }
    
    private string _description;
    public string SampleDescription
    {
        get => _description;
        set
        {
            _description = value;
        }
    }
    

    private string _draw = "";

    public string Draw
    {
        get => _draw;
        set => _draw = value;
    }

    public string Source
    {
        get => _source;
        set
        {
            _source = value;
        }
    }

    public LogoContext()
    {
    }
    
    public void SetSample(string sampleName)
    {
        if (string.IsNullOrEmpty(sampleName))
        {
            Draw = "";
            Source = "";
            _description = "";
            _draw = "";
        }
        else
        {
            var sample = Samples[sampleName];
            
            Source = getSampleSource(sampleName);
            _description = sample.description;
            _sampleName = sampleName;
            
            _draw = "";
        }
    }

    

    public void SetSource(string source)
    {
        Source = source;
    }

    public void SetDraw(string draw)
    {
        Draw = draw;
    }
    
    
    
    private Dictionary<string, string> SamplesSources = new Dictionary<string, string>();

    private Dictionary<string, (string file, string description)> Samples = new Dictionary<string, (string file, string description)>()
    {
        { "", ("","") },
        { "polygones", ("polygones.logo","drawing polygones") },
        { "trees", ("trees.logo","drawing trees") }
    };

    private string _source;

    public List<(string name, string description)> GetSamples()
    {
        return Samples.Select(x =>(x.Key, x.Value.description)).ToList();
    }

    
    
    public string getSampleSource(string sampleName)
    {
        string sampleSource = "";
        if (!SamplesSources.TryGetValue(sampleName, out sampleSource))
        {
            
            if (Samples.TryGetValue(sampleName, out var d ))
            {
                var fs = new EmbeddedResourceFileSystem(GetType().Assembly);
                sampleSource = fs.ReadAllText($"/samples/{d.file}");
                SamplesSources[sampleName] = sampleSource;
            }
        }

        return sampleSource;
    }
}