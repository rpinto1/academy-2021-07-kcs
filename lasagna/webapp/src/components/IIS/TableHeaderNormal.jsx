import React from 'react'
import {Table } from 'semantic-ui-react'
export default function TableHeaderNormal() {
    return (
        <Table.Header >
        <Table.Row textAlign="center"> 
            <Table.HeaderCell width="3">Ticker</Table.HeaderCell>
            <Table.HeaderCell width="7">Company name</Table.HeaderCell>
            <Table.HeaderCell width="3">Previous Close</Table.HeaderCell>
            <Table.HeaderCell>Profile</Table.HeaderCell>
        </Table.Row>
        </Table.Header>
    )
}
