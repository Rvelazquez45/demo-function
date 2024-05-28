namespace DemoFunction.Logic;

public class SampleLogic : ISampleLogic
{
    public string SayHello(string firstName, string lastName)
    {
        return $"Hello {firstName} {lastName}! This is from the Dependency.";
    }
}
