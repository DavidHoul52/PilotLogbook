﻿namespace LogbookApp.Commands
{

    public enum AirfieldDesignation
    {
        From,
        To
    };


    public class FlightAirfieldActionCommand :FlightActionCommand
    {
        public AirfieldDesignation AirfieldDesignation { get; set; }
    }
}