using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   
    
        public class Serveurs 
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<Database> Databases { get; set; }
        }

        public class Database
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ServeursId { get; set; }
            public Serveurs Serveurs { get; set; }
        }
    
}

