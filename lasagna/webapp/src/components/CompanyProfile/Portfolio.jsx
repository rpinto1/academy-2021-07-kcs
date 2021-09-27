import React, { useState, useEffect } from 'react';



export default function Portfolio({portfolio}) {

    const { portfolioName, portfolioCompanies } = portfolio

console.log(portfolioName)
    return (
        <p>{portfolioName}</p>
    )
}
