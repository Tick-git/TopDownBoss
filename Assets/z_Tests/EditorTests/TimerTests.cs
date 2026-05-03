using NUnit.Framework;

public class TimerTests
{
    [Test]
    public void Timer_Stops_After_Duration()
    {
        var timer = new Timer(1f);
        timer.Start();

        timer.Tick(0.5f);
        Assert.IsTrue(timer.IsRunning);

        timer.Tick(0.5f);
        Assert.IsFalse(timer.IsRunning);
        Assert.AreEqual(1f, timer.Elapsed);
    }

    [Test]
    public void Timer_Does_Not_Run_When_Stopped()
    {
        var timer = new Timer(1f);
        timer.Start();
        timer.Stop();

        timer.Tick(1f);

        Assert.IsFalse(timer.IsRunning);
        Assert.AreEqual(0f, timer.Elapsed);
    }

    [Test]
    public void Timer_Reset_Works()
    {
        var timer = new Timer(1f);
        timer.Start();

        timer.Tick(1f);
        Assert.IsFalse(timer.IsRunning);

        timer.Reset();
        Assert.IsTrue(timer.IsRunning == false);
        Assert.AreEqual(0f, timer.Elapsed);
    }

    [Test]
    public void Timer_Completed_Event_Fires_Once()
    {
        var timer = new Timer(1f);
        int calls = 0;
        
        timer.Start();
        timer.Completed += () => calls++;

        timer.Tick(2f);
        timer.Tick(1f);

        Assert.AreEqual(1, calls);
        Assert.IsFalse(timer.IsRunning);
    }

}