using NBomber.Contracts;

namespace NBomberXunitDemo.Steps
{
    public interface IDemoStep
    {
        IStep CreateStep(string stepName);
    }
}
