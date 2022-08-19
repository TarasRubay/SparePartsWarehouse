using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SparePartsWarehouse
{
    static class UserInterface
    {
        public const long Dziubak = 862684408;
        public const long Rubay = 1143288883;
        public const string FindForEquipment = "Пошук запчастини по обладнанню";
        public const string FindForType = "Пошук запчастини по типу";
        public const string FindForPartsNumber = "Пошук запчастини по заказному коду";
        public const string FindForEquipmentService = "Пошук запчастини для ТО";

        public const string CSK = "ЦСК";
        public const string CVK = "ЦВК";

        public const string ChoiseTypeArea = "Виберіть Цех";
        public const string EquipmentType = "Виберіть тип обланднання";
        public const string EquipmentParts = "Виберіть запчастину";

        public const string ChoiseTypeParts = "Виберіть Тип Запчастини";
        public const string EquipmentTypeww = "Виберіть тип обланднання";
        public const string EquipmentPartsww = "Виберіть запчастину";


        public const string ElectricalParts = "Електричні запчастини";
        public const string MechanicalParts = "Механічні запчастини";

        public const string Follow = "підписатись на графік працівника";
        public const string Unfollow = "відписатись від графіка працівника";
        public const string WriteAll = "hello all";
        public const string ViewUserInShift = "viewUserShift";
        public const string ChoiceEmploye = "виберіть працівника";
        public const string ExitProfMenu = ".вийти";
        
        public const string Exit = "вийти";
        public const string Take = "беру зі складу";
        public const string Parts = "запчастина";

        public const string ViewOwnSchedule = "переглянути підписані графіки";
        public const string ViewScheduleShift = "переглянути графік зміни №";
        public const string ViewUser = "переглянути графік працівника";
        public const string ProMenu = "розширене меню";
        public const string empty = "empty";

        public static async void RecivingMessage(ITelegramBotClient client, Message msg,UserModel user, CancellationToken cancellationToken)
        {
            //Message sentMessage = await client.SendTextMessageAsync(
            //    chatId: user.TelegramId,
            //    text: "Ти кажеш:\n" + msg.Text);
            user.CountRequest++;
            
            if (msg.Text is not null && msg.Text == Exit || msg.Text == "/start")
            {
                try
                {
                    user.LastMessage = "empty";
                    user.MenuLevel = 0;
                    await client.SendTextMessageAsync(
                                msg.Chat.Id,
                                $"основне меню",
                                replyMarkup: MainMenu(),
                                cancellationToken : cancellationToken);
                }
                catch (Exception Ex)
                {

                }
            }
            if (user.LastMessage is null) user.LastMessage = empty;
            if (user.LastMessage == empty || user.LastMessage.Length > 25)
            {
                FirtMenu(client,msg,user,cancellationToken);
            }
            else
            {
                SecondMenu(client,msg,user,cancellationToken);
            }
          
            
        }


        public static async void FirtMenu(ITelegramBotClient client, Message msg, UserModel user, CancellationToken cancellationToken)
        {
            switch (msg.Text)
            {
                case Take:
                    try
                    {
                        user.MenuLevel = 0;
                        await client.SendTextMessageAsync(
                                    Dziubak,
                                    user.LastMessage + $"\n забрав {user}" 
                                    );
                        await client.SendTextMessageAsync(
                                msg.Chat.Id,
                                $"основне меню",
                                replyMarkup: MainMenu(),
                                cancellationToken: cancellationToken);
                        user.LastMessage = empty;
                    }
                    catch (Exception)
                    { }
                    break;
                case FindForEquipment:
                    try
                    {
                        user.LastMessage = ChoiseTypeArea;
                        user.MenuLevel = 1;
                        await client.SendTextMessageAsync(
                                    msg.Chat.Id,
                                    ChoiseTypeArea,
                                    replyMarkup: MenuLevel_1(),
                                    cancellationToken: cancellationToken);
                    }
                    catch (Exception)
                    { }
                    break;
                case FindForType:
                        try
                        {
                            user.MenuLevel = 1;
                            user.LastMessage = ChoiseTypeParts;
                            await client.SendTextMessageAsync(
                                        msg.Chat.Id,
                                        ChoiseTypeParts,
                                        replyMarkup: MenuLevel_ListTypeSpareparts(),
                                        cancellationToken: cancellationToken);
                        }
                        catch (Exception)
                        { }
                    break;
                
                default:
                    break;
            }
        }
        public static async void SecondMenu(ITelegramBotClient client, Message msg, UserModel user, CancellationToken cancellationToken)
        {
            switch (user.LastMessage)
            {
                case ChoiseTypeArea:
                    try
                    {
                        await client.SendTextMessageAsync(
                                    msg.Chat.Id,
                                    ChoiseTypeArea,
                                    replyMarkup: MenuLevel_1_listEquipment(msg.Text),
                                    cancellationToken: cancellationToken);
                    }
                    catch (Exception)
                    { }
                    user.MenuLevel = 1;
                    user.LastMessage = EquipmentParts;
                    break;
                case EquipmentParts:
                    try
                    {
                        await client.SendTextMessageAsync(
                                    msg.Chat.Id,
                                    EquipmentParts,
                                    replyMarkup: MenuLevel_2_listEquipmentParts(msg.Text),
                                    cancellationToken: cancellationToken);
                    }
                    catch (Exception)
                    { }
                    user.MenuLevel = 0;
                    user.LastMessage = Parts;
                    break;
                case Parts:
                    try
                    {
                        await client.SendTextMessageAsync(
                                    msg.Chat.Id,
                                    WarehouseRepository.GetSparepart(msg.Text).ToString(),
                                    replyMarkup: _Back(),
                                    cancellationToken: cancellationToken);
                        user.LastMessage = WarehouseRepository.GetSparepart(msg.Text).ToStr();
                    }
                    catch (Exception)
                    {
                        user.LastMessage = empty;
                    }
                    break;
                case ChoiseTypeParts:
                    try
                    {
                        await client.SendTextMessageAsync(
                                    msg.Chat.Id,
                                    EquipmentParts,
                                    replyMarkup: MenuLevel_1_GetListSparesByType(msg.Text),
                                    cancellationToken: cancellationToken);
                        user.LastMessage = Parts;
                    }
                    catch (Exception)
                    {
                        user.LastMessage = empty;
                    }
                    break;

                default:
                    break;
            }
        }

        public static async void FindForEquipmentLevel(int level)
        {
            if(level == 1)
            {

            }else if(level == 2)
            {

            }else if(level == 3)
            {

            }

        }
        public static ReplyKeyboardMarkup _Back()
        {

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                 {
                    new KeyboardButton[] { Exit },
                    new KeyboardButton[] { Take }
                })
            {
                ResizeKeyboard = true
            };
            return replyKeyboardMarkup;
        }
        public static ReplyKeyboardMarkup MainMenu()
        {

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                 {
                    new KeyboardButton[] { FindForEquipment, FindForType }
                    //new KeyboardButton[] { FindForPartsNumber, FindForEquipmentService },
                })
            {
                ResizeKeyboard = true
            };
            return replyKeyboardMarkup;
        }
        public static ReplyKeyboardMarkup MenuLevel_1()
        {

            List<KeyboardButton[]> butIns = new();
            butIns.Add(new KeyboardButton[] { Exit });
            foreach (var item in WarehouseRepository.GetListArea())
            {
                butIns.Add(new KeyboardButton[] { item });
            }
            ReplyKeyboardMarkup replyKeyboardMarkup = new(butIns.ToArray())
            {
                ResizeKeyboard = true,
                Selective = true
            };

            return replyKeyboardMarkup;
        }
        public static ReplyKeyboardMarkup MenuLevel_ListTypeSpareparts()
        {

            List<KeyboardButton[]> butIns = new();
            butIns.Add(new KeyboardButton[] { Exit });
            foreach (var item in WarehouseRepository.GetListTypeSpareparets())
            {
                butIns.Add(new KeyboardButton[] { item });
            }
            ReplyKeyboardMarkup replyKeyboardMarkup = new(butIns.ToArray())
            {
                ResizeKeyboard = true,
                Selective = true
            };

            return replyKeyboardMarkup;
        }
        public static IReplyMarkup MenuLevel_1_listEquipment(string areaType)
        {
            List<KeyboardButton[]> butIns = new();
            butIns.Add(new KeyboardButton[] { Exit });
            foreach (var item in WarehouseRepository.GetListTypeEquipment(areaType))
            {
                butIns.Add(new KeyboardButton[] { item });
            }
            ReplyKeyboardMarkup replyKeyboardMarkup = new(butIns.ToArray())
            {
                ResizeKeyboard = true,
                Selective = true
            };
            
            return replyKeyboardMarkup; 
        }
        public static IReplyMarkup MenuLevel_1_GetListSparesByType(string spareType)
        {
            List<KeyboardButton[]> butIns = new();
            butIns.Add(new KeyboardButton[] { Exit });
            foreach (var item in WarehouseRepository.GetListTypeParts(spareType))
            {
                butIns.Add(new KeyboardButton[] { item });
            }
            ReplyKeyboardMarkup replyKeyboardMarkup = new(butIns.ToArray())
            {
                ResizeKeyboard = true,
                Selective = true
            };

            return replyKeyboardMarkup;
        }
        public static IReplyMarkup MenuLevel_2_listEquipmentParts(string eequipment)
        {
            List<KeyboardButton[]> butIns = new();
            butIns.Add(new KeyboardButton[] { Exit });
            foreach (var item in WarehouseRepository.GetListTypeEquipmentParts(eequipment))
            {
                butIns.Add(new KeyboardButton[] { item });
            }
            ReplyKeyboardMarkup replyKeyboardMarkup = new(butIns.ToArray())
            {
                ResizeKeyboard = true,
                Selective = true
            };

            return replyKeyboardMarkup;
        }

    }
}
