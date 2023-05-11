using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repositories.GeneratedModels;

public partial class Member
{
    public int Code { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Id { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? HouseNumber { get; set; }

    public string? Phone { get; set; }

    public string? MobilePhone { get; set; }
    [JsonIgnore]
    public virtual ICollection<CoronaVirusDetail> CoronaVirusDetails { get; set; } = new List<CoronaVirusDetail>();
    [JsonIgnore]
    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
