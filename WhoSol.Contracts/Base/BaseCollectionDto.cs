using System.Runtime.Serialization;

namespace WhoSol.Contracts.Base
{
    [DataContract(Namespace="")]
    public abstract class BaseCollectionDto
    {
        protected string controllerName;

        [DataMember]
        public int Total { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int Page { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int PageSize { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string NextPageUrl
        {
            get
            {
                return Page * PageSize < Total ? string.Format("/api/{0}?page={1}&pageSize={2}", controllerName, Page + 1, PageSize) : null;
            }
            set { }
        }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string PrevPageUrl
        {
            get
            {
                return Page > 1 ? string.Format("/api/{0}?page={1}&pageSize={2}", controllerName, Page - 1, PageSize) : null;
            }
            set { }
        }

        public BaseCollectionDto()
        {
            PageSize = 100;
            Page = 1;
        }
    }
}
