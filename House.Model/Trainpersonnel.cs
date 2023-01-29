using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Model
{
    /// <summary>
    /// 培训人员表
    /// </summary>
    public class Trainpersonnel
    {
        [Key]
        public int Id { get; set; }
        public string Tra_Id { get; set; }
        public string TrainId { get; set; }
        public string Participants { get; set; }
        public string Department { get; set; }
        public decimal RegistrationFee { get; set; }
        public decimal HotelExpense { get; set; }
        public decimal PlaneTicket { get; set; }
        public decimal Other { get; set; }
    }
}
