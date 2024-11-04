using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit;

[TestFixture]
public class BuzzerTest
{
    
    private Buzzer uut;
    private IOutput output;

    [SetUp]
    public void Setup()
    {
        output = Substitute.For<IOutput>();
        uut = new Buzzer(output);
    }

    [Test]
    public void Activate_CorrectOutput()
    {
        uut.Activate();
        output.OutputLine(Arg.Is<string>(str => str.Contains("beep")));
    }

    [Test]
    public void NotActivating_NoOutPut()
    {
        output.DidNotReceive().OutputLine(Arg.Do<string>(str => str.Contains("beep")));
    }
}
