import React from 'react'
import {Table } from 'semantic-ui-react'
export default function TableHeaderAuth() {
    return (
        <Table.Header >
        <Table.Row textAlign="center"> 
            <Table.HeaderCell width="2">Ticker</Table.HeaderCell>
            <Table.HeaderCell width="4">Company name</Table.HeaderCell>
            <Table.HeaderCell width="2">Score</Table.HeaderCell>
            <Table.HeaderCell width="2">Sticker Price</Table.HeaderCell>
            <Table.HeaderCell width="2">Margin Of Safety</Table.HeaderCell>
            <Table.HeaderCell width="2">Previous Close</Table.HeaderCell>
            <Table.HeaderCell>Profile</Table.HeaderCell>
        </Table.Row>
        </Table.Header>
    )
}
