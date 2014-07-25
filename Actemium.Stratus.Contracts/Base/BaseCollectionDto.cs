﻿using System.Runtime.Serialization;

namespace Actemium.Stratus.Contracts.Base
{
    [DataContract]
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
                return Page + 1 * PageSize < Total ? string.Format("/api/{0}?page={1}&pageSize={2}", controllerName, Page + 1, PageSize) : null;
            }
            set { }
        }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string PrevPageUrl
        {
            get
            {
                return Page > 0 ? string.Format("/api/{0}?page={1}&pageSize={2}", controllerName, Page - 1, PageSize) : null;
            }
            set { }
        }

    }
}