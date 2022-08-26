using System;
using System.Collections.Generic;

namespace Agenda.Models
{
    public partial class Approver
    {
        public int ApproverId { get; set; }
        public string? ApproverName { get; set; }
        public int? RoleId { get; set; }

        public virtual ApproverRole? Role { get; set; }
    }
}
