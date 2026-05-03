public class RollBuffer
{
    public bool IsBuffered => _timer.IsRunning;
    
    private readonly Timer _timer;

    public RollBuffer(float duration)
    {
        _timer = new Timer(duration);
    }

    public void Trigger()
    {
        _timer.Reset();
        _timer.Start();
    }
    
    public void Tick(float deltaTime)
    {
        _timer.Tick(deltaTime);
    }
}