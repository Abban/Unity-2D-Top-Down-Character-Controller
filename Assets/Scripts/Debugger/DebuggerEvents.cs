namespace BBX.Debugger
{
    public class DebugMessageSignal
    {
        public DebugMessageSignal(
            string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public class DebugClearSignal
    {
    }
}