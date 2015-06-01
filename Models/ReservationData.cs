using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelNWT.Models
{
    public class ReservationData
    {
        public int idreservation { get; set; }
        public System.DateTime from_date { get; set; }
        public System.DateTime to_date { get; set; }
        public double price { get; set; }
        public byte status { get; set; }
        public Nullable<byte> type { get; set; }
        public int user_iduser { get; set; }

        public virtual user user { get; set; }
    }
}