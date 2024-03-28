using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class TransactionDto
    {
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public double Sum { get; set; }
    }
}