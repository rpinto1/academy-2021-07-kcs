import React, { useEffect, useState } from 'react'
import { Table, Dropdown } from 'semantic-ui-react'

export const Company = ({company}) => {

  const [authenticated, setAuthenticated] = useState(false)
  
  useEffect(() => {
    if(company["score"] !== undefined){
      setAuthenticated(true)
    }else{
      setAuthenticated(false)
    }
  }, [company])


    return (
    <Table.Row textAlign="center">
        <Table.Cell>
          {company.ticker}
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
        <Table.Cell><Dropdown  floating options={[{key: "", text: "Please Log In First", value: ""}]} text='Profile' /></Table.Cell>
      </Table.Row>
    )
}
