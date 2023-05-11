using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repositories.GeneratedModels;

public partial class CoronaVirusDetail
{
    public int? MemberCode { get; set; }

    public DateTime? DateOfPositiveAnswer { get; set; }

    public DateTime? DateOfRecovery { get; set; }

    public int Code { get; set; }
    [JsonIgnore]
    public virtual Member? MemberCodeNavigation { get; set; }
}
