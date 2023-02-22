using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zametochka
{
    internal class DataModel
    {
        public DateTime CrDate { get; set; } = DateTime.Today;
        private string _Descr;
        private string _Name;
        private bool _ReadyOrNot;
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }


        public bool ReadyOrNot
        {
            get { return _ReadyOrNot; }
            set { _ReadyOrNot = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        public string Descr
        {
            get { return _Descr; }
            set { _Descr = value; }
        }


    }
}
