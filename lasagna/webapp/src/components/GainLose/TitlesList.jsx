import React from 'react'

export default function TitlesList({ data, className}) {

    let signal = className === 'gain-items' ? '+' : '';
    let title = className === 'gain-items' ? 'Gainers' : 'Losers';
    
    if (data) {
        if(data["quotes"] != null){
            data = data["quotes"];
        }
        return (
            <section className={className}>
                <h1>{ title }</h1>
                <table className="ui very basic collapsing celled table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Change</th>
                            <th>% Change</th>
                        </tr>
                    </thead>
                    <tbody>
                {
                    data.map((item, i) => (
                        <tr key={i}>
                            <td>
                                <span className="item-symbol">{`${item.symbol}`}</span>
                                <br />
                                <span className="item-name">{`${item.displayName}`}</span>
                            </td>
                            <td>
                                <p>{`${signal}${item.regularMarketChange.toFixed(2)}`}</p>
                            </td>
                            <td>
                                <p>{`${signal}${item.regularMarketChangePercent.toFixed(2)}%`}</p>
                            </td>
                        </tr>
                    ))
                }
                    </tbody>
                </table>
            </section>
        );
    }

    return (
        <div>
            <h3>Loading...</h3>
        </div>
    );
}
