using System;
using System.Collections.Generic;

namespace Agenda.Models
{
    public partial class AgendaItem
    {
        public int AgendaId { get; set; }
        public string? AgendaItem1 { get; set; }
        public int? AgendaTypeId { get; set; }
        public int? ApproverId { get; set; }
        public int? RoleId { get; set; }
    }
}
