
// TODO: Application.Contracts/DataPoints/DeleteDataPointDto

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DataPointManagement.DataPoints;

public class DeleteDataPointDto
{

    [Required]
    public required string DeviceId { get; set; }
    [Required]
    public required long Time { get; set; }
    [Required]
    public required string DataKey { get; set; }
}

