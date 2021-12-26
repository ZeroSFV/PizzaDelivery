using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class StatusModel
    {
        public int Status_Id { get; set; }

        public string Status_Name { get; set; }

        public StatusModel() { }

        public StatusModel(Status st)
        {
            Status_Id = st.Status_Id;
            Status_Name = st.Status_Name;
        }
    }
}
