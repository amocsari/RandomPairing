﻿//------------------------------------------------------------------------------
// <auto-generated>
    //     This code was generated by a tool.
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace RandomPairer.Dal.Entities
{
    public partial class User 
    {
        public User()
        {
            InversePair = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public int? PairId { get; set; }
        public virtual User Pair { get; set; }
        public virtual ICollection<User> InversePair { get; set; }
    }
}
