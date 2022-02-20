using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DLWMSApi.Helpers
{
    public class FileDetail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? DateEntered { get; set; }
        public bool? Deleted { get; set; }
        public string DocumentName { get; set; }
        public string DocType { get; set; }
        public string DocUrl { get; set; }
    }
}
