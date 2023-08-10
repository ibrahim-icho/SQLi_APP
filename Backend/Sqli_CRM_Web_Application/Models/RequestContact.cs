﻿namespace Sqli_CRM_Web_Application.Models
{
    public class RequestContact
    {
        public Guid contactId { get; set; }
        public string fullname { get; set; } = String.Empty;
        public string firstname { get; set; } = String.Empty;
        public string lastname { get; set; } = String.Empty;
        public string emailaddress1 { get; set; } = String.Empty;
        public string jobtitle { get; set; } = String.Empty;
        public Int32? gendercode { get; set; } = Int32.MinValue;
        public int? statecode { get; set; }
        /// <summary>
        /// Select the contact's role within the company or sales process, such as decision maker, employee, or influencer.
        /// 1 => Decision Maker, 2 => Employee, 3 => Influencer
        /// </summary>
        public int accountrolecode { get; set; }
    }
}
