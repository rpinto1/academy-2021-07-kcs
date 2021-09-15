import React from "react";
import GainLoseFetcher from "./GainLoseFetcher";
import { useSelector } from 'react-redux'

export default function GainLose() {

    //TO DO: hide this api key
    //Maybe redux?
    
    
    var gainLoseURL = "http://localhost:3010/api/ExternalServices/gainlose";

    const countriesPicked = useSelector(state => state.countries);
    const countriesJson =encodeURI(JSON.stringify(countriesPicked));
    //var gainLoseURL = "http://localhost:3010/api/Companies/prices/" + countriesJson;
    return (
        <div>
            <GainLoseFetcher gainLoseURL={gainLoseURL} />
        </div>
    )
}
