import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button } from 'semantic-ui-react';
import { urlAddCompanytoPortfolio as url } from '../PortfolioManager';
import { headers } from '../UserManager';



export default function Portfolio({portfolio}, {ticker}) {

    const { portfolioName, portfolioId } = portfolio

    const addCompanytoPortfolioModel = {
        PortfolioUuid: portfolioId,
        Ticker: ticker
    }

    const handleClick = () => axios.post(url, addCompanytoPortfolioModel)
                                .then(res => {
                                console.log(res)
                            })
                            .catch(error => console.log(error))


console.log(portfolioId)
console.log(portfolioName)
    
    return (
        <Button className="non_btn" onClick={handleClick}>{portfolioName}</Button>
    )
}
