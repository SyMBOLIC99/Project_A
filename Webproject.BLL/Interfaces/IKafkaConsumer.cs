using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webproject.BLL.Interfaces
{
    public  interface IKafkaConsumer
    {
        public Task ConsumeClientAsync(Client c);
    }
}
