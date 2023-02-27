/* 
   Copyright (C) Damien Golding (dgolding, 2020-1-18 - 14:07)
   
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

public class Ip2Location {
    public static Ip2Location FromFile(string jsonFile) {
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
    public static Ip2Location FromString(string jsonString) {
        try {
            return (JsonConvert.DeserializeObject<Ip2Location>(jsonString));
        }
        catch (Exception ex) {
            Console.WriteLine("[-] Exception: {0}", ex.Message);
        }
        return (null);
    }
    public string country_code { get; set; }
    public string country_name { get; set; }
    public string region_name { get; set; }
    public string city_name { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string zip_code { get; set; }
    public string time_zone { get; set; }
    public string isp { get; set; }
    public string domain { get; set; }
    public string net_speed { get; set; }
    public string idd_code { get; set; }
    public string area_code { get; set; }
    public string weather_station_code { get; set; }
    public string weather_station_name { get; set; }
    public string mcc { get; set; }
    public string mnc { get; set; }
    public string mobile_brand { get; set; }
    public string elevation { get; set; }
    public string usage_type { get; set; }
    public int credits_consumed { get; set; }
    public Continent continent { get; set; }
    public Country country { get; set; }
    public Region region { get; set; }
    public City city { get; set; }
    public Geotargeting geotargeting { get; set; }
    public Country_Groupings[] country_groupings { get; set; }
    public Time_Zone_Info time_zone_info { get; set; }
}

public class Continent {
    public string name { get; set; }
    public string code { get; set; }
    public string[] hemisphere { get; set; }
    public object[] translations { get; set; }
}

public class Country {
    public string name { get; set; }
    public string alpha3_code { get; set; }
    public string numeric_code { get; set; }
    public string demonym { get; set; }
    public string flag { get; set; }
    public string capital { get; set; }
    public string total_area { get; set; }
    public string population { get; set; }
    public Currency currency { get; set; }
    public Language language { get; set; }
    public string idd_code { get; set; }
    public string tld { get; set; }
    public object[] translations { get; set; }
}

public class Currency {
    public string code { get; set; }
    public string name { get; set; }
    public string symbol { get; set; }
}

public class Language {
    public string code { get; set; }
    public string name { get; set; }
}

public class Region {
    public string name { get; set; }
    public string code { get; set; }
    public object[] translations { get; set; }
}

public class City {
    public string name { get; set; }
    public object[] translations { get; set; }
}

public class Geotargeting {
    public string metro { get; set; }
}

public class Time_Zone_Info {
    public string olson { get; set; }
    public DateTime current_time { get; set; }
    public int gmt_offset { get; set; }
    public string is_dst { get; set; }
}

public class Country_Groupings {
    public string acronym { get; set; }
    public string name { get; set; }
}
