﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsWarehouse
{
    public static class WarehouseRepository
    {
        
        public static List<string> GetListTypeEquipment(string TypeArea) 
        {
            List<string> list = new List<string>();
            foreach (var item in TelegramBot.Spareparts)
            {
                if (item.TypeAreaProduction is not null && item.TypeAreaProduction == TypeArea && !list.Contains(item.EquipmentName))
                {
                    if(item.EquipmentName.Contains(','))
                        {
                        var str = item.EquipmentName.Split(',');
                            foreach (var it in str)
	                        {
                                if(!list.Contains(it.TrimStart().TrimEnd()))
                                list.Add(it.TrimStart().TrimEnd());
                          	}
                        }
                    else
                            list.Add(item.EquipmentName);
                }
            }
            return list;
        }
        public static List<string> GetListArea()
        {
            List<string> list = new List<string>();
            foreach (var item in TelegramBot.Spareparts)
            {
                if(!list.Contains(item.TypeAreaProduction))
                list.Add(item.TypeAreaProduction);
            }
            return list;
        }
        public static List<string> GetListTypeSpareparets()
        {
            List<string> list = new List<string>();
            foreach (var item in TelegramBot.Spareparts)
            {
                if (!list.Contains(item.TypeNumber))
                    list.Add(item.TypeNumber);
            }
            return list;
        }
        public static List<string> GetListTypeEquipmentParts(string equipment)
        {
            List<string> list = new List<string>();
            foreach (var item in TelegramBot.Spareparts)
            {
                if (item.EquipmentName.Contains(equipment)) list.Add($"{item.Id}*|{item.TypeNumber}|{item.NumberSpareParts}|{item.CharacteristicsSpareParts}| [{item.WarehouseBalance} шт]");
            }
            return list;
        }
        public static List<string> GetListGoogleSearch(string equipment)
        {
            List<string> list = new List<string>();
            foreach (var item in TelegramBot.Spareparts)
            {
                string str = item.EquipmentName.ToLower()
                    + item.Brand.ToLower()
                    + item.CharacteristicsSpareParts.ToLower()
                    + item.TypeNumber.ToLower()
                    + item.NumberSpareParts.ToLower();
                if (str.Contains(equipment.ToLower()))
                    list.Add($"{item.Id}*" +
                        $"{item.TypeNumber} " +
                        $"{item.Brand} " +
                        $" {item.NumberSpareParts} " +
                        $" {item.EquipmentName} " +
                        $" {item.TypeAreaProduction} " +
                        $": {item.WarehouseBalance} шт");
            }
            return list;
        }

        public static List<string> GetListTypeParts(string typeSpare)
        {
            List<string> list = new List<string>();
            foreach (var item in TelegramBot.Spareparts)
            {
                if (item.TypeNumber == typeSpare) list.Add($"{item.Id}*|{item.EquipmentName}|{item.NumberSpareParts}|{item.CharacteristicsSpareParts}| [{item.WarehouseBalance} шт]");
            }
            return list;
        }
        public static Sparepart GetSparepart(string partsName)
        {
           if(partsName is not null)
            {
                string str = partsName.Split('*')[0];
                if (str.Length > 0)
                {
                    Sparepart sparepart = TelegramBot.Spareparts.FirstOrDefault(s => s.Id.Equals(Convert.ToInt32(str)));
                    if (sparepart != null)
                    {
                        sparepart.RealBalance--;
                    }
                    return sparepart;
                }
                else
                    return new Sparepart();
            }
           else
            return new Sparepart();
        }
    }
}
