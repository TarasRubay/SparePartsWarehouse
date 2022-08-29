using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SparePartsWarehouse
{
    public class TelegramBot
    {
        private static System.Timers.Timer TimeToRead;
        public static DateTime LastUpdateExcel;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        public static List<Sparepart> Spareparts { get; set; } = new();
        public static List<UserModel> Users { get; set; } = new();
        

        private static string _token;
        private static string _excelPath;
        private static string _userListPath;

        private static TelegramBotClient botClient;
        private CancellationTokenSource cancellationTokenSource;

        public TelegramBot(string Token, string excelPath, string userListPath)
        {
            _token = Token;
            _excelPath = excelPath;
            _userListPath = userListPath;
        }
        public void Run()
        {
            DataManager manager = new(_userListPath);
            Users = manager.LoadDataJSON();
            Console.WriteLine("staet bot");
            Task task = StartRecivingBots();
            var handle = GetConsoleWindow();
            TimeToRead = new System.Timers.Timer();
            TimeToRead.Interval = 1000;
            TimeToRead.Elapsed += OnTimedEvent;
            TimeToRead.AutoReset = true;
            TimeToRead.Enabled = true;
            //ShowWindow(handle, SW_HIDE);
            ShowWindow(handle, SW_SHOW);
            Console.ReadLine();
        }
        public async Task StartRecivingBots()
        {
            botClient = new TelegramBotClient(_token);
            cancellationTokenSource = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cancellationTokenSource.Token
            );

            var me = await botClient.GetMeAsync();

            //Console.WriteLine($"Start listening for @{me.Username}");
            //Console.ReadLine();

            // Send cancellation request to stop bot
            //cts.Cancel();

        }
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
            {
                Console.WriteLine("update");
                return;
            }
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            if (message is not null)
            {
                await Login(message,cancellationToken);
                Console.WriteLine("message");
            }
            
            //var chatId = message.Chat.Id;
            
            //// Echo received message text

            //Message sentMessage = await botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: "You said:\n" + messageText,
            //    cancellationToken: cancellationToken);
        }
        async Task Login(Message message, CancellationToken cancellationToken)
        {
            UserModel user = Users.Find(u => u.TelegramId == message.Chat.Id);
            if (message.Text == null) message.Text = "";

            try
            {
                if (user is not null)
                {
                    UserInterface.RecivingMessage(botClient,message,user,cancellationToken);   
                }
                else
                {
                    if (message.Text == "0000")
                    {
                        await botClient.SendTextMessageAsync(
                            message.Chat.Id,
                            $"Новий користувач {message.Chat.FirstName} {message.Chat.LastName}\nприйнято",
                            replyMarkup: UserInterface.MainMenu(),
                            cancellationToken: cancellationToken
                            );
                        if (!Users.Exists(it => it.Id == message.Chat.Id))
                        {
                            UserModel us = new()
                            {
                                TelegramId = message.Chat.Id,
                                Name = message.Chat.FirstName,
                                Surname = message.Chat.LastName,
                                CountRequest = 0
                            };
                            Users.Add(us);
                            DataManager manager = new(_userListPath);
                            manager.SaveDataJSON(Users);
                        }
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                         message.Chat.Id,
                         $"Новий користувач {message.Chat.FirstName} {message.Chat.LastName}\nВведіть пінкод:"
                         , cancellationToken: cancellationToken);
                    }
                }
            }
            catch (Exception Ex)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, Ex.Message, cancellationToken: cancellationToken);
                //BagReport.SetBagAndSave(Ex.Message, Program.pathBagJSON);
            }
        }
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            CancellationTokenSource cancellation = new CancellationTokenSource();
            Console.WriteLine($"{LastUpdateExcel} {e.SignalTime}");
            if (LastUpdateExcel < new FileInfo(_excelPath).LastWriteTime)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(_excelPath))
                    {
                        Console.WriteLine("The file be read:");
                       //Console.WriteLine("delay 5 sec");
                        LastUpdateExcel = new FileInfo(_excelPath).LastWriteTime;
                    }
                    //Console.WriteLine("delay 5 sec");
                    //Thread.Sleep(5000);
                    //ExcelManager excelManager = new(_excelPath);
                    //Spareparts = excelManager.ReadSparepart();
                    //Console.WriteLine("delay 5 sec");
                    //Thread.Sleep(5000);
                    //excelManager = new(_excelPath);
                    //Spareparts = excelManager.ReadSparepart();
                    var res = Task.Run(async delegate
                    {
                        await Task.Delay(5000, cancellation.Token);
                        ExcelManager excelManager = new(_excelPath);
                        Spareparts = excelManager.ReadSparepart();
                        await Task.Delay(5000, cancellation.Token);
                        excelManager = new(_excelPath);
                        Spareparts = excelManager.ReadSparepart();
                        return "Reading is done";
                    });
                    res.Wait();
                    //if(res is not null)
                    //{
                    //    Console.WriteLine("delay 10 sec");
                    //    var res2 = Task.Run(async delegate
                    //    {
                    //        await Task.Delay(10000, cancellation.Token);
                    //        ExcelManager excelManager = new(_excelPath);
                    //        Spareparts = excelManager.ReadSparepart();
                    //        return "Reading is done";
                    //    });
                    //    res2.Wait();
                    //    Console.WriteLine(res2.Status);
                    //}
                    // Console.WriteLine(res.Status);


                    foreach (var part in Spareparts)
                    {
                        Console.WriteLine(part);
                        Console.WriteLine("//////////////////////////////////////////");
                    }
                }
                catch (Exception Ex)
                {

                    Console.WriteLine(Ex.Message);
                    // BagReport.SetBagAndSave(Ex.Message, Program.pathBagJSON);
                }

            }
        }
    }
}