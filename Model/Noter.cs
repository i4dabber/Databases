using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Noter
    {
        public string Notes { get; set; }

        public virtual long NoteID { get; set; }

        public Person perso = new Person();

    }
}

