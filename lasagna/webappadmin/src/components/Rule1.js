import * as React from "react";
import { List, Datagrid, TextField, EmailField, ReferenceField, EditButton, 
    Edit, Create, SimpleForm, ReferenceInput, SelectInput, TextInput, SearchInput } from 'react-admin';

export const Rule1List = props => (
    // <List {...props} filters={Rule1Filters}  >
    <List {...props}  >
        <Datagrid rowClick="edit">
            <TextField source="ticker" />
            <TextField source="name" />
            <TextField source="price" />    
            <TextField source="score" />    
            <TextField source="stickerPrice" />    
            <TextField source="marginSafety" />    

            {/* Task<CompanyScorePoco> SearchCompaniesByIndexAuthenticated(string indexName, string sectorName, string industryName, int page, List<string> countries) */}

            {/* <TextField source="SectorId" />
            <TextField source="IndustryId" /> */}

            {/* <TextField source="ExchangeId" />
            <TextField source="Currency" />
            <TextField source="CompanyType" /> */}
        </Datagrid>
    </List>
);


const Rule1Filters = [
    <TextInput label="First Name" source="firstName" />,    
    <TextInput label="Last Name" source="lastName" />,    
    <TextInput label="Email" source="email" />,    
];

