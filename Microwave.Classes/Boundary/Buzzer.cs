using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary;

public class Buzzer : IBuzzer
{
    private IOutput myOutput;

    public Buzzer(IOutput output)
    {
        myOutput = output;
    }
    
    public void Activate()
    {
        myOutput.OutputLine("Buzzer - beep beep beep");
    }
}