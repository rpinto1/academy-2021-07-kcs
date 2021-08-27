import React, { useState } from 'react'
import { Table, Dropdown } from 'semantic-ui-react'

export const Company = () => {

    
    return (
    <Table.Row>
        <Table.Cell>
          BAC
          BanckOfAmerica
        </Table.Cell>
        <Table.Cell>10000</Table.Cell>
        <Table.Cell>15</Table.Cell>
        <Table.Cell>50</Table.Cell>
        <Table.Cell><Dropdown floating options={[{key: "", text: "", value: ""}]} text='Profile' /></Table.Cell>
      </Table.Row>
    )
}
