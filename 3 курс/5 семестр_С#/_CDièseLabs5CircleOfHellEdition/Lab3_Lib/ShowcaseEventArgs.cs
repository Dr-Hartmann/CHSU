namespace LibShowcase
{
    public class ShowcaseEventArgs
    {
        public ShowcaseEventArgs(string message, string type)
        {
            Message = message;
            Type = type;
        }
        public string Message { get; }
        public string Type { get; }
    }
}