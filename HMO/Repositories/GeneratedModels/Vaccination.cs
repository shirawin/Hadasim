using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repositories.GeneratedModels;

public partial class Vaccination
{
    public int VaccinationCode { get; set; }

    public int? MemberCode { get; set; }

    public DateTime? DateReceived { get; set; }

    public string? Manufacturer { get; set; }
    [JsonIgnore]
    public virtual Member? MemberCodeNavigation { get; set; }
}
