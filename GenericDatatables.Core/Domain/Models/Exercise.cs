using System;
using System.Security.Principal;
using GenericDatatables.Core.Base.Models;

namespace GenericDatatables.Core.Domain.Models
{
    public class Exercise: Entity
    {
        public virtual string Name { get; set; }
        public virtual TimeSpan Duration { get; set; }

        public virtual int GymMemberId { get; set; }
        public virtual GymMember GymMember { get; set; }    
    }
}
