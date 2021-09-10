import React, { useState } from 'react'
import { Table, Dropdown } from 'semantic-ui-react'

export const Company = ({company}) => {

    
    return (
    <Table.Row textAlign="center">
        <Table.Cell>
          {company.ticker}
        </Table.Cell>
        <Table.Cell>
          {company["name"]}
        </Table.Cell>
        {false &&
          <Table.Cell>10000</Table.Cell>
        }
        <Table.Cell>{company.price == null? "Unavailable":company.price }</Table.Cell>
        {false &&
          <Table.Cell>50</Table.Cell>
        }
        <Table.Cell><Dropdown  floating options={[{key: "", text: "Please Log In First", value: ""}]} text='Profile' /></Table.Cell>
      </Table.Row>
    )
}
