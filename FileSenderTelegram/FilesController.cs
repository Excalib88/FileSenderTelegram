using System.IO;
using System.Threading.Tasks;
using Deployf.Botf;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace FileSenderTelegram
{
    public class FilesController: BotController
    {
        private readonly IConfiguration _configuration;

        public FilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Action("/start", "Start to work with bot")]
        public void Start()
        {
            PushL("Hello world!");
        }

        [Action("/file", "Download file")]
        public async Task DownloadFile(string fileName)
        {
            // /file abc.txt
            var filePath = System.IO.Path.Combine(_configuration["FileStoragePath"], fileName);

            if (File.Exists(filePath))
            {
                await using var stream = File.Open(filePath, FileMode.Open);
                await Context.Bot.Client.SendDocumentAsync(Context.GetSafeChatId()!, new InputOnlineFile(stream, fileName));
            }
            else
            {
                PushL("Файл не найден");
            }
        }

        [On(Handle.Unknown)]
        public async Task Unknown()
        {
            if (Context.Update.Message?.Document is not null)
            {
                var document = Context.Update.Message?.Document;
                var file = await Context.Bot.Client.GetFileAsync(document!.FileId);
                var filePath = System.IO.Path.Combine(_configuration["FileStoragePath"], document.FileName!);

                await using var fs = new FileStream(filePath, FileMode.Create);
                await Context.Bot.Client.DownloadFileAsync(file.FilePath!, fs);
                
                PushL($"Файл {file.FilePath} был скачен в {filePath}");
            }
        }
    }
}