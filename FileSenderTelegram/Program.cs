using Deployf.Botf;

namespace FileSenderTelegram
{
    public class Program : BotfProgram
    {
        public static void Main(string[] args)
        {
            StartBot(args, onConfigure: (svc, _) =>
            {
            }, onRun: (_, _) =>
            {
            });
        }
    }
}