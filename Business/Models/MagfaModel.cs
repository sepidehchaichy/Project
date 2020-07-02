using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    //[Table("Request")]
    public class MagfaModel
    {
        public int CountGLN { get; set; }
        public int ExternalCodeCount { get; set; }
        public int InternalCodeCount { get; set; }
        public string Title { get; set; }

        //[ForeignKey("FK_RequestType")]
        //public int RequestTypeID { get; set; }
        //public virtual RequestType FK_RequestType { get; set; }
        //public virtual ICollection<RequestGTIN> LastRequestGTINs { get; set; }

    }
}
