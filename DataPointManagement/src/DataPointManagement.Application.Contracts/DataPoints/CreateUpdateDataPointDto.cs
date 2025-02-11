
// TODO: Application.Contracts/DataPoints/CreateUpdateDataPointDto

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DataPointManagement.DataPoints;

public class CreateUpdateDataPointDto
{

    [Required]
    public required string DeviceId { get; set; }
    [Required]
    public required long Time { get; set; }
    [Required]
    public required Dictionary<string, object> Data { get; set; }
}

