import * as React from "react";
import { List, Datagrid, TextField, EmailField, ReferenceField, EditButton, 
    Edit, Create, SimpleForm, ReferenceInput, SelectInput, TextInput, SearchInput } from 'react-admin';

export const UserList = props => (
    <List {...props} filters={userFilters}  >
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="firstName" label="First Name" />
            <TextField source="lastName" label="Last Name" />
            <EmailField source="email" />
            <TextField source="role" />
        </Datagrid>
    </List>
);

export const UserEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="firstName" />
            <TextInput source="lastName" />
            <TextInput disabled source="email" />
        </SimpleForm>
    </Edit>
);

export const UserCreate = props => (
    <Create {...props}>
        <SimpleForm>
            {/* <TextInput disabled source="id" /> */}
            <TextInput source="firstName" />
            <TextInput source="lastName" />
            <TextInput source="email" />
        </SimpleForm>
    </Create>
);

const userFilters = [
    <TextInput label="First Name" source="firstName" />,    
    <TextInput label="Last Name" source="lastName" />,    
    <TextInput label="Email" source="email" />,    
];

