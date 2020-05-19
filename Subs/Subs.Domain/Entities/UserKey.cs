using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Subs.Domain.Entities
{
    public class UserKey
    {
        [Key]
        public string Username { get; set; }
        public string Key { get; set; }
    }
}
