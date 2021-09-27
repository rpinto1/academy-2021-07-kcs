import * as React from "react";
import { Admin, Resource  } from 'react-admin';
import { UserList, UserEdit, UserCreate } from './Users';
import { Rule1List } from './Rule1';
import jsonServerProvider from 'ra-data-json-server';
import UserIcon from '@material-ui/icons/Group';
import Rule1Icon from '@material-ui/icons/Book';
import dataProvider from './DataProvider';

// const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');


const AdminComp = () => (
    <Admin dataProvider={dataProvider}>
        <Resource name="Users" list={UserList} edit={UserEdit} create={UserCreate} icon={UserIcon} />
        <Resource name="Rule1" list={Rule1List} icon={Rule1Icon} />
    </Admin>
    );

export default AdminComp;