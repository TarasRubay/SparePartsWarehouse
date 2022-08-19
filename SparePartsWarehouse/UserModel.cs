using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsWarehouse
{
    public class UserModel
    {
        public int Id { get; set; }
        public int CountRequest { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string LastMessage { get; set; } = "empty";
        public long TelegramId { get; set; } = 0;
        public int MenuLevel { get; set; } = 0;
        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
