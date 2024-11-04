using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();

            uut = new CookController(timer, display, powerTube, ui);
        }

        [TestCase(5)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(250)]
        [TestCase(800)]
        public void SetUpMaxPower_OnPowerTube(int maxPower)
        {
            uut.SetUpMaxPower(maxPower);
            powerTube.Received().SetUpMaxPower(Arg.Is<int>(maxPower));
        }

        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }


        [TestCase(1, TestName = "AddTimeCallReceived")]
        [TestCase(2, TestName = "AddTimeTwoCallsReceived")]
        [TestCase(3, TestName = "AddTimeThreeCallsReceived")]
        [TestCase(4, TestName = "AddTimeFourCallsReceived")]
		[Test]
        public void Cooking_PressedPowerButtonWhileCooking(int timesPressed)
        {
	      uut.StartCooking(50,60);

	      for (int i = 0; i < timesPressed; i++)
	      {
		      uut.AddTimeToTimer();
	      }

	      timer.Received(timesPressed).AddTime(60);
        }

    }
}