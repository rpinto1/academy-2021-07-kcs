import React from 'react'

export default function PortfolioItem(props) {
    const { name, ticker } = portfolioItem;
    return (
        <div>
                        <p>{ticker} - {name}</p>
        </div>
    )
}
