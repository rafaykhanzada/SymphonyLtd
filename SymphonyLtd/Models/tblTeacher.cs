//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SymphonyLtd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTeacher
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string Subj { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Start { get; set; }
        public string Experience { get; set; }
        public Nullable<int> Likes { get; set; }
    }
}