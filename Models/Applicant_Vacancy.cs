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

    public partial class Applicant_Vacancy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Applicant_Vacancy()
        {
            this.Interviews = new HashSet<Interview>();
        }
    
        [DisplayName("App-Vac code")]
        
        public int VAId { get; set; }
        [DisplayName("Aplicant code")]
    
        public int ApId { get; set; }
        [DisplayName("Vacancy code")]
    
        public string VacID { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime AttachesDate { get; set; }
        public string Status { get; set; }
    
        public virtual Applicant Applicant { get; set; }
        public virtual Vacancy Vacancy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
