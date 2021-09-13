import React from 'react';
import { Link } from 'react-router-dom';


function NameTicker(props) {
    {
        console.log('Inside name ticker');
        console.log(props);
    }
    return (
            <Link to='/company/details/${props.nameTicker.ticker}'> 
                <p>{props.nameTicker.name}</p>
            </Link>
    

       /*  <article>
                 <a href={'localhost:3010/company/detail/'+props.nameTicker.ticker} target="_blank">
                <p>{props.nameTicker.name}</p>
            </a>
        </article> */

    )
}

export default NameTicker;