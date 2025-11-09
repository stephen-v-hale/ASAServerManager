using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ASARcon;

public class ASAPlayerCoordinates
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public static ASAPlayerCoordinates FromString( string coordString )
    {
        // Example input: "X=1234.56, Y=789.12, Z=345.67"
        var parts = coordString.Split(',');

        var coordinates = new ASAPlayerCoordinates();

        foreach ( var part in parts )
        {
            var trimmed = part.Trim();
            var keyValue = trimmed.Split('=');
            if ( keyValue.Length != 2 ) continue;

            // Parse the float and cast to int
            float value = float.Parse(keyValue[1], CultureInfo.InvariantCulture);
            switch ( keyValue[0].ToUpper() )
            {
                case "X":
                coordinates.X = ( int )value;
                break;
                case "Y":
                coordinates.Y = ( int )value;
                break;
                case "Z":
                coordinates.Z = ( int )value;
                break;
            }
        }

        return coordinates;
    }
}

