import React, { useState, useEffect } from "react";
import GainLoseFetcher from "./GainLoseFetcher";


export default function GainLose() {

    //TO DO: hide this api key
    //Maybe redux?
    const API_OPTIONS = {
        "key": "c16e3abf67msh98c7e364d5eed9ep19f991jsnd7123a9386c2",
        "host": "yahoo-finance15.p.rapidapi.com"
    }

    const yahooURLSet = {
        "gainersUrl": "https://yahoo-finance15.p.rapidapi.com/api/yahoo/co/collections/day_gainers?start=0",
        "losersUrl": "https://yahoo-finance15.p.rapidapi.com/api/yahoo/co/collections/day_losers?start=0"
    }

    const localURLSet = {
        "gainersUrl": "http://localhost:3002/gainers",
        "losersUrl": "http://localhost:3002/losers"
    }

    const options = {
        "method": "GET",
        "headers": {
            "x-rapidapi-key": API_OPTIONS.key,
            "x-rapidapi-host": API_OPTIONS.host
        }
    }


    return (
        <div>
            <GainLoseFetcher options={options} urlSet={localURLSet} />
        </div>
    )
}
