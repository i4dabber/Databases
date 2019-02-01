
using System.Collections.Generic;

namespace Model
{
    public class Person
    {
        public virtual string First_Name { get; set; }

        public virtual string Middle_Name { get; set; }

        public virtual string Last_Name { get; set; }

        public virtual long PersonID { get; set; }

   

        public virtual Phone Phones { get; set; }

        public virtual Adress PrimaryAddress { get; set; }

        public virtual E_mail E_mails { get; set; }
        public virtual Alternativ alternativ { get; set; }

        public virtual ZIP Zips { get; set; }

    }
}