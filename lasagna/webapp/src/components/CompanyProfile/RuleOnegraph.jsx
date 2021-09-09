import React from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export default function RuleOnegraph() {


//Data precisa receber info from db

const data = [{year: '1990', ROIC: 1000, StickerPrice: 150, Score: 100, CashFlow: 1000}, 
              {year: '1991', ROIC: 200, StickerPrice: 410 , Score: 200, CashFlow: 200},
              {year: '1992', ROIC: -40, StickerPrice: 90, Score: 500, CashFlow: 2000},
              {year: '1993', ROIC: 80, StickerPrice: 620, Score: 100, CashFlow: 4000},];
            


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

            
        <Line type="monotone" dataKey="ROIC" stroke="#e71e28" />
        <Line type="monotone" dataKey="StickerPrice" stroke="#e71e28" />
        <Line type="monotone" dataKey="Score" stroke="#6cad0e" />
        <Line type="monotone" dataKey="CashFlow" stroke="#c545ff" />


        <CartesianGrid stroke="#ccc" />
        <XAxis dataKey="year" />
        <YAxis />
        <Tooltip />
        <Legend />
      </LineChart>
    )
  }