using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class E_mail
    {
        public string Email_Adress { get; set; }

        public virtual long EmailID { get; set; }

        public Person persons = new Person();

    }
}