using SparePartsWarehouse;
using System;


string TelegramToken = "5303680583:AAGmGSOuuUnqiRJLMyuxr4ANgF-qsa5uLvo";
string PathUserList = "UserList.json";
string PathToExcel = "PathToExcelFile.json";
string PathExcel = @$"C:\Users\taras.rubay\OneDrive - KORM_ENZ\Запчастини\Залишки запчастин.xlsx";

//TelegramBot Bot = new(TelegramToken,DataManager.LoadFileExcelJSON(PathToExcel),PathUserList);
TelegramBot Bot = new(TelegramToken, PathExcel, PathUserList);
Bot.Run();



