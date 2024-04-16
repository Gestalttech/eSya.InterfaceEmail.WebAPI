﻿using System;
using System.Collections.Generic;

namespace eSya.InterfaceEmail.DL.Entities
{
    public partial class GtEcem91
    {
        public int BusinessKey { get; set; }
        public string OutgoingMailServer { get; set; } = null!;
        public int Port { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
