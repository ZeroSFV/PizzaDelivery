using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SizeModel
    {
        public int Size_Id { get; set; }

       
        public string Size_Name { get; set; }

        public SizeModel() { }

        public SizeModel(Size s)
        {
            Size_Id = s.Size_Id;
            Size_Name = s.Size_Name;
        }
    }
}
