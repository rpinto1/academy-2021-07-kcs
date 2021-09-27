import React, { useState, useEffect } from 'react';


import { urlGetPortofolios, fetchAndSetPortfolio } from '../PortfolioManager';

export default function PortfolioListDropdownMenu() {

    const [portfolios, setPortfolios] = useState([]);

    const [noPortfolioInfo, setNoPortfolioInfo] = useState(false);
    const [finishedLoading, setFinishedLoading] = useState(false);


    useEffect(() => {

        (() => {
            fetchAndSetPortfolio(urlGetPortofolios, setPortfolios);
            console.log(portfolios);

            if (portfolios.length === 0) {
                setNoPortfolioInfo(true);
            } 

            setFinishedLoading(() => true);

        })();

    }, []);

    console.log(urlGetPortofolios);




    return (
        <div className="ui raised fluid text segment">
            {portfolios}
        </div>
    )
}
