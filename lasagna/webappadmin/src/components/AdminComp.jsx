import * as React from "react";
 import { Admin, Resource  } from 'react-admin';
import { UserList, UserEdit, UserCreate, UserDelete } from './users';
import { scoreList, scoreEdit, scoreCreate } from './scores';
import jsonServerProvider from 'ra-data-json-server';
import UserIcon from '@material-ui/icons/Group';
import scoreIcon from '@material-ui/icons/Book';
import dataProvider from './DataProvider';

// const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');


const AdminComp = () => (
    <Admin dataProvider={dataProvider}>
        <Resource name="users" list={UserList} edit={UserEdit} create={UserCreate} icon={UserIcon} />
        <Resource name="scores" list={scoreList} edit={scoreEdit} create={scoreCreate} icon={scoreIcon} />
    </Admin>
    );

export default AdminComp;