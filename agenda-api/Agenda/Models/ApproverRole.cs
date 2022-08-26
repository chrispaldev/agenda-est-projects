using System;
using System.Collections.Generic;

namespace Agenda.Models
{
    public partial class ApproverRole
    {
        public ApproverRole()
        {
            Approvers = new HashSet<Approver>();
        }

        public int RoleId { get; set; }
        public string? RoleDescription { get; set; }

        public virtual ICollection<Approver> Approvers { get; set; }
    }
}
