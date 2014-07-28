//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Actemium.Stratus.DataObjects
{
     
    using System;
    using System.Collections.Generic;
    
    
    public partial class Result : IEntity  
    {
        public Result()
        {
            this.ChildResults = new HashSet<Result>();
        }
    
        public int Id { get; set; }
        public int SequenceExecutionId { get; set; }
        public double RelativeTime { get; set; }
        public int ResultDescriptionId { get; set; }
        public ResultType Type { get; set; }
        public Nullable<ResultStatus> Status { get; set; }
        public string Value { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public string Units { get; set; }
        public Nullable<bool> IsHidden { get; set; }
        public Nullable<bool> IsFixed { get; set; }
        public Nullable<int> ParentResultId { get; set; }
        public bool IsHistoric { get; private set; }
        public int ProductId { get; set; }
        public int SequenceId { get; set; }
        public Nullable<DataFileType> DataType { get; set; }
        public ResultSource ResultSource { get; set; }
    
        public virtual SequenceExecution SequenceExecution { get; set; }
        public virtual ResultDescription ResultDescription { get; set; }
        public virtual ICollection<Result> ChildResults { get; set; }
        public virtual Result ParentResult { get; set; }
        public virtual Product Product { get; set; }
        public virtual Sequence Sequence { get; set; }
    }
}
