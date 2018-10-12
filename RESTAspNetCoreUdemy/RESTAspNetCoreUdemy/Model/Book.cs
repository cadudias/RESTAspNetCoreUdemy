using RESTAspNetCoreUdemy.Model.Base;
using System;

namespace RESTAspNetCoreUdemy.Model
{
    public class Book : BaseEntity
    {
        public string Title
        {
            get; set;
        }

        public string Author
        {
            get; set;
        }

        public string Price
        {
            get; set;
        }

        public DateTime LaunchDate
        {
            get; set;
        }
    }
}