
// TODO: Domain/DataPoints/DataPoint

using System;
using System.Collections.Generic;

namespace DataPointManagement.DataPoints;
public class DataPoint
{
    public DataPoint(string deviceId, long time, Dictionary<string, object> data) 
    {
        this.DeviceId = deviceId;
        this.Time = time;
        this.Data = data;
    }
    public string DeviceId { get; set; }
    public long Time { get; set; }
    public Dictionary<string, object> Data { get; set; }

/**
    public string Values { get; set; } // Store values as a single string

    // Add a method to convert Values string to a list of column names (measurement names)
    public List<string> ToColumns()
    {
        // Assuming Values are stored in the format: "temp=25.5,hum=60.2,press=1012"
        return Values?.Split(',').Select(v => v.Split('=')[0].Trim()).ToList() ?? new List<string>();
    }

    // Add a method to convert Values string to a list of values
    public List<object> ToValues()
    {
        // Assuming Values are stored in the format: "temp=25.5,hum=60.2,press=1012"
        return Values?.Split(',').Select(v =>
        {
            string valStr = v.Split('=')[1].Trim();
            if (double.TryParse(valStr, out double val))
            {
                return (object)val;
            }
            return valStr; // Return as string if not a double
        }).ToList() ?? new List<object>();
    }
**/

}
