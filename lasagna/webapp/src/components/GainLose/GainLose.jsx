﻿import React, { useState, useEffect } from "react";
import GainLoseFetcher from "./GainLoseFetcher";


export default function GainLose() {

    //TO DO: hide this api key
    //Maybe redux?
    
    
    var gainLoseURL = "localhost:3010/api/ExternalServices/gainlose";

    return (
        <div>
            <GainLoseFetcher gainLoseURL={gainLoseURL} />
        </div>
    )
}