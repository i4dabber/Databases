using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Model
{
    public class Adress
    {

        public virtual string Street_Name { get; set; }

        public virtual int Street_Nr { get; set; }

        public virtual int Apartment_Nr { get; set; }

        public virtual long AdressID { get; set; }

        public virtual int PersonID { get; set; }

        public virtual ZIP ZIPs { get; set; }
    }
}
