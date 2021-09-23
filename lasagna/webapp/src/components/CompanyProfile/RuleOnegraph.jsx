import React, {useState,useEffect} from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from 'recharts';

export default function RuleOnegraph( { selected } ) {

  const {ticker, option} = selected;
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
      return(<Line 
              type="monotone" 
              dataKey="returnOnInvestedCapital"
              stroke="#e71e28" />)
      break;
    case 'DPS': 
      return(<Line type="monotone" dataKey="dividendsPerShare" stroke="#65b473" />)
      break;
    case 'GM': 
      return(<Line type="monotone" dataKey="grossMargin" stroke="#6cad0e"/>)
      break;
    case 'GP': 
      return(<Line type="monotone" dataKey="grossProfit" name= 'Gross jndajs'stroke="#6cad0e"/>)
      break;  
    case 'OM': 
      return(<Line type="monotone" dataKey="operatingMargin" stroke="#c545ff" />)
      break;
    case 'OP': 
      return(<Line type="monotone" dataKey="operatingProfit" stroke="#c545f1" />)
      break;
    case 'ROA': 
      return(<Line type="monotone" dataKey="returnOnAssets" stroke="#c545f1" />)
      break;
    case 'ROE': 
      return(<Line type="monotone" dataKey="returnOnEquity" stroke="#c545f1" />)
      break;
    case 'R': 
      return(<Line type="monotone" dataKey="revenue" stroke="#c545f1" />)
      break;
    case 'RG': 
      return(<Line type="monotone" dataKey="revenueGrowth" stroke="#c545f1" />)
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