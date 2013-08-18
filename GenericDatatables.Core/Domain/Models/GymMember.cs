using System;
using System.Collections.Generic;
using GenericDatatables.Core.Base.Models;

namespace GenericDatatables.Core.Domain.Models
{
    public class GymMember: Entity
    {
        private ICollection<Exercise> _excercises;
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual DateTime? DateOfBirth { get; set; }

        public virtual double? Weight { get; set; }

        public virtual decimal? MembershipPrice { get; set; }

        public virtual TimeSpan? AverageExerciseTime { get; set; }

        public virtual ICollection<Exercise> Excercises
        {
            get { return _excercises ?? (_excercises = new List<Exercise>()); }
            set { _excercises = value; }
        }
    }
}
