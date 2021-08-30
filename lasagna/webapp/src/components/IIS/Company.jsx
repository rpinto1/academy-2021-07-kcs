import React, { useState } from 'react'
import { Table, Dropdown } from 'semantic-ui-react'

export const Company = ({company}) => {

    
    return (
    <Table.Row>
        <Table.Cell>
          {company.ticker}
        </Table.Cell>
        <Table.Cell>
          {company["name"]}
        </Table.Cell>
        <Table.Cell>10000</Table.Cell>
        <Table.Cell>15</Table.Cell>
        <Table.Cell>50</Table.Cell>
        <Table.Cell><Dropdown floating options={[{key: "", text: "", value: ""}]} text='Profile' /></Table.Cell>
      </Table.Row>
    )
}
