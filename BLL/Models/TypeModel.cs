using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TypeModel
    {
        public int Type_Id { get; set; }

        public string Type_Name { get; set; }

        public TypeModel() { }

        public TypeModel(DAL.Entities.Type t)
        {
            Type_Id = t.Type_Id;
            Type_Name = t.Type_Name;
        }
    }
}
