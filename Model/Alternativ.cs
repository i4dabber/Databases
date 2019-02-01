using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Alternativ
    {
        public string aatype { get; set; }


        public Adress Adresse = new Adress();

        public Person Persone = new Person();
        public virtual long aaID { get; set; }



    }
}
