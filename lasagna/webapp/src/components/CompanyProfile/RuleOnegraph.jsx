import React from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export default function RuleOnegraph( { aspect } ) {

  const { ROIC, SGR, EPS, BVPS, FCF } = aspect;
  const randomValue = Math.ceil()*1000;

//Data precisa receber info from db

const data = [{year: '2014', ROIC: randomValue, Sales: randomValue, Earnings: randomValue, Equity: randomValue, CashFlow: randomValue}, 
              {year: '2015', ROIC: randomValue, Sales: randomValue, Earnings: randomValue, Equity: randomValue, CashFlow: randomValue},
              {year: '2016', ROIC: randomValue, Sales: randomValue, Earnings: randomValue, Equity: randomValue, CashFlow: randomValue},
              {year: '2017', ROIC: randomValue, Sales: randomValue, Earnings: randomValue, Equity: randomValue, CashFlow: randomValue},
              {year: '2018', ROIC: randomValue, Sales: randomValue, Earnings: randomValue, Equity: randomValue, CashFlow: randomValue},
              {year: '2019', ROIC: randomValue, Sales: randomValue, Earnings: randomValue, Equity: randomValue, CashFlow: randomValue},
              {year: '2020', ROIC: randomValue, Sales: randomValue, Earnings: randomValue, Equity: randomValue, CashFlow: randomValue}]


    return (

        <LineChart 
        width={500} 
        height={500} 
        data={data} 
        margin={{
            top: 100,
            right: 30,
            left: 20,
            bottom: 5,
          }}>

            
        {ROIC && <Line type="monotone" dataKey="ROIC" stroke="#e71e28" />}
        { SGR && <Line type="monotone" dataKey="Sales" stroke="#e71e28" />}
        { EPS && <Line type="monotone" dataKey="Earnings" stroke="#6cad0e" />}
        { FCF && <Line type="monotone" dataKey="Equity" stroke="#c545ff" />}
        { BVPS && <Line type="monotone" dataKey="CashFlow" stroke="#c545f1" />}


        <CartesianGrid stroke="#ccc" />
        <XAxis dataKey="year" />
        <YAxis />
        <Tooltip />
        <Legend />
          
      </LineChart>

      
    )
  }