import React from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export default function RuleOnegraph( { aspect } ) {

  const { ROIC, SGR, EPS, BVPS, FCF } = aspect;
  const randomValue = Math.ceil()*1000;

//Data precisa receber info from db

const data = [{year: '2014', ROIC: 1000, Sales: 300, Earnings: 200, Equity: randomValue, CashFlow: randomValue}, 
              {year: '2015', ROIC: 500, Sales: 200, Earnings: 300, Equity: randomValue, CashFlow: randomValue},
              {year: '2016', ROIC: 200, Sales: 150, Earnings: 500, Equity: randomValue, CashFlow: randomValue},
              {year: '2017', ROIC: 50, Sales: 100, Earnings: 100, Equity: randomValue, CashFlow: randomValue},
              {year: '2018', ROIC: 800, Sales: 40, Earnings: 50, Equity: randomValue, CashFlow: randomValue},
              {year: '2019', ROIC: 10, Sales: 20, Earnings: 700, Equity: randomValue, CashFlow: randomValue},
              {year: '2020', ROIC: 200, Sales: 10, Earnings: 800, Equity: randomValue, CashFlow: randomValue}]


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

            
        { ROIC && 
          <Line type="monotone" dataKey="ROIC" stroke="#e71e28" />
          }
        { SGR && 
          <Line type="monotone" dataKey="Sales" stroke="#65b473" />
          }
        { EPS && 
          <Line type="monotone" dataKey="Earnings" stroke="#6cad0e" />
          }
        { FCF && 
          <Line type="monotone" dataKey="Equity" stroke="#c545ff" />
          }
        { BVPS 
          && <Line type="monotone" dataKey="CashFlow" stroke="#c545f1" />
          }


        <CartesianGrid stroke="#ccc" />
        <XAxis dataKey="year" />
        <YAxis />
        <Tooltip />
          
      </LineChart>

      
    )
  }