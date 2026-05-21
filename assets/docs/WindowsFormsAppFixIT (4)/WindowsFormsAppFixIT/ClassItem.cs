using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppFixIT
{
    internal class ClassItem
    {
        public int ID;
        public string Nom;
        public ClassItem(int ID, string Nom)
        {
            this.ID = ID;
            this.Nom = Nom;
        }

        public override string ToString()
        {
            return this.Nom;
        }
    }
}
