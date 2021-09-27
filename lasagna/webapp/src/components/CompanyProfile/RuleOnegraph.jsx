import React, { useState, useEffect} from 'react'
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';

export default function RuleOnegraph( { selected } ) {

  const {ticker, label} = selected;
  const [data, setData] = useState([]);


useEffect (() => fetch(`http://localhost:3010/api/Companies/search/${ticker}`).then(result => {
  if (result.status !== 200) {
      console.log("error");
      return;
  }
  result.json().then(data => {
      if (data != null) {
        setData(data.result);
        console.log(data.result)
      }
  })
}), [ticker]);


function camelCase(str) {
  return str.replace(/(?:^\w|[A-Z]|\b\w)/g, function(word, index)
  {
      return index === 0 ? word.toLowerCase() : word.toUpperCase();
  }).replace(/\s+/g, '');
}

const labelCamelCased = camelCase(label);

console.log(ticker);


const userSelection = ()=>{
  return(
          <Line 
          type="monotone" 
          dataKey={labelCamelCased}
          stroke="#01C8AC" 
          name={label} />
          
        ) 
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
   
        {userSelection()}
   
          <CartesianGrid stroke="#ccc" />
          <XAxis dataKey="year" reversed />
          <YAxis />
          <Tooltip />
          
        </LineChart>

      
    )
  }