using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Server
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ConnexionString { get; set; }
        //public virtual ICollection<serveurdata> server.serveurdatas { get; set; }

        
    }
}

