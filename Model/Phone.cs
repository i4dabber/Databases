using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Phone
    {

        public virtual long Phone_Nr { get; set; }

        public virtual long Phone_Type { get; set; }

        public virtual long PhoneID { get; set; }

        public Person personer = new Person();

   

    }

}