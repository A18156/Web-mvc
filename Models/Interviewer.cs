//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recruitment_Process_System_HR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Interviewer
    {
        [DisplayName("Interviewer Code")]
        
        public string InterCode { get; set; }
        public string Status { get; set; }
        [DisplayName("Interview code")]
  
        public int InterID { get; set; }
        [DisplayName("Employee code")]
      
        public string EmployeeCode { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Interview Interview { get; set; }
    }
}
