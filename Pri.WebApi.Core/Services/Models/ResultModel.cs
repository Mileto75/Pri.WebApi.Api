using Pri.CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Services.Models
{
    public class ResultModel<T> : BaseEntity
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Result { get; set; }
    }
}
