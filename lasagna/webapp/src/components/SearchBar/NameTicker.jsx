import React from 'react';
import { Link } from 'react-router-dom';


function NameTicker({ nameTicker }) {
    
       
    const { name, ticker } = nameTicker;
   
    const url = '/company/details/' + ticker + '/' + name;

    return (
        <Link to={url}>
            <p>{ticker} - {name}</p>
        </Link>
    

    )
}

export default NameTicker;