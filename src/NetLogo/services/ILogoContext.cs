namespace NetLogo.services;

public interface ILogoContext
{
    string SampleName { get;  }
    
    string SampleDescription { get;  }
    
    string Draw { get; set; }
    
    string Source { get; set; }
    void SetSample(string sampleName);

    List<(string name, string description)> GetSamples();
}