
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DonorAppVersion2.Models
{

using System;
    using System.Collections.Generic;
    
public partial class MatchRequestedByParent
{

    public int ParentMatchRequestId { get; set; }

    public int ParentId { get; set; }

    public Nullable<System.DateTime> RequestDate { get; set; }

    public bool isApproved { get; set; }

    public string Status { get; set; }

    public Nullable<bool> isPaidByParent { get; set; }

    public Nullable<int> ParentsPaymentId { get; set; }

    public string Note { get; set; }



    public virtual Parent Parent { get; set; }

}

}
