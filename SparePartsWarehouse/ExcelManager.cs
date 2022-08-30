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
            int a = 0;
            int b = 1;
            int c = 2;
            int d = 3;
            int i = 4;
            int f = 5;
            int g = 6;
            int e = 7;
            int j = 8;
            int k = 9;
            int l = 10;
            int m = 11;

            List<Sparepart> spareparts = new ();
            ExcelReader excel = new(path);
            List<List<object>> array = excel.read(1);
            int ind=0;
            foreach (List<object> item in array)
            {
                Sparepart sparepart = new Sparepart();
                sparepart.Id = ind++;
                sparepart.TypeAreaProduction = item[a] is not null? item[a].ToString() : "";
                sparepart.Brand = item[b] is not null? item[b].ToString() : "";
                sparepart.NumberSpareParts = item[c] is not null? item[c].ToString() : "";
                sparepart.TypeNumber = item[d] is not null? item[d].ToString() : "";
                sparepart.EquipmentName = item[i] is not null? item[i].ToString() : "";
                sparepart.CharacteristicsSpareParts = item[f] is not null? item[f].ToString() : "";
                sparepart.StateInEquipment = item[g] is not null? item[g].ToString() : "";
                sparepart.NumberShelfShelving = item[e] is not null? item[e].ToString() : "";
                sparepart.CriticalBalance = item[j] is not null? item[j].ToString() : "";
                sparepart.WarehouseBalance = item[k] is not null? item[k].ToString() : "";
                sparepart.HotChangeBalance = item[l] is not null? item[l].ToString() : "";
                sparepart.Notes = item[m] is not null? item[m].ToString() : "";
                if(sparepart.WarehouseBalance == "")
                sparepart.RealBalance = Convert.ToInt32(sparepart.WarehouseBalance);
                spareparts.Add(sparepart);
            }
            return spareparts;
        }
    }
}
