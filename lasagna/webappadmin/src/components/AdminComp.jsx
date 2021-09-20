import * as React from "react";
 import { Admin, Resource  } from 'react-admin';
import { UserList, UserEdit, UserCreate, UserDelete } from './users';
import { PostList, PostEdit, PostCreate } from './posts';
import jsonServerProvider from 'ra-data-json-server';
import PostIcon from '@material-ui/icons/Book';
import UserIcon from '@material-ui/icons/Group';
import dataProvider from './DataProvider';

// const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');


const AdminComp = () => (
    <Admin dataProvider={dataProvider}>
        <Resource name="posts" list={PostList} edit={PostEdit} create={PostCreate} icon={PostIcon} />
        <Resource name="users" list={UserList} edit={UserEdit} create={UserCreate} icon={UserIcon} />
    </Admin>
    );

export default AdminComp;