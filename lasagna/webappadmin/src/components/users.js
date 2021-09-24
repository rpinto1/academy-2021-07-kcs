import * as React from "react";
import { List, Datagrid, TextField, EmailField, 
    Edit, Create, SimpleForm, SelectInput, TextInput } from 'react-admin';

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
            <SelectInput source="role" 
                choices={[
                    {id: "Basic User", name: "Basic User"}, 
                    {id: "Premium User", name: "Premium User"},
                    {id: "Manager", name: "Manager"}, 
                    {id: "Admin", name: "Admin"}]}
                // initialValue={"Basic User"}
                />
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
            <SelectInput source="role" 
                choices={[
                    {id: "Basic User", name: "Basic User"}, 
                    {id: "Premium User", name: "Premium User"},
                    {id: "Manager", name: "Manager"}, 
                    {id: "Admin", name: "Admin"}]}
                initialValue={"Basic User"}/>
        </SimpleForm>
    </Create>
);

const userFilters = [
    <TextInput label="First Name" source="firstName" />,    
    <TextInput label="Last Name" source="lastName" />,    
    <TextInput label="Email" source="email" />,    
    <SelectInput source="role" 
        choices={[
            {id: "Basic User", name: "Basic User"}, 
            {id: "Premium User", name: "Premium User"},
            {id: "Manager", name: "Manager"}, 
            {id: "Admin", name: "Admin"}]
        }
    />
];

