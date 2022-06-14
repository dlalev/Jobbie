using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Models
{
    public class ClientPost
    {
        public Guid Id { get; set; }
        public string Title{ get; set; }
        public DateTime AddedOnDate { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}