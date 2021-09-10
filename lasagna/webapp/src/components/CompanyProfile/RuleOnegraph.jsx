import React from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export default function RuleOnegraph( { selected } ) {

  const {option} = selected;

//Data precisa receber info from db

const data = [{year: '2014', ROIC: 1000, Sales: 300, Earnings: 200, Equity: 200, CashFlow: 200}, 
              {year: '2015', ROIC: 500, Sales: 200, Earnings: 300, Equity: 300, CashFlow: 300},
              {year: '2016', ROIC: 200, Sales: 150, Earnings: 500, Equity: 350, CashFlow: 400},
              {year: '2017', ROIC: 50, Sales: 100, Earnings: 100, Equity: 400, CashFlow: 800},
              {year: '2018', ROIC: 800, Sales: 40, Earnings: 50, Equity: 450, CashFlow: 500},
              {year: '2019', ROIC: 10, Sales: 20, Earnings: 700, Equity: 500, CashFlow: 100},
              {year: '2020', ROIC: 200, Sales: 10, Earnings: 800, Equity: 550, CashFlow: 800}]

const userSelection = (value)=>{
  switch (value){
    case 'ROIC': 
      return(<Line type="monotone" dataKey="ROIC" stroke="#e71e28" />)
      break;
    case 'SGR': 
      return(<Line type="monotone" dataKey="Sales" stroke="#65b473" />)
      break;
    case 'EPS': 
      return(<Line type="monotone" dataKey="Earnings" stroke="#6cad0e"/>)
      break;
    case 'FCF': 
      return(<Line type="monotone" dataKey="Equity" stroke="#c545ff" />)
      break;
    case 'BVPS': 
      return(<Line type="monotone" dataKey="CashFlow" stroke="#c545f1" />)
      break;
  }
};


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
   
        {userSelection(option)}
   
          <CartesianGrid stroke="#ccc" />
          <XAxis dataKey="year" />
          <YAxis />
          <Tooltip />
          
        </LineChart>

      
    )
  }