using SparePartsWarehouse;
using System;


string TelegramToken = "5303680583:AAGmGSOuuUnqiRJLMyuxr4ANgF-qsa5uLvo";
string PathUserList = "UserList.json";
string PathExcel = @$"C:\Users\taras.rubay\Downloads\Telegram Desktop\Залишки запчастин (1).xlsx";

TelegramBot Bot = new(TelegramToken,PathExcel,PathUserList);
Bot.Run();



