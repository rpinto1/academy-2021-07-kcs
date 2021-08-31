import React from 'react';


function NameTicker(props) {
    {
        console.log('Inside name ticker');
        console.log(props);
    }
    return (
        <article>
            <a href={'localhost:3010/company/detail/'+props.nameTicker.ticker} target="_blank">
                <p>{props.nameTicker.name}</p>
            </a>
        </article>

    )
}

export default NameTicker;