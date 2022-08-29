using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsWarehouse
{
    public class Sparepart
    {
        public int Id { get; set; }
        /// <summary>
        /// ЦЕХ
        /// </summary>
        public string TypeAreaProduction { get; set; }
        /// <summary>
        /// Заказний номер
        /// </summary>
        public string NumberSpareParts { get; set; }
        /// <summary>
        /// Тип запчастини
        /// </summary>
        public string TypeNumber { get; set; }
        /// <summary>
        /// Назва обладнання
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// Характеристика запчастини
        /// </summary>
        public string CharacteristicsSpareParts { get; set; }
        /// <summary>
        /// На обладнанні
        /// </summary>
        public string StateInEquipment { get; set; }
        /// <summary>
        /// Стелаж полиця
        /// </summary>
        public string NumberShelfShelving { get; set; }
        /// <summary>
        /// Критичний залишок
        /// </summary>
        public string CriticalBalance { get; set; }
        /// <summary>
        /// Склад
        /// </summary>
        public string WarehouseBalance { get; set; }
        /// <summary>
        /// Склад
        /// </summary>
        public int RealBalance { get; set; }
        /// <summary>
        /// Гаряча заміна
        /// </summary>
        public string HotChangeBalance { get; set; }
        /// <summary>
        /// Примітки
        /// </summary>
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"ЦЕХ: {TypeAreaProduction}\n" +
                   $"Заказний №: {NumberSpareParts}\n" +
                   $"Тип: {TypeNumber}\n" +
                   $"Назва: {EquipmentName}\n" +
                   $"Характеристика: {CharacteristicsSpareParts}\n" +
                   $"На обладнанні: {StateInEquipment}\n" +
                   $"Стелаж №: {NumberShelfShelving}\n" +
                   $"На складі: {WarehouseBalance}\n" +
                   $"Гаряча заміна: {HotChangeBalance}\n" +
                   $"Примітки: {Notes}";
        }
        public string ToStr()
        {
            return $"ЦЕХ: {TypeAreaProduction}\n" +
                   $"Заказний №: {NumberSpareParts}\n" +
                   $"Тип: {TypeNumber}\n" +
                   $"Назва: {EquipmentName}\n" +
                   $"Характеристика: {CharacteristicsSpareParts}\n" +
                   $"На обладнанні: {StateInEquipment}\n" +
                   $"Стелаж №: {NumberShelfShelving}\n" +
                   $"На складі залишилось: {Convert.ToInt32(WarehouseBalance) - 1}\n" +
                   $"Гаряча заміна: {HotChangeBalance}\n" +
                   $"Примітки: {Notes}";
        }
    }
}
