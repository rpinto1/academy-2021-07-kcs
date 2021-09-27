import React, {useState,useEffect} from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';

export default function RuleOnegraph( { selected } ) {

  const {ticker, option} = selected;
  console.log(selected)
  const [data, setData] = useState([]);

//ask help on this. ticker.ticker is horrible. 

useEffect (() => fetch(`http://localhost:3010/api/Companies/search/${ticker.ticker}`).then(result => {
  if (result.status != 200) {
      console.log("error");
      return;
  }
  result.json().then(data => {
      if (data != null) {
        setData(data.result);
      }
  })
}), []);


console.log(data);


const userSelection = (value)=>{
  switch (value){
    case 'ROIC': 
      return(<Line type="monotone" dataKey="returnOnInvestedCapital" stroke="#e71e28" name='Return on invested capital' />)
      break;
    case 'DPS': 
      return(<Line type="monotone" dataKey="dividendsPerShare" stroke="#333EFF" name='Dividends per share' />) //azul rey
      break;
    case 'GM': 
      return(<Line type="monotone" dataKey="grossMargin" stroke="#EC8407" name='Gross margin'/>) //azul pastel
      break;
    case 'GP': 
      return(<Line type="monotone" dataKey="grossProfit" name= 'Gross profit' stroke="#27D507"/>) //verde neon
      break;  
    case 'OM': 
      return(<Line type="monotone" dataKey="operatingMargin" name= 'Operating margin' stroke="#A900F5" />) //morado
      break;
    case 'OP': 
      return(<Line type="monotone" dataKey="operatingProfit" name= 'Operating profit' stroke="#FF6400" />) //anaranjado
      break;
    case 'ROA': 
      return(<Line type="monotone" dataKey="returnOnAssets" name= 'Return on assets ' stroke="#FF0000" />) // rojo
      break;
    case 'ROE': 
      return(<Line type="monotone" dataKey="returnOnEquity" name= 'Return on equity' stroke="#FF0083" />) //rosado
      break;
    case 'R': 
      return(<Line type="monotone" dataKey="revenue" name= 'Revenue' stroke="#01C8AC" />) //turquesa
      break;
    case 'RG': 
      return(<Line type="monotone" dataKey="revenueGrowth" name= 'Revenue growth' stroke="#03E9A5"/>) //verde mar 
      break;

    }
};


    return (

        <LineChart 
          width={700} 
          height={500} 
          data={data} 
          margin={{
            top: 100,
            right: 30,
            left: 50,
            bottom: 5,
        }}>
   
        {userSelection(option)}
   
          <CartesianGrid stroke="#ccc" />
          <XAxis dataKey="year" reversed />
          <YAxis />
          <Tooltip />
          
        </LineChart>

      
    )
  }