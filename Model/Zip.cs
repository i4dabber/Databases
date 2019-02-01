using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ZIP
    {
        public virtual string City_Name { get; set; }


        public virtual string Country_Name { get; set; }

        public virtual int Postal_Code { get; set; }

        public virtual long ZipID { get; set; }

        public ZipListe ZIPL = new ZipListe();



    }
}