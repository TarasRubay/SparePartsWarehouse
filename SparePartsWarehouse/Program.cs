using SparePartsWarehouse;
using System;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

const int SW_HIDE = 0;
const int SW_SHOW = 5;
var handle = GetConsoleWindow();
ShowWindow(handle, 0);
//string TelegramToken = "5303680583:AAGmGSOuuUnqiRJLMyuxr4ANgF-qsa5uLvo";
string TelegramToken = "1912296215:AAHlgaOhoqY0zApv_XILroulH0Y8VWV1RFQ";
string PathUserList = "UserList.json";
string PathToExcel = "PathToExcelFile.json";
string PathExcel = @$"C:\Users\taras.rubay\OneDrive - KORM_ENZ\Запчастини\Залишки запчастин.xlsx";
//string PathExcel = @$"C:\Users\taras.rubay\OneDrive - KORM_ENZ\Запчастини\Залишки запчастин-kth-rubay.xlsx";
//ShowWindow(handle, SW_SHOW);
//TelegramBot Bot = new(TelegramToken,DataManager.LoadFileExcelJSON(PathToExcel),PathUserList);
ShowWindow(handle, 0);
TelegramBot Bot = new(TelegramToken, PathExcel, PathUserList);
Bot.Run();
ShowWindow(handle, 0);



