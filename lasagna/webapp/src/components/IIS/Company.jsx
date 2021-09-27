import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { Table, Dropdown } from 'semantic-ui-react'
import { userId } from '../UserManager'

export const Company = ({company, portfolios}) => {

  const [authenticated, setAuthenticated] = useState(false)
  const [options, setOptions] = useState([{key: "", text: "Please Log In First", value: ""}])

  const turnIntoOptions = (data) => {return data.map(x=>({
    key: x["portfolioId"],
    text: x["portfolioName"],
    value: x["portfolioId"],
})) }

  useEffect(() => {
    if(userId != null){
      setOptions(turnIntoOptions(portfolios))
    }else{
      setOptions([{key: "", text: "Please Log In First", value: ""}])
    }
    
  }, [authenticated])

  useEffect(() => {
    if(company["score"] !== undefined){
      setAuthenticated(true)
    }else{
      setAuthenticated(false)
    }
  }, [company])

  const url = '/company/details/' + company.ticker +"/"+company.name;
    return (
    <Table.Row textAlign="center">
        <Table.Cell>
    <Link to={url}>
        <p>{company.ticker}</p>
    </Link>
        </Table.Cell>
        <Table.Cell>
          {company["name"]}
        </Table.Cell>
        {authenticated &&
          <Table.Cell>{company.score == null? "No Score" : company["score"].toFixed(2)}</Table.Cell> //score
        }
       
        {authenticated &&
          <Table.Cell>{company["stickerPrice"]}</Table.Cell> // sticker price 
        }
        {authenticated &&
          <Table.Cell>{company["marginSafety"]}</Table.Cell>// Margin of safety
        }
         <Table.Cell>{company.price == null? "Unavailable":company.price }</Table.Cell>
        <Table.Cell><Dropdown  floating options={options} text='Profile' /></Table.Cell>
      </Table.Row>
    )
}
