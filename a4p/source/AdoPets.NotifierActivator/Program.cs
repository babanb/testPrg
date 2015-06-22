using AdoPets.Notifier;

namespace AdoPets.NotifierActivator
{
    class Program
    {
        static void Main(string[] args)
        {
            var notifier = new BaseNotifier();
            notifier.Notify();
        }
    }
}
