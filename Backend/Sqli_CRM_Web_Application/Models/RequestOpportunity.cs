namespace Sqli_CRM_Web_Application.Models
{
    public class RequestOpportunity
    {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime EstimatedCloseDate { get; set; }
            public decimal Amount { get; set; }
            public int Status { get; set; }
            public int PriorityCode { get; set; }
            public Guid CustomerID { get; set; }
            public Guid OwnerID { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime ModifiedOn { get; set; }
            public int SalesStageCode { get; set; }
            public bool IsRecurring { get; set; }
            public int CloseProbability { get; set; }
            public decimal EstimatedValue { get; set; }
            public decimal ActualValue { get; set; }
            public string Competitors { get; set; }
            public string ProposedSolution { get; set; }
            public bool DecisionMaker { get; set; }
            public Guid ContactID { get; set; }
            public Guid AccountID { get; set; }
            public string Tags { get; set; }
            public string Notes { get; set; }
            public string CurrentSituation { get; set; }
            public int PurchaseTimeframe { get; set; }
        }

}

