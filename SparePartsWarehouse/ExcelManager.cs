using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsWarehouse
{
    internal class ExcelManager
    {
        string path;
        public ExcelManager(string path)
        {
            this.path = path;
        }
        public List<Sparepart> ReadSparepart()
        {
            List<Sparepart> spareparts = new ();
            ExcelReader excel = new(path);
            List<List<object>> array = excel.read(1);
            int ind=0;
            foreach (List<object> item in array)
            {
                Sparepart sparepart = new Sparepart();
                sparepart.Id = ind++;
                sparepart.TypeAreaProduction = item[0] is not null? item[0].ToString() : "";
                sparepart.NumberSpareParts = item[1] is not null? item[1].ToString() : "";
                sparepart.TypeNumber = item[2] is not null? item[2].ToString() : "";
                sparepart.EquipmentName = item[3] is not null? item[3].ToString() : "";
                sparepart.CharacteristicsSpareParts = item[4] is not null? item[4].ToString() : "";
                sparepart.StateInEquipment = item[5] is not null? item[5].ToString() : "";
                sparepart.NumberShelfShelving = item[6] is not null? item[6].ToString() : "";
                sparepart.CriticalBalance = item[7] is not null? item[7].ToString() : "";
                sparepart.WarehouseBalance = item[8] is not null? item[8].ToString() : "";
                sparepart.HotChangeBalance = item[9] is not null? item[9].ToString() : "";
                sparepart.Notes = item[10] is not null? item[10].ToString() : "";
                if(sparepart.WarehouseBalance == "")
                sparepart.RealBalance = Convert.ToInt32(sparepart.WarehouseBalance);
                spareparts.Add(sparepart);
            }
            return spareparts;
        }
    }
}
