using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SymphonyLtd.ViewModels
{
    public class ExamMappingVM
    {
        public int? ExamID { get; set; }
        public int? StudentID { get; set; }
        public string ExamName { get; set; }
        public string StudentName { get; set; }
        public string StudentGR { get; set; }
        public TimeSpan ExamDuration { get; set; }
    }
}