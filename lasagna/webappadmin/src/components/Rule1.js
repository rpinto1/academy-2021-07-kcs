import * as React from "react";
import { List, Datagrid, TextField, TextInput, Button, TopToolbar, ExportButton, BulkUpdateButton, Pagination, FilterButton, fetchUtils } from 'react-admin';
import { Fragment } from 'react';
import { stringify } from 'query-string';


const httpClient = fetchUtils.fetchJson;


const handleUpdateAll = () => {

    const url = "http://localhost:3011/api/Admin/Rule1/UpdateAllScores";

    return httpClient(url).then(({ headers, json }) => {
        // console.log("UpdateAll Result: " + JSON.stringify(json.result))
        return {
            data: json.result,
        }
    });    
}


const Rule1ListActions = (props) => (
    <TopToolbar>
        <FilterButton />
        <ExportButton />
        <Button label="Update All" onClick={ handleUpdateAll }  />
    </TopToolbar>
);


const Rule1BulkActionButtons = props => (
    <Fragment>
        <BulkUpdateButton {...props} />
    </Fragment>
);


const Rule1Pagination = props => <Pagination rowsPerPageOptions={[5, 10, 25, 50, 100, 200, 500, 1000]} {...props} />;


export const Rule1List = props => (
    <List {...props} filters={Rule1Filters}
        actions={<Rule1ListActions />}
        bulkActionButtons={<Rule1BulkActionButtons />}
        pagination={<Rule1Pagination />}>

        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="name" />
            <TextField source="country" />
            <TextField source="exchange" />
            <TextField source="sector" />
            <TextField source="industry" />
            <TextField source="price" />
            <TextField source="currency" />
            <TextField source="score" />
            <TextField source="stickerPrice" />
            <TextField source="marginSafety" />
            <TextField source="ranking" />
            <TextField source="updatedOn" />
        </Datagrid>
    </List>
);


const Rule1Filters = [
    <TextInput label="Ticker" source="id" />,
    <TextInput label="Name" source="name" />,
    <TextInput label="Country" source="country" />,
    <TextInput label="Exchange" source="exchange" />,
    <TextInput label="Sector" source="sector" />,
    <TextInput label="Industry" source="industry" />,
    <TextInput label="Currency" source="currency" />,
];

