import * as React from "react";
import { List, Datagrid, TextField, EmailField, ReferenceField, EditButton, 
    Edit, Create, SimpleForm, ReferenceInput, SelectInput, TextInput, SearchInput } from 'react-admin';

export const scoreList = props => (
    <List {...props} filters={scoreFilters}  >
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Ticker" />
            <TextField source="Name" />
            <TextField source="ExchangeId" />
            <TextField source="SectorId" />
            <TextField source="IndustryId" />
            <TextField source="Currency" />
            <TextField source="CompanyType" />
        </Datagrid>
    </List>
);

export const scoreEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="firstName" />
            <TextInput source="lastName" />
            <TextInput disabled source="email" />
        </SimpleForm>
    </Edit>
);

export const scoreCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="firstName" />
            <TextInput source="lastName" />
            <TextInput source="email" />
        </SimpleForm>
    </Create>
);

const scoreFilters = [
    <TextInput label="First Name" source="firstName" />,    
    <TextInput label="Last Name" source="lastName" />,    
    <TextInput label="Email" source="email" />,    
];

