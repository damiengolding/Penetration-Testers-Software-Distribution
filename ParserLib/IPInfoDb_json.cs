/* 
   Copyright (C) Damien Golding (dgolding, 2020-1-19 - 05:56)
   
   This is free software; you can redistribute it and/or
   modify it under the terms of the GNU Library General Public
   License as published by the Free Software Foundation; either
   version 2 of the License, or (at your option) any later version.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
   Library General Public License for more details.
   
   You should have received a copy of the GNU Library General Public
   License along with this software; if not, write to the Free
   Software Foundation, Inc., 59 Temple Place - Suite 330, Boston,
   MA 02111-1307, USA

  Don't use it to find and eat babies ... unless you're really REALLY hungry ;-)

*/

using Newtonsoft.Json;
using System;
using System.IO;

public class IPInfoDb {
    public static IPInfoDb FromFile(string jsonFile) {
        string json = String.Empty;
        try {
            json = File.ReadAllText(jsonFile);
            return (FromString(json));
        }
        catch (Exception ex) {
            Console.WriteLine("[-] Exception: {0}", ex.Message);
        }
        return (null);
    }
    public static IPInfoDb FromString(string jsonString) {
        try {
            return (JsonConvert.DeserializeObject<IPInfoDb>(jsonString));
        }
        catch (Exception ex) {
            Console.WriteLine("[-] Exception: {0}", ex.Message);
        }
        return (null);
    }
    public string statusCode { get; set; }
    public string statusMessage { get; set; }
    public string ipAddress { get; set; }
    public string countryCode { get; set; }
    public string countryName { get; set; }
    public string regionName { get; set; }
    public string cityName { get; set; }
    public string zipCode { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string timeZone { get; set; }
}
